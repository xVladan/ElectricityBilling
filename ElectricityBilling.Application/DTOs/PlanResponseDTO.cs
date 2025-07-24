using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBilling.Application.DTOs
{
    public class PricingTierResponseDTO
    {
        public int ID { get; set; }

        public int? Threshold { get; set; }
        public decimal PricePerKwh { get; set; }
    }
    public class PlanResponseDTO
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public decimal Discount { get; set; }
        public List<PricingTierResponseDTO> PricingTiers { get; set; } = new();
    }
}
