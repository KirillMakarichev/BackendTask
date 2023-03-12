namespace BackendTask.Models.Entities;

internal class TreeNode
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<TreeNode> Children { get; set; }
}