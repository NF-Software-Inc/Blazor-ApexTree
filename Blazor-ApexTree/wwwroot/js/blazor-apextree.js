import ApexTree from './apextree.esm.js?ver=1.3.0'

/**
 * Export function for Blazor to point to the window.blazor_apextree.
 * @returns To be compatible with the most JS Interop calls the window will be returned.
 */
export function GetApexTree() {
    window.ApexTree = ApexTree
    return window;
}

window.blazorApextree = {
    /**
     * Stores references to each chart.
     */
    ChartReferences: new Map(),

    /**
     * Creates an Apex Tree chart on the specified element.
     * @param {any} container A reference to the HTML element to create the chart on.
     * @param {any} options The serialized options to use for the chart.
     * @param {any} data The serialized objects to use in the chart.
     */
    CreateChart: function (container, options, data) {
        var parsed = this.Deserialize(options);

        if (parsed.debug === true)
            console.log(parsed);

        var tree = new ApexTree(container, parsed);
        var graph = tree.render(this.Deserialize(data));

        this.ChartReferences.set(parsed.id, graph);

        if (parsed.debug === true) {
            console.log(`Chart ${parsed.id} created.`);
        }
    },

    /**
     * Removes the chart from the collection.
     * @param {string} id The HTML id of the element the chart is attached to.
     */
    DeleteChart: function (id) {
        if (this.ChartReferences.has(id))
            this.ChartReferences.delete(id);
    },

    /**
     * Collapses the specified node.
     * @param {string} id The HTML id of the element the chart is attached to.
     * @param {string} nodeId The HTML id of the node to collapse.
     */
    CollapseNode: function (id, nodeId) {
        if (this.ChartReferences.has(id) === false)
            return;

        var graph = this.ChartReferences.get(id);

        graph.collapse(nodeId);
    },

    /**
     * Expands the specified node.
     * @param {string} id The HTML id of the element the chart is attached to.
     * @param {string} nodeId The HTML id of the node to expand.
     */
    ExpandNode: function (id, nodeId) {
        if (this.ChartReferences.has(id) === false)
            return;

        var graph = this.ChartReferences.get(id);

        graph.expand(nodeId);
    },

    /**
     * Changes the layout of the chart.
     * @param {string} id The HTML id of the element the chart is attached to.
     * @param {string} direction The updated direction of the layout to apply.
     */
    ChangeLayout: function (id, direction) {
        if (this.ChartReferences.has(id) === false)
            return;

        var graph = this.ChartReferences.get(id);

        graph.changeLayout(JSON.parse(direction));
    },

    /**
     * Updates the chart to fit to the current viewport.
     * @param {string} id The HTML id of the element the chart is attached to.
     */
    FitScreen: function (id) {
        if (this.ChartReferences.has(id) === false)
            return;

        var graph = this.ChartReferences.get(id);

        graph.fitScreen();
    },

    /**
     * Rerenders the chart.
     * @param {string} id The HTML id of the element the chart is attached to.
     * @param {boolean} keepOldPosition Undocumented.
     */
    Render: function (id, keepOldPosition) {
        if (this.ChartReferences.has(id) === false)
            return;

        var graph = this.ChartReferences.get(id);

        graph.render(keepOldPosition);
    },

    /**
     * Converts the provided JSON options into an object.
     * @param {any} options The options to deserialize.
     */
    Deserialize: function (options) {
        return JSON.parse(options, (key, value) => {
            if (typeof (value) !== 'undefined' && value !== null && typeof (value) === 'object' && '@eval' in value) {
                return eval?.("'use strict'; (" + value['@eval'] + ")");
            }
            else {
                return value;
            }
        });
    }
}
