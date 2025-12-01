using Inventive.AdminAPI.Models.V1.Equipment;
using Inventive.Core.DTO.Equipment;
using Inventive.Core.Interfaces.Services.Equipment;
using Microsoft.AspNetCore.Mvc;

namespace Inventive.AdminAPI.Controllers.V1;

[ApiController]
[Route("v1/[controller]")]
internal sealed class EquipmentController(IAdminEquipmentService equipmentService, ILogger<EquipmentController> logger)
    : Controller
{
    [HttpPost]
    public async Task<IActionResult> AddEquipment(AddEquipmentRequestModel request)
    {
        logger.LogInformation(
            "{EquipmentControllerName}.{AddEquipmentName}: Received create equipment request for item: {RequestName}",
            nameof(EquipmentController), nameof(AddEquipment), request.Name);
        var result = await equipmentService.AddNewEquipment(new AddEquipmentRequestDto
        {
            Name = request.Name,
            Description = request.Description,
            Length = request.Length,
            Width = request.Width,
            Height = request.Height,
            Weight = request.Weight
        });
        if (!result.IsSuccess)
        {
            logger.LogError(
                "{EquipmentControllerName}.{AddEquipmentName}: Unexpected error during create equipment request for item: {RequestName}",
                nameof(EquipmentController), nameof(AddEquipment), request.Name);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(new AddEquipmentResponseModel
        {
            Id = result.Value.Id,
            Name = result.Value.Name,
            Description = result.Value.Description,
            Status = result.Value.Status,
            Length = result.Value.Length,
            Width = result.Value.Width,
            Height = result.Value.Height,
            Weight = result.Value.Weight,
            CreatedAt = result.Value.CreatedAt
        });
    }
}
