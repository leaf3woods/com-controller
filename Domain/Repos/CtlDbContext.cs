using Domain.Repos.Model;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repos
{
    public class CtlDbContext : DbContext
    {
        /// <summary>
        ///     操作记录
        /// </summary>
        public DbSet<OperationLog> OperationLogs { get; set; } = null!;

        /// <summary>
        ///     设备
        /// </summary>
        public DbSet<Device> Devices { get; set; } = null!;

        public CtlDbContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=controller.db; Mode=ReadWrite;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}