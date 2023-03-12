using BackendTask.Models.Entities;
using TreeNode = BackendTask.DataBase.Models.TreeNode;

namespace BackendTask.Providers.Interfaces;

internal interface ITreeProvider
{
    Task<TreeNode> GetOrCreateAsync(string name);
    Task<ProcessingResponse> CreateNodeAsync(string treeName, long parentNodeId, string nodeName);
    Task<ProcessingResponse> RenameNodeAsync(string treeName, long nodeId, string newNodeName);
    Task<ProcessingResponse> DeleteNodeAsync(string treeName, long nodeId);
}