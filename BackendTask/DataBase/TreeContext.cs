using BackendTask.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Exception = BackendTask.DataBase.Models.Exception;

namespace BackendTask.DataBase;

internal class TreeContext : DbContext
{
    public DbSet<TreeNode> Nodes { get; set; }
    public DbSet<ExceptionData> ExceptionData { get; set; }
    public DbSet<Exception> Exceptions { get; set; }

    public TreeContext(DbContextOptions<TreeContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TreeNode>(entity =>
        {
            entity.
                HasIndex(p => new {p.Name , p.ParentNodeId}).IsUnique();
        });
    }
}