using Inventive.AdminAPI.Models.V1.Equipment;
using Inventive.Core.DTO.Common;
using Inventive.Core.DTO.Equipment;
using Inventive.Core.Interfaces.Services.Equipment;
using Inventive.Core.Models.Errors;
using Inventive.Core.Util;
using Microsoft.AspNetCore.Mvc;

namespace Inventive.AdminAPI.Controllers.V1;

[ApiController]
[Route("v1/[controller]")]
public sealed class EquipmentController(IAdminEquipmentService equipmentService, ILogger<EquipmentController> logger)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddEquipment(AddEquipmentRequestModel request)
    {
        logger.LogInformationWithContext(
            "Received create equipment request for item: {RequestName}",
            request.Name);

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
            logger.LogErrorWithContext(
                "Unexpected error during create equipment request for item: {RequestName}",
                request.Name);
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

    [HttpGet]
    public async Task<IActionResult> GetAllEquipment([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        logger.LogInformationWithContext(
            "Fetching equipment page {Page} with size {PageSize}",
            page, pageSize);

        var result =
            await equipmentService.GetAllEquipment(new PaginatedRequestDto { Page = page, PageSize = pageSize });

        if (!result.IsSuccess)
        {
            logger.LogErrorWithContext("Unexpected error fetching equipment");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // Calculate API-specific pagination metadata
        var totalPages = (int)Math.Ceiling((double)result.Value.TotalCount / pageSize);

        var response = new GetAllEquipmentResponseModel
        {
            Items = result.Value.Items.Select(dto => new EquipmentItemModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Status = dto.Status,
                Length = dto.Length,
                Width = dto.Width,
                Height = dto.Height,
                Weight = dto.Weight,
                CreatedAt = dto.CreatedAt,
                CreatedBy = dto.CreatedBy,
                ModifiedAt = dto.ModifiedAt,
                ModifiedBy = dto.ModifiedBy
            }).ToList(),
            TotalCount = result.Value.TotalCount,
            Page = page,
            PageSize = pageSize,
            TotalPages = totalPages,
            HasNextPage = page < totalPages,
            HasPreviousPage = page > 1
        };

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEquipmentById(Guid id)
    {
        logger.LogInformationWithContext(
            "Fetching equipment {EquipmentId}",
            id);

        var result = await equipmentService.GetEquipmentById(id);

        if (!result.IsSuccess)
        {
            var error = result.Errors is [var first, ..] ? first : null;
            if (error is NotFoundError)
            {
                logger.LogWarningWithContext(
                    "Equipment {EquipmentId} not found",
                    id);
                return NotFound(new { message = error.Message });
            }

            logger.LogErrorWithContext(
                "Unexpected error fetching equipment {EquipmentId}",
                id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var response = new GetEquipmentByIdResponseModel
        {
            Id = result.Value.Id,
            Name = result.Value.Name,
            Description = result.Value.Description,
            Status = result.Value.Status,
            Length = result.Value.Length,
            Width = result.Value.Width,
            Height = result.Value.Height,
            Weight = result.Value.Weight,
            CreatedAt = result.Value.CreatedAt,
            CreatedBy = result.Value.CreatedBy,
            ModifiedAt = result.Value.ModifiedAt,
            ModifiedBy = result.Value.ModifiedBy
        };

        return Ok(response);
    }
}
