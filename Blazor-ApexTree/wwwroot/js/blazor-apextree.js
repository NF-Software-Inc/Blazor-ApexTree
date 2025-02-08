import ApexTree from './apextree.esm.js?ver=1.3.0'

/**
 * Export function for Blazor to point to the window.blazor_apextree.
 * @returns To be compatible with the most JS Interop calls the window will be returned.
 */
export function get_apextree() {
    window.ApexTree = ApexTree
    return window;
}

window.blazor_apextree = {
}
