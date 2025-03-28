using System.Collections.Generic;

namespace CryptoTracker.API.New.DTOs
{
    public class CryptoPriceHistoryDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string TimeRange { get; set; } = string.Empty;
        public List<PriceHistoryPointDto> PriceHistory { get; set; } = new List<PriceHistoryPointDto>();
    }
}