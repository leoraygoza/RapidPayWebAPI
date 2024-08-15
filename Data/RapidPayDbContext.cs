using Microsoft.EntityFrameworkCore;
using RapidPayWebAPI.Models;

namespace RapidPayWebAPI.Data;

public class RapidPayDbContext : DbContext
{
    public RapidPayDbContext(DbContextOptions<RapidPayDbContext> options) : base(options) { }

    public DbSet<Card> Cards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>().HasData(
            new Card
            {
                CardNumber = "123456789012345",
                CardHolderName = "Homer Simpson",
                ExpirationDate = new DateTime(2023, 12, 31),
                Balance = 1000,
                CVV = "123"
            },
            new Card
            {
                CardNumber = "654321098765432",
                CardHolderName = "Dak Prescott",
                ExpirationDate = new DateTime(2024, 12, 31),
                Balance = 2000,
                CVV = "321"
            }
        );
    }
}