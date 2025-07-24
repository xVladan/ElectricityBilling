using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBilling.Application.DTOs
{
    public class RecommendationRequestDTO
    {
        public string TaxGroupName { get; set; } = string.Empty;
        public decimal MonthlyConsumption { get; set; }
    }

}
