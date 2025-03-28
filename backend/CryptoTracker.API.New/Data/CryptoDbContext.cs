using Microsoft.EntityFrameworkCore;
using CryptoTracker.API.New.Models;
using System;

namespace CryptoTracker.API.New.Data
{
    public class CryptoDbContext : DbContext
    {
        public CryptoDbContext(DbContextOptions<CryptoDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Cryptocurrency> Cryptocurrencies { get; set; }
        public DbSet<PriceDataPoint> PriceDataPoints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<PriceDataPoint>()
                .HasOne(p => p.Cryptocurrency)
                .WithMany(c => c.PriceHistory)
                .HasForeignKey(p => p.CryptocurrencyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Add index for faster querying of price history
            modelBuilder.Entity<PriceDataPoint>()
                .HasIndex(p => new { p.CryptocurrencyId, p.Timestamp });

            // Add seed data for popular cryptocurrencies
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cryptocurrency>().HasData(
                new Cryptocurrency
                {
                    Id = 1,
                    Symbol = "btc",
                    Name = "Bitcoin",
                    CurrentPrice = 53000.00m,
                    MarketCap = 1028502937621m,
                    Volume24h = 31578397452m,
                    CirculatingSupply = 19401525m,
                    PriceChangePercentage24h = 2.34m,
                    ImageUrl = "https://assets.coingecko.com/coins/images/1/large/bitcoin.png"
                },
                new Cryptocurrency
                {
                    Id = 2,
                    Symbol = "eth",
                    Name = "Ethereum",
                    CurrentPrice = 2950.15m,
                    MarketCap = 354021576329m,
                    Volume24h = 14935903245m,
                    CirculatingSupply = 120105764m,
                    PriceChangePercentage24h = 3.56m,
                    ImageUrl = "https://assets.coingecko.com/coins/images/279/large/ethereum.png"
                },
                new Cryptocurrency
                {
                    Id = 3,
                    Symbol = "usdt",
                    Name = "Tether",
                    CurrentPrice = 1.00m,
                    MarketCap = 97944489305m,
                    Volume24h = 83140364432m,
                    CirculatingSupply = 97881458867m,
                    PriceChangePercentage24h = 0.01m,
                    ImageUrl = "https://assets.coingecko.com/coins/images/325/large/Tether.png"
                },
                new Cryptocurrency
                {
                    Id = 4,
                    Symbol = "bnb",
                    Name = "BNB",
                    CurrentPrice = 605.73m,
                    MarketCap = 95276514971m,
                    Volume24h = 1904643035m,
                    CirculatingSupply = 157204722m,
                    PriceChangePercentage24h = 0.92m,
                    ImageUrl = "https://assets.coingecko.com/coins/images/825/large/bnb-icon2_2x.png"
                },
                new Cryptocurrency
                {
                    Id = 5,
                    Symbol = "sol",
                    Name = "Solana",
                    CurrentPrice = 151.12m,
                    MarketCap = 75234781241m,
                    Volume24h = 2718456192m,
                    CirculatingSupply = 498073742m,
                    PriceChangePercentage24h = 4.27m,
                    ImageUrl = "https://assets.coingecko.com/coins/images/4128/large/solana.png"
                }
            );

            // Replace DateTime.UtcNow with a fixed date
            DateTime seedDate = new DateTime(2025, 2, 12, 17, 42, 4, 257, DateTimeKind.Utc);
            
            // Use this fixed date for all your seed data instead of DateTime.UtcNow
            // For example:
            
            // Seed Bitcoin price data for the last 30 days
            decimal bitcoinBasePrice = 50000m;
            Random rand = new Random(42); // Use fixed seed for reproducible "random" numbers
            for (int i = 30; i >= 0; i--)
            {
                // Add some randomness to the price for demonstration
                decimal fluctuation = (decimal)((rand.NextDouble() - 0.5) * 0.05); // ±2.5% daily fluctuation
                decimal price = bitcoinBasePrice * (1 + fluctuation);
                bitcoinBasePrice = price; // Use each day's price as the base for the next

                modelBuilder.Entity<PriceDataPoint>().HasData(
                    new PriceDataPoint
                    {
                        Id = i + 1,
                        CryptocurrencyId = 1, // Bitcoin
                        Timestamp = seedDate.AddDays(-i), // Use the fixed date
                        Price = Math.Round(price, 2),
                        Volume = 30000000000m + (decimal)(rand.NextDouble() * 4000000000),
                        MarketCap = Math.Round(price * 19000000m, 0) // Approximate circulating supply
                    }
                );
            }

            // Seed Ethereum price data (starting from ID = 32)
            decimal ethBasePrice = 2900m;
            for (int i = 30; i >= 0; i--)
            {
                decimal fluctuation = (decimal)((rand.NextDouble() - 0.5) * 0.06); // ±3% daily fluctuation
                decimal price = ethBasePrice * (1 + fluctuation);
                ethBasePrice = price;

                modelBuilder.Entity<PriceDataPoint>().HasData(
                    new PriceDataPoint
                    {
                        Id = i + 32,
                        CryptocurrencyId = 2, // Ethereum
                        Timestamp = seedDate.AddDays(-i), // Use the fixed date
                        Price = Math.Round(price, 2),
                        Volume = 15000000000m + (decimal)(rand.NextDouble() * 2000000000),
                        MarketCap = Math.Round(price * 120000000m, 0)
                    }
                );
            }
        }
    }
}