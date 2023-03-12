using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTask.Models.Entities;

[Table("nodes")]
internal class TreeNode
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<TreeNode> Children { get; set; }
    public long? ParentNodeId { get; set; }
    public TreeNode? ParentNode { get; set; }
    public string RootName { get; set; }
}