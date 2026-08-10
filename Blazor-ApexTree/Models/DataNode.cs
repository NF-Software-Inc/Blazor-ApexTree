namespace ApexTree;

/// <summary>
/// Implementation of data node for name-URL pairs.
/// </summary>
public class ImageDataNode : DataNode<Image> { }

/// <summary>
/// Implementation of data node for simple string data.
/// </summary>
public class StringDataNode : DataNode<string?> { }

/// <summary>
/// Defines properties required for each node to display in the tree.
/// </summary>
public class DataNode<TItem>
{
	/// <summary>
	/// The HTML element id for this node.
	/// </summary>
	public string? Id { get; set; }

	/// <summary>
	/// The content to display in this node.
	/// </summary>
	public TItem? Data { get; set; }

	/// <summary>
	/// Configuration specific to this node.
	/// </summary>
	public DataNodeOptions? Options { get; set; }

	/// <summary>
	/// Items to display below this node.
	/// </summary>
	public List<DataNode<TItem>>? Children { get; set; }

	/// <summary>
	/// Marks this node as having children that are not loaded yet, so it renders an expand button
	/// even though <see cref="Children"/> is empty. Requires ApexTree core 2.0.0 or later.
	/// </summary>
	/// <remarks>
	/// The core's lazy-loading callback is a JavaScript function and is not exposed through this
	/// wrapper. Use this flag with the <c>OnNodeExpanded</c> event instead: mark the node, fetch the
	/// children in C# when it is expanded, then call
	/// <see cref="ApexTree{TItem}.UpdateData(DataNode{TItem})"/> with the filled-in tree. Because
	/// that reconciles rather than rebuilds, the subtree springs into place.
	/// </remarks>
	public bool? HasChildren { get; set; }

	/// <summary>
	/// Adds a child item to this node and returns it.
	/// </summary>
	/// <param name="data">The data value for the child.</param>
	public DataNode<TItem> AddChild(TItem data) => AddChild(null, data, null);

	/// <summary>
	/// Adds a child item to this node and returns it.
	/// </summary>
	/// <param name="id">The id value for the child.</param>
	/// <param name="data">The data value for the child.</param>
	public DataNode<TItem> AddChild(string id, TItem data) => AddChild(id, data, null);

	/// <summary>
	/// Adds a child item to this node and returns it.
	/// </summary>
	/// <param name="id">The id value for the child.</param>
	/// <param name="data">The data value for the child.</param>
	/// <param name="options">The options value for the child.</param>
	public DataNode<TItem> AddChild(string? id, TItem data, DataNodeOptions? options)
	{
		var child = new DataNode<TItem>() { Id = id, Data = data, Options = options };

		Children ??= [];
		Children.Add(child);

		return child;
	}

	/// <summary>
	/// Adds a collection of child items to this node and returns them.
	/// </summary>
	/// <param name="data">The child items to add.</param>
	public IEnumerable<DataNode<TItem>> AddChild(IEnumerable<TItem> data) => AddChild(data, null);

	/// <summary>
	/// Adds a collection of child items to this node and returns them.
	/// </summary>
	/// <param name="data">The child items to add.</param>
	/// <param name="options">The options value for the children.</param>
	public IEnumerable<DataNode<TItem>> AddChild(IEnumerable<TItem> data, DataNodeOptions? options)
	{
		var children = data.Select(x => new DataNode<TItem>() { Data = x, Options = options });

		Children ??= [];
		Children.AddRange(children);

		return children;
	}
}
