using CryptoTracker.API.New.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoTracker.API.New.Data.Repositories
{
    public interface ICryptoRepository
    {
        Task<IEnumerable<Cryptocurrency>> GetAllCryptocurrenciesAsync();
        Task<Cryptocurrency?> GetCryptocurrencyByIdAsync(int id);
        Task<Cryptocurrency?> GetCryptocurrencyBySymbolAsync(string symbol);
        Task<IEnumerable<PriceDataPoint>> GetPriceHistoryAsync(int cryptoId, DateTime startDate, DateTime endDate);
    }
}