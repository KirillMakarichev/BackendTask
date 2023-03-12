using BackendTask.DataBase.Models;

namespace BackendTask.Providers.Interfaces;

internal interface ITreeProvider
{
    Task<TreeNode> GetOrCreateAsync(string name);
    Task<bool> CreateNodeAsync(string treeName, long parentNodeId, string nodeName);
}