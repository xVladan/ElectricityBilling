using ElectricityBilling.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBilling.Application.Interfaces
{
    public interface ITaxGroupService
    {
        Task<IEnumerable<TaxGroupResponseDTO>> GetAllTaxGroupsAsync();
        Task<TaxGroupResponseDTO?> GetTaxGroupByIdAsync(int id);
        Task<TaxGroupResponseDTO> CreateTaxGroupAsync(TaxGroupDTO dto);
        Task<bool> UpdateTaxGroupAsync(int id, TaxGroupDTO dto);
        Task<bool> DeleteTaxGroupAsync(int id);
    }
}
