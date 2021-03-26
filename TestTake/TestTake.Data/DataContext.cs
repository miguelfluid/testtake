using System;
using Microsoft.EntityFrameworkCore;
using TestTake.Core.Business;

namespace TestTake.Data
{
  public class DataContext : DbContext
  {
    private readonly string _connectionString;
    public DataContext(string connectionString)
    {
      _connectionString = connectionString;

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer(_connectionString);
      }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

      builder.Entity<User>().ToTable("Users");
      builder.Entity<Chat>().ToTable("Chat");
      builder.Entity<Room>().ToTable("Room");

      builder.Entity<User>().HasKey(x => x.Id);
      builder.Entity<Chat>().HasKey(x => x.Id);
      builder.Entity<Room>().HasKey(x => x.Id);


      base.OnModelCreating(builder);
    }
  }
}