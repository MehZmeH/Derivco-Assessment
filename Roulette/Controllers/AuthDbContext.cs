using Roulette.models;
using Microsoft.EntityFrameworkCore;

namespace Roulette.Db;

public class AuthDbContext : DbContext
{
    public DbSet<Bet> Bets { get; set; }
    public DbSet<RouletteTable> RouletteTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=./games_table.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bet>().ToTable("Bets");
        modelBuilder.Entity<RouletteTable>().ToTable("RouletteTables");

        modelBuilder.Entity<RouletteTable>().HasData(new RouletteTable { ID = 1, SpinIteration = 1 });
    }
}