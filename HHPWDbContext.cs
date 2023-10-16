using Microsoft.EntityFrameworkCore;
using HHPW_Bangazon_BE.Models;

public class HHPWDbContext : DbContext
    {
    public DbSet<Items> Items { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<Users> Users { get; set; }

    public HHPWDbContext(DbContextOptions<HHPWDbContext> context) : base(context) 
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
