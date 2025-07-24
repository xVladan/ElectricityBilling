using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricityBilling.Application.DTOs;
using FluentValidation;

namespace ElectricityBilling.Application.Validators
{
    public class UpdatePlanValidator : AbstractValidator<PlanDTO>
    {
        public UpdatePlanValidator() 
        {
            RuleFor(x => x.Name)
              .Must(val => val.ToLower() != "string")
              .WithMessage("Name cannot be 'string'.");

            RuleForEach(x => x.PricingTiers).SetValidator(new PricingTierValidator());

        }
    }
}
