using ElectricityBilling.Application.DTOs;
using ElectricityBilling.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectricityBilling.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<RecommendationDTO>> Recommend([FromBody] RecommendationRequestDTO request)
        {
            var recommendation = await _recommendationService.RecommendBestPlanAsync(request.TaxGroupName, request.MonthlyConsumption);

            if (recommendation == null)
            {
                var availableTaxGroups = await _recommendationService.GetValidTaxGroupsAsync();
                return NotFound(new
                {
                    Message = $"Tax group '{request.TaxGroupName}' not found.",
                    AvailableTaxGroups = availableTaxGroups
                });
            }

            return Ok(recommendation);
        }
    }
}
