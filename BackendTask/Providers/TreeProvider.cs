using BackendTask.DataBase;
using BackendTask.Models.Entities;
using BackendTask.Providers.Interfaces;
using Microsoft.EntityFrameworkCore;
using TreeNode = BackendTask.DataBase.Models.TreeNode;

namespace BackendTask.Providers;

internal class TreeProvider : ITreeProvider
{
    private readonly TreeContext _treeContext;

    public TreeProvider(TreeContext treeContext)
    {
        _treeContext = treeContext;
    }

    public async Task<TreeNode> GetOrCreateAsync(string name)
    {
        var tree = (await _treeContext.Nodes
            .Where(x => x.RootName == name)
            .ToListAsync()).FirstOrDefault(x => x.ParentNodeId == null);

        if (tree != null) return tree;

        tree = new TreeNode()
        {
            Name = name,
            RootName = name,
            ParentNodeId = null,
            ParentNode = null
        };

        _treeContext.Nodes.Add(tree);
        await _treeContext.SaveChangesAsync();

        return tree;
    }

    public async Task<ProcessingResponse> CreateNodeAsync(string treeName, long parentNodeId, string nodeName)
    {
        var node =
            await _treeContext.Nodes
                .Include(x => x.Children)
                .FirstOrDefaultAsync(x => x.Id == parentNodeId);

        if (node == null)
            return new ProcessingResponse($"Node with ID = {parentNodeId} was not found");

        if (node.RootName != treeName)
            return new ProcessingResponse("Requested node was found, but it doesn't belong your tree");
        
        if (node.Children.Any(x => x.Name == nodeName))
            return new ProcessingResponse("Duplicate name");
        
        _treeContext.Nodes.Add(new TreeNode()
        {
            Name = nodeName,
            ParentNodeId = parentNodeId,
            RootName = treeName
        });

        await _treeContext.SaveChangesAsync();
        return new ProcessingResponse();
    }

    public async Task<ProcessingResponse> RenameNodeAsync(string treeName, long nodeId, string newNodeName)
    {
        var node =
            await _treeContext.Nodes
                .FirstOrDefaultAsync(x => x.Id == nodeId);

        if (node == null)
            return new ProcessingResponse($"Node with ID = {nodeId} was not found");

        if (node.RootName != treeName)
            return new ProcessingResponse("Requested node was found, but it doesn't belong your tree");
        
        if (node.ParentNodeId == null)
            return new ProcessingResponse("Couldn't rename root node");

        node.Name = newNodeName;

        await _treeContext.SaveChangesAsync();

        return new ProcessingResponse();
    }

    public async Task<ProcessingResponse> DeleteNodeAsync(string treeName, long nodeId)
    {
        var node =
            await _treeContext.Nodes
                .Include(x => x.Children)
                .FirstOrDefaultAsync(x => x.Id == nodeId);

        if (node == null)
            return new ProcessingResponse($"Node with ID = {nodeId} was not found");

        if (node.RootName != treeName)
            return new ProcessingResponse("Requested node was found, but it doesn't belong your tree");
        
        if (node.Children.Any())
            return new ProcessingResponse("You have to delete all children nodes first");

        _treeContext.Nodes.Remove(node);

        await _treeContext.SaveChangesAsync();

        return new ProcessingResponse();
    }
}