using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTracker.API.New.Models
{
    public class Cryptocurrency
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(10)]
        public string Symbol { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,8)")]
        public decimal CurrentPrice { get; set; }
        
        [Column(TypeName = "decimal(24,2)")]
        public decimal MarketCap { get; set; }
        
        [Column(TypeName = "decimal(24,2)")]
        public decimal Volume24h { get; set; }
        
        [Column(TypeName = "decimal(24,2)")]
        public decimal CirculatingSupply { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal PriceChangePercentage24h { get; set; }
        
        [StringLength(255)]
        public string ImageUrl { get; set; } = string.Empty;
        
        // Navigation property
        public virtual ICollection<PriceDataPoint> PriceHistory { get; set; } = new List<PriceDataPoint>();
    }
}