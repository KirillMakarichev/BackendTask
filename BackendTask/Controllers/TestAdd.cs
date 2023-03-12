using BackendTask.DataBase;
using BackendTask.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTask.Controllers;

internal class TestAdd : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public TestAdd(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scope = _serviceProvider.CreateScope();
        var treeContext = scope.ServiceProvider.GetService<TreeContext>();

        var search = "1";
        
        var tree = (await treeContext.Nodes
            .Where(x => x.RootName == search)
            .ToListAsync()).FirstOrDefault(x => x.ParentNodeId == null);

        Console.WriteLine();
        
        // var tree = new TreeNode()
        // {
        //     Name = "1"
        // };
        // treeContext.Nodes.Add(tree);
        //
        // await treeContext.SaveChangesAsync();
        //
        // var node1 = new TreeNode()
        // {
        //     Name = "2",
        //     ParentNodeId = tree.Id
        // };
        //
        // var node2 = new TreeNode()
        // {
        //     Name = "3",
        //     ParentNodeId = tree.Id
        // };
        //
        // treeContext.Nodes.Add(node1);
        // treeContext.Nodes.Add(node2);
        //
        // await treeContext.SaveChangesAsync();
        //
        // var node3 = new TreeNode()
        // {
        //     Name = "4",
        //     ParentNodeId = node1.Id
        // };
        //
        // treeContext.Nodes.Add(node3);
        // await treeContext.SaveChangesAsync();
    }
}