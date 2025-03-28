using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTracker.API.New.Models
{
    public class PriceDataPoint
    {
        [Key]
        public int Id { get; set; }
        
        public int CryptocurrencyId { get; set; }
        
        [Required]
        public DateTime Timestamp { get; set; }
        
        [Column(TypeName = "decimal(18,8)")]
        public decimal Price { get; set; }
        
        [Column(TypeName = "decimal(24,2)")]
        public decimal Volume { get; set; }
        
        [Column(TypeName = "decimal(24,2)")]
        public decimal MarketCap { get; set; }
        
        // Foreign key relationship
        [ForeignKey("CryptocurrencyId")]
        public virtual Cryptocurrency? Cryptocurrency { get; set; }
    }
}