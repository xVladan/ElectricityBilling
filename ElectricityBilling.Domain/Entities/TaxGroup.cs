using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBilling.Domain.Entities
{
    public class TaxGroup
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public decimal Vat { get; set; }
        public decimal EcoTax { get; set; }
    }
}
