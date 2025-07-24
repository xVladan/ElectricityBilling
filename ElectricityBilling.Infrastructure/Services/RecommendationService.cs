using ElectricityBilling.Application.DTOs;
using ElectricityBilling.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBilling.Infrastructure.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly AppDbContext _context;

        public RecommendationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetValidTaxGroupsAsync()
        {
            return await _context.TaxGroups
                .Select(t => t.Name)
                .ToListAsync();
        }

        public async Task<RecommendationDTO> RecommendBestPlanAsync(string taxGroupName, decimal monthlyConsumption)
        {
            var taxGroup = await _context.TaxGroups.FirstOrDefaultAsync(t => t.Name == taxGroupName);
            if (taxGroup == null)
                return null;

            var plans = await _context.Plans
                .Include(p => p.PricingTiers)
                .ToListAsync();

            RecommendationDTO bestRecommendation = null;
            decimal lowestFinalCost = decimal.MaxValue;

            foreach (var plan in plans)
            {
                decimal baseCost = 0;
                decimal remaining = monthlyConsumption;

                var tiers = plan.PricingTiers
                    .OrderBy(t => t.Threshold ?? int.MaxValue)
                    .ToList();

                int previousThreshold = 0;

                foreach (var tier in tiers)
                {
                    int currentThreshold = tier.Threshold ?? int.MaxValue;
                    int tierRange = currentThreshold - previousThreshold;

                    decimal kWhInThisTier = Math.Min(remaining, tierRange);
                    baseCost += kWhInThisTier * tier.PricePerKwh;

                    remaining -= kWhInThisTier;
                    previousThreshold = currentThreshold;

                    if (remaining <= 0) break;
                }

                var discounted = baseCost * (1 - plan.Discount / 100m);

                var vatAmount = discounted * (taxGroup.Vat / 100m);
                var finalCost = discounted + vatAmount + taxGroup.EcoTax;

                if (finalCost < lowestFinalCost)
                {
                    lowestFinalCost = finalCost;

                    bestRecommendation = new RecommendationDTO
                    {
                        Plan = new PlanDTO
                        {
                            Name = plan.Name,
                            Discount = plan.Discount,
                            PricingTiers = tiers.Select(t => new PricingTierDTO
                            {
                                Threshold = t.Threshold,
                                PricePerKwh = t.PricePerKwh
                            }).ToList()
                        },
                        BaseCost = baseCost,
                        Discount = plan.Discount,
                        Vat = taxGroup.Vat,
                        EcoTax = taxGroup.EcoTax,
                        FinalCost = finalCost
                    };
                }
            }

            return bestRecommendation!;
        }

    }
}
