using ElectricityBilling.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBilling.Application.Validators
{
    public class PricingTierValidator : AbstractValidator<PricingTierDTO>
    {
        public PricingTierValidator()
        {
            RuleFor(x => x.PricePerKwh).GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(x => x.Threshold).GreaterThanOrEqualTo(0).When(x => x.Threshold.HasValue);
        }
    }
}
