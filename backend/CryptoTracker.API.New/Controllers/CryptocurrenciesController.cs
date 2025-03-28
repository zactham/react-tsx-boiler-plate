using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CryptoTracker.API.New.Data.Repositories;
using CryptoTracker.API.New.Models;
using CryptoTracker.API.New.DTOs;

namespace CryptoTracker.API.New.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CryptocurrenciesController : ControllerBase
    {
        private readonly ILogger<CryptocurrenciesController> _logger;
        private readonly ICryptoRepository _repository;

        public CryptocurrenciesController(
            ILogger<CryptocurrenciesController> logger,
            ICryptoRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/cryptocurrencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CryptocurrencyDto>>> GetCryptocurrencies()
        {
            try
            {
                var cryptocurrencies = await _repository.GetAllCryptocurrenciesAsync();
                var cryptoDtos = cryptocurrencies.Select(c => new CryptocurrencyDto
                {
                    Id = c.Id,
                    Symbol = c.Symbol,
                    Name = c.Name,
                    CurrentPrice = c.CurrentPrice,
                    MarketCap = c.MarketCap,
                    Volume24h = c.Volume24h,
                    CirculatingSupply = c.CirculatingSupply,
                    PriceChangePercentage24h = c.PriceChangePercentage24h,
                    ImageUrl = c.ImageUrl
                });

                return Ok(cryptoDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all cryptocurrencies");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/cryptocurrencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CryptocurrencyDto>> GetCryptocurrency(int id)
        {
            try
            {
                var cryptocurrency = await _repository.GetCryptocurrencyByIdAsync(id);

                if (cryptocurrency == null)
                {
                    return NotFound();
                }

                var cryptoDto = new CryptocurrencyDto
                {
                    Id = cryptocurrency.Id,
                    Symbol = cryptocurrency.Symbol,
                    Name = cryptocurrency.Name,
                    CurrentPrice = cryptocurrency.CurrentPrice,
                    MarketCap = cryptocurrency.MarketCap,
                    Volume24h = cryptocurrency.Volume24h,
                    CirculatingSupply = cryptocurrency.CirculatingSupply,
                    PriceChangePercentage24h = cryptocurrency.PriceChangePercentage24h,
                    ImageUrl = cryptocurrency.ImageUrl
                };

                return Ok(cryptoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting cryptocurrency with ID: {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/cryptocurrencies/symbol/btc
        [HttpGet("symbol/{symbol}")]
        public async Task<ActionResult<CryptocurrencyDto>> GetCryptocurrencyBySymbol(string symbol)
        {
            try
            {
                var cryptocurrency = await _repository.GetCryptocurrencyBySymbolAsync(symbol);

                if (cryptocurrency == null)
                {
                    return NotFound();
                }

                var cryptoDto = new CryptocurrencyDto
                {
                    Id = cryptocurrency.Id,
                    Symbol = cryptocurrency.Symbol,
                    Name = cryptocurrency.Name,
                    CurrentPrice = cryptocurrency.CurrentPrice,
                    MarketCap = cryptocurrency.MarketCap,
                    Volume24h = cryptocurrency.Volume24h,
                    CirculatingSupply = cryptocurrency.CirculatingSupply,
                    PriceChangePercentage24h = cryptocurrency.PriceChangePercentage24h,
                    ImageUrl = cryptocurrency.ImageUrl
                };

                return Ok(cryptoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting cryptocurrency with symbol: {symbol}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/cryptocurrencies/5/history/7d
        [HttpGet("{id}/history/{timeRange}")]
        public async Task<ActionResult<CryptoPriceHistoryDto>> GetPriceHistory(int id, string timeRange)
        {
            try
            {
                var cryptocurrency = await _repository.GetCryptocurrencyByIdAsync(id);

                if (cryptocurrency == null)
                {
                    return NotFound("Cryptocurrency not found");
                }

                DateTime endDate = DateTime.UtcNow;
                DateTime startDate = GetStartDateFromTimeRange(timeRange);

                var priceHistory = await _repository.GetPriceHistoryAsync(id, startDate, endDate);
                
                if (!priceHistory.Any())
                {
                    return NotFound("No price history data found for the specified time range");
                }

                var priceHistoryDto = new CryptoPriceHistoryDto
                {
                    Id = cryptocurrency.Id,
                    Symbol = cryptocurrency.Symbol,
                    Name = cryptocurrency.Name,
                    TimeRange = timeRange,
                    PriceHistory = priceHistory.Select(p => new PriceHistoryPointDto
                    {
                        Timestamp = p.Timestamp.ToString("o"),
                        Price = p.Price
                    }).ToList()
                };

                return Ok(priceHistoryDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting price history for cryptocurrency ID: {id} with time range: {timeRange}");
                return StatusCode(500, "Internal server error");
            }
        }

        private DateTime GetStartDateFromTimeRange(string timeRange)
        {
            DateTime endDate = DateTime.UtcNow;
            
            return timeRange.ToLower() switch
            {
                "1d" => endDate.AddDays(-1),
                "7d" => endDate.AddDays(-7),
                "30d" => endDate.AddDays(-30),
                "1y" => endDate.AddYears(-1),
                "all" => endDate.AddYears(-10), // Arbitrary "all" timeframe
                _ => endDate.AddDays(-7) // Default to 7 days
            };
        }
    }
}