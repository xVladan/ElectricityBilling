using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace ElectricityBilling.Domain.Entities
{
    public class PricingTier
    {
        public int ID { get; set; }
        public int? Threshold { get; set; } // može biti null
        public decimal PricePerKwh { get; set; }

        public int PlanID { get; set; }

        [JsonIgnore]
        public Plan Plan { get; set; } = null!;
    }
}
