using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBilling.Domain.Entities
{
    public class Plan
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public decimal Discount { get; set; }

        public List<PricingTier> PricingTiers { get; set; } = new();
    }
}
