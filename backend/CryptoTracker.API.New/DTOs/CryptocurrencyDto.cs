namespace CryptoTracker.API.New.DTOs
{
    public class CryptocurrencyDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
        public decimal MarketCap { get; set; }
        public decimal Volume24h { get; set; }
        public decimal CirculatingSupply { get; set; }
        public decimal PriceChangePercentage24h { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}