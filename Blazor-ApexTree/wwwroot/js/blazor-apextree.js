import ApexTree from "./apextree.esm.js?ver=2.0.0";

/**
 * Export function for Blazor to point to the window.blazor_apextree.
 * @returns To be compatible with the most JS Interop calls the window will be returned.
 */
export function GetApexTree() {
  window.ApexTree = ApexTree;
  return window;
}

window.blazorApextree = {
  /**
   * Stores references to each chart.
   */
  ChartReferences: new Map(),

  /**
   * Stores .NET object references for callbacks.
   */
  DotNetReferences: new Map(),

  /**
   * Sets the ApexTree license key.
   * @param {string} licenseKey The commercial license key.
   */
  SetLicense: function (licenseKey) {
    try {
      if (typeof ApexTree !== "undefined" && ApexTree.setLicense) {
        ApexTree.setLicense(licenseKey);
        return true;
      }
      console.warn("ApexTree.setLicense not available");
      return false;
    } catch (error) {
      console.error("Error setting license:", error);
      return false;
    }
  },

  /**
   * Creates an Apex Tree chart on the specified element.
   * @param {any} container A reference to the HTML element to create the chart on.
   * @param {string} id The HTML id of the element.
   * @param {any} options The serialized options to use for the chart.
   * @param {any} data The serialized objects to use in the chart.
   * @param {any} dotNetRef The .NET object reference for callbacks.
   */
  CreateChart: function (container, id, options, data, dotNetRef) {
    var parsed = this.Deserialize(options);

    if (parsed.debug === true) console.log(parsed);

    if (dotNetRef) {
      // Use library's built-in onNodeClick callback for reliable node ID detection
      parsed.onNodeClick = function (nodeData) {
        var nodeId = "";
        if (typeof nodeData === "string") {
          nodeId = nodeData;
        } else if (nodeData && typeof nodeData === "object") {
          nodeId = nodeData.id || "";
        }
        dotNetRef.invokeMethodAsync("OnNodeClicked", nodeId);
      };
    }

    var tree = new ApexTree(container, parsed);
    var graph = tree.render(window.blazorApextree.AsData(data));

    this.ChartReferences.set(id, graph);

    if (dotNetRef) {
      this.DotNetReferences.set(id, dotNetRef);

      var lastHoveredId = "";
      container.addEventListener("mouseover", function (event) {
        var el = event.target;
        while (el && el !== container) {
          if (el.dataset && el.dataset.self) {
            var nodeId = el.dataset.self;
            if (nodeId !== lastHoveredId) {
              lastHoveredId = nodeId;
              dotNetRef.invokeMethodAsync("OnNodeHovered", nodeId);
            }
            return;
          }
          el = el.parentNode;
        }
      });

      container.addEventListener("mouseleave", function () {
        lastHoveredId = "";
      });

      // Wrap expand/collapse methods to fire events when nodes are toggled
      var originalCollapse = graph.collapse.bind(graph);
      graph.collapse = function (nodeId) {
        var result = originalCollapse(nodeId);
        dotNetRef.invokeMethodAsync("OnNodeCollapsed", nodeId);
        return result;
      };

      var originalExpand = graph.expand.bind(graph);
      graph.expand = function (nodeId) {
        var result = originalExpand(nodeId);
        dotNetRef.invokeMethodAsync("OnNodeExpanded", nodeId);
        return result;
      };

      // Selection change (active only when enableSelection is set)
      if (typeof graph.onSelectionChange === "function") {
        graph.onSelectionChange(function (selection) {
          dotNetRef.invokeMethodAsync("OnSelectionChanged", window.blazorApextree.NormalizeSelection(selection));
        });
      }
    }

    if (parsed.debug === true) {
      console.log(`Chart ${id} created.`);
    }
  },

  /**
   * Normalizes a selection payload from the core library into a flat array of node id strings.
   * @param {any} selection The raw selection (array of ids, array of node objects, Set, or single value).
   * @returns {string[]} The selected node ids.
   */
  NormalizeSelection: function (selection) {
    if (selection == null) return [];
    var items = Array.isArray(selection)
      ? selection
      : typeof selection.forEach === "function"
        ? Array.from(selection)
        : [selection];
    return items
      .map(function (item) {
        if (typeof item === "string") return item;
        if (item && typeof item === "object") return item.id || "";
        return "";
      })
      .filter(function (id) { return id !== ""; });
  },

  /**
   * Resets the pan/zoom baseline to the current viewBox.
   * @param {string} id The HTML id of the element the chart is attached to.
   */
  ResetPanZoomBase: function (id) {
    if (this.ChartReferences.has(id) === false) return;

    var graph = this.ChartReferences.get(id);

    if (typeof graph.resetPanZoomBase === "function") graph.resetPanZoomBase();
  },

  /**
   * Removes the chart from the collection.
   * @param {string} id The HTML id of the element the chart is attached to.
   */
  DeleteChart: function (id) {
    var element = document.getElementById(id);

    if (typeof element !== "undefined") element.replaceChildren();

    if (this.ChartReferences.has(id)) this.ChartReferences.delete(id);

    if (this.DotNetReferences.has(id)) this.DotNetReferences.delete(id);
  },

  /**
   * Collapses the specified node.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {string} nodeId The HTML id of the node to collapse.
   */
  CollapseNode: function (id, nodeId) {
    if (this.ChartReferences.has(id) === false) return;

    var graph = this.ChartReferences.get(id);

    graph.collapse(nodeId);
  },

  /**
   * Expands the specified node.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {string} nodeId The HTML id of the node to expand.
   */
  ExpandNode: function (id, nodeId) {
    if (this.ChartReferences.has(id) === false) return;

    var graph = this.ChartReferences.get(id);

    graph.expand(nodeId);
  },

  /**
   * Changes the layout of the chart.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {string} direction The updated direction of the layout to apply.
   */
  ChangeLayout: function (id, direction) {
    if (this.ChartReferences.has(id) === false) return;

    var graph = this.ChartReferences.get(id);

    graph.changeLayout(JSON.parse(direction));
  },

  /**
   * Updates the chart to fit to the current viewport.
   * @param {string} id The HTML id of the element the chart is attached to.
   */
  FitScreen: function (id) {
    if (this.ChartReferences.has(id) === false) return;

    var graph = this.ChartReferences.get(id);

    graph.fitScreen();
  },

  /**
   * Rerenders the chart.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {boolean} keepOldPosition Undocumented.
   */
  Render: function (id, keepOldPosition) {
    if (this.ChartReferences.has(id) === false) return;

    var graph = this.ChartReferences.get(id);

    graph.render(keepOldPosition);
  },

  /**
   * Zooms the chart by the specified factor.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {number} factor The zoom factor (e.g. 1.2 to zoom in, 0.8 to zoom out).
   */
  Zoom: function (id, factor) {
    if (this.ChartReferences.has(id) === false) return;

    var graph = this.ChartReferences.get(id);

    graph.zoom(factor);
  },

  /**
   * Downloads the current tree as an SVG file.
   * @param {string} id The HTML id of the element the chart is attached to.
   */
  ExportToSvg: function (id) {
    if (this.ChartReferences.has(id) === false) return;

    var graph = this.ChartReferences.get(id);

    graph.exportToSvg();
  },

  /**
   * Replaces the tree data and re-renders without recreating the ApexTree instance.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {any} data The new serialized tree data.
   */
  Construct: function (id, data) {
    if (this.ChartReferences.has(id) === false) return;

    var graph = this.ChartReferences.get(id);

    graph.construct(this.AsData(data));
  },

  /**
   * Normalizes tree data arriving from .NET into an object.
   *
   * The C# side is not consistent about this: the initial CreateChart passes the node graph as an
   * object (Blazor serializes it), while Construct/RebuildChart/UpdateData hand over a JSON string
   * produced by ChartSerializer so the wrapper controls naming and the "@eval" convention. Passing
   * a raw string through to the core silently does nothing, because it looks for an .id property.
   * @param {any} data Either a JSON string or an already-parsed object.
   */
  AsData: function (data) {
    return typeof data === "string" ? this.Deserialize(data) : data;
  },

  /**
   * Returns the graph for an id, or null when the chart is gone or the core is too old to have
   * the requested method. Keeps every call below a one-liner and makes a missing method a no-op
   * rather than a TypeError, which matters because the bundled core can be newer than a host
   * page's cached copy.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {string} method The graph method about to be called.
   */
  GraphFor: function (id, method) {
    if (this.ChartReferences.has(id) === false) return null;

    var graph = this.ChartReferences.get(id);

    if (typeof graph[method] !== "function") {
      console.warn(`ApexTree: graph.${method} is unavailable in the loaded core.`);
      return null;
    }

    return graph;
  },

  /**
   * Diffs the tree against a new dataset and animates the difference: surviving nodes spring to
   * their new positions, new ids grow in, departed ones retract. Collapse state, selection, focus
   * and expanded cards all survive. Prefer this over Construct, which rebuilds.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {any} data The new serialized tree data.
   */
  UpdateData: function (id, data) {
    var graph = this.GraphFor(id, "updateData");
    if (graph) graph.updateData(this.AsData(data));
  },

  /**
   * Expands every node in the tree.
   * @param {string} id The HTML id of the element the chart is attached to.
   */
  ExpandAll: function (id) {
    var graph = this.GraphFor(id, "expandAll");
    if (graph) graph.expandAll();
  },

  /**
   * Collapses every node in the tree.
   * @param {string} id The HTML id of the element the chart is attached to.
   */
  CollapseAll: function (id) {
    var graph = this.GraphFor(id, "collapseAll");
    if (graph) graph.collapseAll();
  },

  /**
   * Expands the tree down to the given depth and collapses everything deeper.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {number} depth The depth to expand to, with the root at 0.
   */
  ExpandToDepth: function (id, depth) {
    var graph = this.GraphFor(id, "expandToDepth");
    if (graph) graph.expandToDepth(depth);
  },

  /**
   * Expands a node and everything beneath it.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {string} nodeId The node to expand.
   */
  ExpandSubtree: function (id, nodeId) {
    var graph = this.GraphFor(id, "expandSubtree");
    if (graph) graph.expandSubtree(nodeId);
  },

  /**
   * Collapses a node and everything beneath it.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {string} nodeId The node to collapse.
   */
  CollapseSubtree: function (id, nodeId) {
    var graph = this.GraphFor(id, "collapseSubtree");
    if (graph) graph.collapseSubtree(nodeId);
  },

  /**
   * Spotlights a node: dims everything outside its lineage and visible subtree, and frames it.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {string} nodeId The node to spotlight.
   */
  Focus: function (id, nodeId) {
    var graph = this.GraphFor(id, "focus");
    if (graph) graph.focus(nodeId);
  },

  /**
   * Clears the spotlight and restores the full view.
   * @param {string} id The HTML id of the element the chart is attached to.
   */
  ClearFocus: function (id) {
    var graph = this.GraphFor(id, "clearFocus");
    if (graph) graph.clearFocus();
  },

  /**
   * Returns the spotlighted node id, or null when nothing is focused.
   * @param {string} id The HTML id of the element the chart is attached to.
   */
  GetFocusedNodeId: function (id) {
    var graph = this.GraphFor(id, "getFocusedNodeId");
    return graph ? (graph.getFocusedNodeId() ?? null) : null;
  },

  /**
   * Flows an animated dash along the edges from the root to each of the given nodes.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {string[]} nodeIds The target nodes.
   */
  SetActivePath: function (id, nodeIds) {
    var graph = this.GraphFor(id, "setActivePath");
    if (graph) graph.setActivePath(nodeIds);
  },

  /**
   * Clears the active path.
   * @param {string} id The HTML id of the element the chart is attached to.
   */
  ClearActivePath: function (id) {
    var graph = this.GraphFor(id, "clearActivePath");
    if (graph) graph.clearActivePath();
  },

  /**
   * Returns the node ids currently on the active path.
   * @param {string} id The HTML id of the element the chart is attached to.
   */
  GetActivePath: function (id) {
    var graph = this.GraphFor(id, "getActivePath");
    return graph ? (graph.getActivePath() ?? []) : [];
  },

  /**
   * Expands a node's card in place to reveal its detail section. Distinct from expanding children.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {string} nodeId The node whose card to expand.
   */
  ExpandCard: function (id, nodeId) {
    var graph = this.GraphFor(id, "expandCard");
    if (graph) graph.expandCard(nodeId);
  },

  /**
   * Collapses a node's card back to its summary.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {string} nodeId The node whose card to collapse.
   */
  CollapseCard: function (id, nodeId) {
    var graph = this.GraphFor(id, "collapseCard");
    if (graph) graph.collapseCard(nodeId);
  },

  /**
   * Toggles a node's card between summary and detail.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {string} nodeId The node whose card to toggle.
   */
  ToggleCard: function (id, nodeId) {
    var graph = this.GraphFor(id, "toggleCard");
    if (graph) graph.toggleCard(nodeId);
  },

  /**
   * Replaces the set of expanded cards in one pass.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {string[]} nodeIds The nodes whose cards should be expanded.
   */
  SetExpandedCards: function (id, nodeIds) {
    var graph = this.GraphFor(id, "setExpandedCards");
    if (graph) graph.setExpandedCards(nodeIds);
  },

  /**
   * Returns the ids of the nodes whose cards are expanded.
   * @param {string} id The HTML id of the element the chart is attached to.
   */
  GetExpandedCards: function (id) {
    var graph = this.GraphFor(id, "getExpandedCards");
    return graph ? (graph.getExpandedCards() ?? []) : [];
  },

  /**
   * Centres the camera on a node, keeping the current zoom.
   * @param {string} id The HTML id of the element the chart is attached to.
   * @param {string} nodeId The node to centre on.
   */
  CenterOnNode: function (id, nodeId) {
    var graph = this.GraphFor(id, "centerOnNode");
    if (graph) graph.centerOnNode(nodeId);
  },

  /**
   * Converts the provided JSON options into an object.
   * @param {any} options The options to deserialize.
   */
  Deserialize: function (options) {
    return JSON.parse(options, (key, value) => {
      if (
        typeof value !== "undefined" &&
        value !== null &&
        typeof value === "object" &&
        "@eval" in value
      ) {
        return eval?.("'use strict'; (" + value["@eval"] + ")");
      } else {
        return value;
      }
    });
  },
};
