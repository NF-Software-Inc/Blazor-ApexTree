// Headless browser smoke + interaction test for the Blazor-ApexTree demo.
//
// Why this exists: a Blazor chart wrapper can build and even serialize its options perfectly
// yet still fail in the browser, because the real contract lives in the JS interop and the core
// library. This test drives the actual WASM app in a real browser and fails CI if any demo page
// throws, shows the Blazor error UI, renders no chart, or breaks the selection round-trip.
//
// Usage:
//   BASE_URL=http://localhost:5182 node smoke.mjs
//   PW_CHANNEL=chrome node smoke.mjs   # drive an installed Chrome instead of bundled chromium
//
// In CI we install the full `playwright` package (bundled chromium). Locally you can point it at
// an installed Chrome with PW_CHANNEL=chrome, which also works with only `playwright-core` present.
let chromium;
try {
  ({ chromium } = await import('playwright'));
} catch {
  ({ chromium } = await import('playwright-core'));
}

const BASE = process.env.BASE_URL || 'http://localhost:5182';
const channel = process.env.PW_CHANNEL;

// Chart demo routes. Each must render a tree SVG with no console/page errors. The home route ('')
// is a landing page with no chart, so it is exercised by the boot step below (errors only, no SVG).
const ROUTES = [
  'basic', 'custom-template', 'styled-nodes', 'expand-collapse', 'custom-tooltip',
  'node-click', 'dynamic-view', 'org-card', 'selection', 'theme-search',
  'external-labels', 'localization',
  // ApexTree 2.0 surface: these cover the new interop paths (updateData, focus, active path,
  // card expansion, batch verbs) and the radial layout.
  'live-data', 'radial', 'focus-mode', 'expandable-cards', 'active-path',
  'semantic-zoom', 'batch-verbs',
];

// Expected console noise, not a failure. The demo applies a licence key locked to its GitHub Pages
// domain, so the core correctly reports the mismatch whenever the demo runs anywhere else, which
// includes localhost both here and in CI. Filtering it keeps the test about render and interop
// regressions; every other console error still fails the run.
const EXPECTED_CONSOLE = [/^\[Apex\] License is not valid for this domain/];
const isExpected = (text) => EXPECTED_CONSOLE.some(re => re.test(text));

const failures = [];
const launchOpts = channel ? { channel, headless: true } : { headless: true };
const browser = await chromium.launch(launchOpts);
const page = await browser.newPage({ viewport: { width: 1280, height: 900 } });

let pageErrors = [];
page.on('console', m => {
  if (m.type() !== 'error') return;
  const text = m.text().slice(0, 200);
  if (!isExpected(text)) pageErrors.push(text);
});
page.on('pageerror', e => pageErrors.push('PAGEERROR: ' + e.message.slice(0, 200)));

// Boot the home page: warms the WASM runtime and asserts the landing page loads clean.
pageErrors = [];
await page.goto(BASE + '/', { waitUntil: 'domcontentloaded' });
await page.waitForSelector('.sidebar', { timeout: 90000 });
await page.waitForTimeout(3000);
const bootErrUi = await page.evaluate(() => {
  const eu = document.querySelector('#blazor-error-ui');
  return eu ? getComputedStyle(eu).display !== 'none' : false;
});
if (pageErrors.length) failures.push(`[(home)] console/page errors: ${JSON.stringify(pageErrors)}`);
if (bootErrUi) failures.push('[(home)] Blazor error UI is visible');
console.log(`[(home)] booted errUi=${bootErrUi} errors=${pageErrors.length}`);

async function checkRoute(route) {
  pageErrors = [];
  const label = route || '(home)';
  await page.goto(BASE + '/' + route, { waitUntil: 'domcontentloaded' });
  await page.waitForSelector('.sidebar', { timeout: 30000 });
  try { await page.waitForSelector('section svg, .box svg', { timeout: 20000 }); } catch { /* asserted below */ }
  await page.waitForTimeout(1500);

  const st = await page.evaluate(() => {
    const eu = document.querySelector('#blazor-error-ui');
    return {
      errUiShown: eu ? getComputedStyle(eu).display !== 'none' : false,
      hasSvg: !!document.querySelector('section svg, .box svg'),
    };
  });

  if (pageErrors.length) failures.push(`[${label}] console/page errors: ${JSON.stringify(pageErrors)}`);
  if (st.errUiShown) failures.push(`[${label}] Blazor error UI is visible`);
  if (!st.hasSvg) failures.push(`[${label}] no tree SVG rendered`);
  console.log(`[${label}] svg=${st.hasSvg} errUi=${st.errUiShown} errors=${pageErrors.length}`);
}

