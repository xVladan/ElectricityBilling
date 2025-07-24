using ElectricityBilling.Application.DTOs;
using ElectricityBilling.Application.Interfaces;
using ElectricityBilling.Domain.Entities;
using ElectricityBilling.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectricityBilling.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;
        private readonly ILogger<PlanController> _logger;

        public PlanController(IPlanService planService, ILogger<PlanController> logger)
        {
            _planService = planService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanDTO>>> GetAllPlans()
        {
            try
            {
                var plans = await _planService.GetAllPlansAsync();
                _logger.LogInformation("Successfully retrieved {Count} plans", plans.Count());
                return Ok(plans);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all plans");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlanDTO>> GetPlanById(int id)
        {
            try
            {
                var plan = await _planService.GetPlanByIdAsync(id);
                if (plan == null)
                {
                    _logger.LogWarning("Plan with ID {Id} not found", id);
                    return NotFound("Plan not found");
                }

                _logger.LogInformation("Successfully fetched plan with ID {Id}", id);
                return Ok(plan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching plan with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PlanResponseDTO>> CreatePlan([FromBody] PlanDTO dto)
        {
            try
            {
                var createdPlan = await _planService.CreatePlanAsync(dto);
                if (createdPlan == null)
                {
                    _logger.LogWarning("Failed to create plan. Plan name must be unique.");
                    return BadRequest("Plan name must be unique.");
                }

                _logger.LogInformation("Successfully created plan with ID {Id}", createdPlan.ID);
                return CreatedAtAction(nameof(GetPlanById), new { id = createdPlan.ID }, createdPlan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new plan");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePlan(int id, [FromBody] PlanDTO dto)
        {
            try
            {
                var result = await _planService.UpdatePlanAsync(id, dto);
                if (!result)
                {
                    _logger.LogWarning("Plan with ID {Id} not found for update", id);
                    return NotFound("Plan not found");
                }

                _logger.LogInformation("Successfully updated plan with ID {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating plan with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            try
            {
                var result = await _planService.DeletePlanAsync(id);
                if (!result)
                {
                    _logger.LogWarning("Plan with ID {Id} not found for deletion", id);
                    return NotFound("Plan not found");
                }

                _logger.LogInformation("Successfully deleted plan with ID {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting plan with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
