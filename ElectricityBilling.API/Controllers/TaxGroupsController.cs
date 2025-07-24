using ElectricityBilling.Application.DTOs;
using ElectricityBilling.Application.Interfaces;
using ElectricityBilling.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ElectricityBilling.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxGroupController : ControllerBase
    {
        private readonly ITaxGroupService _taxGroupService;
        private readonly ILogger<TaxGroupController> _logger;

        public TaxGroupController(ITaxGroupService taxGroupService, ILogger<TaxGroupController> logger)
        {
            _taxGroupService = taxGroupService;
            _logger = logger;
        }

        // GET: api/TaxGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxGroupDTO>>> GetAllTaxGroups()
        {
            try
            {
                var taxGroups = await _taxGroupService.GetAllTaxGroupsAsync();
                _logger.LogInformation("Successfully retrieved {Count} tax groups", taxGroups.Count());
                return Ok(taxGroups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all tax groups");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/TaxGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaxGroupDTO>> GetTaxGroupById(int id)
        {
            try
            {
                var taxGroup = await _taxGroupService.GetTaxGroupByIdAsync(id);
                if (taxGroup == null)
                {
                    _logger.LogWarning("Tax group with ID {Id} not found", id);
                    return NotFound("Tax group not found");
                }

                _logger.LogInformation("Successfully fetched tax group with ID {Id}", id);
                return Ok(taxGroup);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching TaxGroup with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/TaxGroup
        [HttpPost]
        public async Task<ActionResult<TaxGroupResponseDTO>> CreateTaxGroup([FromBody] TaxGroupDTO dto)
        {
            try
            {
                var created = await _taxGroupService.CreateTaxGroupAsync(dto);
                _logger.LogInformation("Successfully created tax group with ID {Id}", created.ID);
                return CreatedAtAction(nameof(GetTaxGroupById), new { id = created.ID }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating new tax group");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/TaxGroup/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTaxGroup(int id, [FromBody] TaxGroupDTO dto)
        {
            try
            {
                var result = await _taxGroupService.UpdateTaxGroupAsync(id, dto);
                if (!result)
                {
                    _logger.LogWarning("Tax group with ID {Id} not found for update", id);
                    return NotFound("Tax group not found");
                }

                _logger.LogInformation("Successfully updated tax group with ID {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating tax group with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/TaxGroup/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTaxGroup(int id)
        {
            try
            {
                var result = await _taxGroupService.DeleteTaxGroupAsync(id);
                if (!result)
                {
                    _logger.LogWarning("Tax group with ID {Id} not found for deletion", id);
                    return NotFound("Tax group not found");
                }

                _logger.LogInformation("Successfully deleted tax group with ID {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting tax group with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
