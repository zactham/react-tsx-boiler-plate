using CryptoTracker.API.New.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoTracker.API.New.Data.Repositories
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly CryptoDbContext _context;

        public CryptoRepository(CryptoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cryptocurrency>> GetAllCryptocurrenciesAsync()
        {
            return await _context.Cryptocurrencies
                .OrderByDescending(c => c.MarketCap)
                .ToListAsync();
        }

        public async Task<Cryptocurrency?> GetCryptocurrencyByIdAsync(int id)
        {
            return await _context.Cryptocurrencies
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cryptocurrency?> GetCryptocurrencyBySymbolAsync(string symbol)
        {
            return await _context.Cryptocurrencies
                .FirstOrDefaultAsync(c => c.Symbol.ToLower() == symbol.ToLower());
        }

        public async Task<IEnumerable<PriceDataPoint>> GetPriceHistoryAsync(int cryptoId, DateTime startDate, DateTime endDate)
        {
            return await _context.PriceDataPoints
                .Where(p => p.CryptocurrencyId == cryptoId && p.Timestamp >= startDate && p.Timestamp <= endDate)
                .OrderBy(p => p.Timestamp)
                .ToListAsync();
        }
    }
}