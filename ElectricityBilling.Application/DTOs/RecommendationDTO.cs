using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBilling.Application.DTOs
{
    public class RecommendationDTO
    {
        public PlanDTO Plan { get; set; }
        public decimal BaseCost { get; set; }
        public decimal Discount { get; set; }
        public decimal Vat { get; set; }
        public decimal EcoTax { get; set; }
        public decimal FinalCost { get; set; }
    }

}
