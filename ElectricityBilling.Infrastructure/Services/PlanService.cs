using ElectricityBilling.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricityBilling.Application.Interfaces;
using ElectricityBilling.Application.DTOs;

namespace ElectricityBilling.Infrastructure.Services
{
    public class PlanService : IPlanService
    {
        private readonly AppDbContext _context;

        public PlanService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlanResponseDTO>> GetAllPlansAsync()
        {
            var plans = await _context.Plans
                .Include(p => p.PricingTiers)
                .ToListAsync();

            return plans.Select(p => new PlanResponseDTO
            {
                ID = p.ID,
                Name = p.Name,
                Discount = p.Discount,
                PricingTiers = p.PricingTiers.Select(t => new PricingTierResponseDTO
                {
                    ID = t.ID,
                    Threshold = t.Threshold,
                    PricePerKwh = t.PricePerKwh
                }).ToList()
            });
        }

        public async Task<PlanResponseDTO?> GetPlanByIdAsync(int id)
        {
            var plan = await _context.Plans
                .Include(p => p.PricingTiers)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (plan == null) return null;

            return new PlanResponseDTO
            {
                ID = plan.ID,
                Name = plan.Name,
                Discount = plan.Discount,
                PricingTiers = plan.PricingTiers.Select(t => new PricingTierResponseDTO
                {
                    ID = t.ID,
                    Threshold = t.Threshold,
                    PricePerKwh = t.PricePerKwh
                }).ToList()
            };
        }

        public async Task<PlanResponseDTO> CreatePlanAsync(PlanDTO dto)
        {

            if (await _context.Plans.AnyAsync(p => p.Name.ToLower() == dto.Name.ToLower()))
                return null;

            var plan = new Plan
            {
                Name = dto.Name,
                Discount = dto.Discount,
                PricingTiers = dto.PricingTiers.Select(t => new PricingTier
                {
                    Threshold = t.Threshold == 0 ? null : t.Threshold,
                    PricePerKwh = t.PricePerKwh
                }).ToList()
            };

            _context.Plans.Add(plan);
            await _context.SaveChangesAsync();

            return new PlanResponseDTO
            {
                ID = plan.ID,
                Name = plan.Name,
                Discount = plan.Discount,
                PricingTiers = plan.PricingTiers.Select(t => new PricingTierResponseDTO
                {
                    ID = t.ID,
                    Threshold = t.Threshold,
                    PricePerKwh = t.PricePerKwh
                }).ToList()
            };
        }

        public async Task<bool> UpdatePlanAsync(int id, PlanDTO dto)
        {
            var plan = await _context.Plans
                .Include(p => p.PricingTiers)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (plan == null)
                return false;

            plan.Name = dto.Name;
            plan.Discount = dto.Discount;

            _context.PricingTiers.RemoveRange(plan.PricingTiers);

            plan.PricingTiers = dto.PricingTiers.Select(t => new PricingTier
            {
                Threshold = t.Threshold == 0 ? null : t.Threshold,
                PricePerKwh = t.PricePerKwh
            }).ToList();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePlanAsync(int id)
        {
            var plan = await _context.Plans
                .Include(p => p.PricingTiers)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (plan == null)
                return false;

            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
