using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBilling.Application.DTOs
{
    public class PricingTierDTO
    {
        public int? Threshold { get; set; }
        public decimal PricePerKwh { get; set; }
    }

    public class PlanDTO
    {
        public required string Name { get; set; }
        public decimal Discount { get; set; }
        public List<PricingTierDTO> PricingTiers { get; set; } = new();
    }
}
