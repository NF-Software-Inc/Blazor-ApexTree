import ApexTree from "./apextree.esm.js?ver=1.3.0";

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
    var graph = tree.render(data);

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
    }

    if (parsed.debug === true) {
      console.log(`Chart ${id} created.`);
    }
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