for (const r of ROUTES) await checkRoute(r);

// Interaction: the selection event must round-trip JS -> [JSInvokable] -> C# and update the UI.
pageErrors = [];
await page.goto(BASE + '/selection', { waitUntil: 'domcontentloaded' });
await page.waitForSelector('section svg, .box svg', { timeout: 20000 });
await page.waitForTimeout(1500);
const selResult = await page.evaluate(async () => {
  const bt = window.blazorApextree;
  if (!bt || !bt.ChartReferences || bt.ChartReferences.size === 0) return { ok: false, reason: 'no chart reference' };
  const id = Array.from(bt.ChartReferences.keys())[0];
  const g = bt.ChartReferences.get(id);
  if (!g || typeof g.setSelection !== 'function') return { ok: false, reason: 'setSelection missing' };
  g.setSelection(['prod']);
  await new Promise(r => setTimeout(r, 600));
  return { ok: true, selection: g.getSelection() };
});
await page.waitForTimeout(800);
const notif = await page.locator('.notification').innerText().catch(() => '');
const notifClean = notif.replace(/\s+/g, ' ').trim();
const roundTripped = selResult.ok
  && Array.isArray(selResult.selection) && selResult.selection.includes('prod')
  && /prod/i.test(notifClean);
if (!roundTripped) {
  failures.push(`[selection] round-trip failed: ${JSON.stringify(selResult)} notification=${JSON.stringify(notifClean)}`);
}
console.log(`[selection] core=${JSON.stringify(selResult.selection || selResult.reason)} csharp=${JSON.stringify(notifClean)}`);

// Interaction: UpdateData must RECONCILE, not rebuild. The invariant the core actually guarantees is
// that a node whose CONTENT is unchanged keeps its own DOM element; a node whose content changed
// (here "eng" and "ceo" gain children, so they gain an expand button) has its content rebuilt inside
// the tree. So mark "ops", which is identical in Q1 and Q2. If the wrapper ever falls back to a
// rebuild or to Construct, every element is replaced and this catches it.
pageErrors = [];
await page.goto(BASE + '/live-data', { waitUntil: 'domcontentloaded' });
await page.waitForSelector('section svg, .box svg', { timeout: 20000 });
await page.waitForTimeout(1500);

const marked = await page.evaluate(() => {
  const el = document.querySelector('g[data-self="ops"]');
  if (!el) return false;
  el.setAttribute('data-smoke-marker', '1');
  return true;
});

// Q2 adds two engineers under the same "eng" id and hires a designer.
await page.getByRole('button', { name: 'Q2' }).click();
await page.waitForTimeout(2500);

const upd = await page.evaluate(() => ({
  survivorKeptElement: !!document.querySelector('g[data-self="ops"][data-smoke-marker="1"]'),
  hasNewHire: !!document.querySelector('g[data-self="design"]'),
  keptSubtree: !!document.querySelector('g[data-self="eng-1"]') && !!document.querySelector('g[data-self="eng-2"]'),
  nodeCount: document.querySelectorAll('g[data-self]').length,
}));

if (!marked) failures.push('[live-data] could not find the "ops" node before the update');
if (!upd.survivorKeptElement) failures.push('[live-data] UpdateData replaced an unchanged node instead of reconciling it');
if (!upd.hasNewHire) failures.push('[live-data] UpdateData did not add the new node');
if (!upd.keptSubtree) failures.push('[live-data] UpdateData did not splice in the new subtree');
if (pageErrors.length) failures.push(`[live-data] console/page errors: ${JSON.stringify(pageErrors)}`);
console.log(`[live-data] reconciled=${upd.survivorKeptElement} newNode=${upd.hasNewHire} subtree=${upd.keptSubtree} nodes=${upd.nodeCount} errors=${pageErrors.length}`);

await browser.close();

if (failures.length) {
  console.error('\nE2E SMOKE FAILED:\n' + failures.map(f => '  - ' + f).join('\n'));
  process.exit(1);
}
console.log('\nE2E smoke passed: all routes rendered and selection round-tripped.');
