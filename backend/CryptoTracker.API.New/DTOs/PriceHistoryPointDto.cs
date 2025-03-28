namespace CryptoTracker.API.New.DTOs
{
    public class PriceHistoryPointDto
    {
        public string Timestamp { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}