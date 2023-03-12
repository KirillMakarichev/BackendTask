﻿using BackendTask.DataBase;
using BackendTask.DataBase.Models;
using BackendTask.Providers.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> CreateNodeAsync(string treeName, long parentNodeId, string nodeName)
    {
        var node =
            await _treeContext.Nodes
                .Include(x => x.Children)
                .FirstOrDefaultAsync(x => x.RootName == treeName && x.Id == parentNodeId);

        if (node == null)
            return false;

        if (node.Children.Any(x => x.Name == nodeName))
            return false;
        
        _treeContext.Nodes.Add(new TreeNode()
        {
            Name = nodeName,
            ParentNodeId = parentNodeId,
            RootName = treeName
        });

        await _treeContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RenameNodeAsync(string treeName, long nodeId, string newNodeName)
    {
        var node =
            await _treeContext.Nodes.FirstOrDefaultAsync(x => x.RootName == treeName && x.Id == nodeId);
        
        if (node == null)
            return false;

        if (node.ParentNodeId == null)
            return false;

        node.Name = newNodeName;

        await _treeContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteNodeAsync(string treeName, long nodeId)
    {
        var node =
            await _treeContext.Nodes
                .Include(x => x.Children)
                .FirstOrDefaultAsync(x => x.RootName == treeName && x.Id == nodeId);
        
        if (node == null)
            return false;

        if (node.Children.Any())
            return false;

        _treeContext.Nodes.Remove(node);

        await _treeContext.SaveChangesAsync();

        return true;
    }
}