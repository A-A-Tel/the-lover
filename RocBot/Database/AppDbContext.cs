using Microsoft.EntityFrameworkCore;
using RocBot.Database.Entities;

namespace RocBot.Database;

public class AppDbContext : DbContext
{
    public DbSet<LoveAction> LoveActions { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        const string relativePath = "Database/database.sqlite";

        if (!File.Exists(relativePath))
            using (File.Create(relativePath)) { }

        options.UseSqlite("Data Source=" + relativePath);
    }
}