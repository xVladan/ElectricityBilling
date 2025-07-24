using ElectricityBilling.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBilling.Application.Interfaces
{
    public interface IPlanService
    {
        Task<IEnumerable<PlanResponseDTO>> GetAllPlansAsync();
        Task<PlanResponseDTO?> GetPlanByIdAsync(int id);
        Task<PlanResponseDTO> CreatePlanAsync(PlanDTO dto);
        Task<bool> UpdatePlanAsync(int id, PlanDTO dto);
        Task<bool> DeletePlanAsync(int id);
    }
}
