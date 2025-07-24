using ElectricityBilling.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBilling.Application.Interfaces
{
    public interface IRecommendationService
    {
        Task<RecommendationDTO> RecommendBestPlanAsync(string taxGroupName, decimal monthlyConsumption);
        Task<List<string>> GetValidTaxGroupsAsync();
    }

}
