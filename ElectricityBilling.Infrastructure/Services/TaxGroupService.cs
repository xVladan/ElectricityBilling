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
    public class TaxGroupService : ITaxGroupService
    {
        private readonly AppDbContext _context;

        public TaxGroupService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaxGroupResponseDTO>> GetAllTaxGroupsAsync()
        {
            return await _context.TaxGroups
                .Select(t => new TaxGroupResponseDTO
                {
                    ID = t.ID,
                    Name = t.Name,
                    Vat = t.Vat,
                    EcoTax = t.EcoTax
                })
                .ToListAsync();
        }

        public async Task<TaxGroupResponseDTO?> GetTaxGroupByIdAsync(int id)
        {
            var taxGroup = await _context.TaxGroups.FindAsync(id);
            if (taxGroup == null) return null;

            return new TaxGroupResponseDTO
            {
                ID = taxGroup.ID,
                Name = taxGroup.Name,
                Vat = taxGroup.Vat,
                EcoTax = taxGroup.EcoTax
            };
        }

        public async Task<TaxGroupResponseDTO> CreateTaxGroupAsync(TaxGroupDTO dto)
        {
            var taxGroup = new TaxGroup
            {
                Name = dto.Name,
                Vat = dto.Vat,
                EcoTax = dto.EcoTax
            };

            _context.TaxGroups.Add(taxGroup);
            await _context.SaveChangesAsync();

            return new TaxGroupResponseDTO
            {
                ID = taxGroup.ID,
                Name = taxGroup.Name,
                Vat = taxGroup.Vat,
                EcoTax = taxGroup.EcoTax
            };
        }

        public async Task<bool> UpdateTaxGroupAsync(int id, TaxGroupDTO dto)
        {
            var taxGroup = await _context.TaxGroups.FindAsync(id);
            if (taxGroup == null)
                return false;

            taxGroup.Name = dto.Name;
            taxGroup.Vat = dto.Vat;
            taxGroup.EcoTax = dto.EcoTax;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTaxGroupAsync(int id)
        {
            var taxGroup = await _context.TaxGroups.FindAsync(id);
            if (taxGroup == null)
                return false;

            _context.TaxGroups.Remove(taxGroup);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
