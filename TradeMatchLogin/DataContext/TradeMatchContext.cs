using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TradeMatchLogin.Models;

namespace TradeMatchLogin.DataContext;

public class TradeMatchContext : DbContext
{
    public TradeMatchContext (DbContextOptions<TradeMatchContext> options) : base(options)
    { }

    public DbSet<User> User { get; set; }
    public DbSet<Login> Login { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Address> Address { get; set; }


    // Fluent-API.
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
     
       
    //}
}
