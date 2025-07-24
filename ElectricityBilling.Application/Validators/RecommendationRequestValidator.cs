using ElectricityBilling.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBilling.Application.Validators
{
    public class RecommendationRequestValidator : AbstractValidator<RecommendationRequestDTO>
    {
        public RecommendationRequestValidator()
        {
            RuleFor(x => x.TaxGroupName)
                .NotEmpty().WithMessage("Tax group is required.");

            RuleFor(x => x.MonthlyConsumption)
                .GreaterThan(0).WithMessage("Monthly consumption must be greater than 0.");
        }
    }
}
