using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBilling.Application.DTOs
{
    public class TaxGroupResponseDTO
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public decimal Vat { get; set; }
        public decimal EcoTax { get; set; }
    }
}
