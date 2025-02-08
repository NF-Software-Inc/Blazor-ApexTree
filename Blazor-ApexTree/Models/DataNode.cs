namespace ApexTree;

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
}
