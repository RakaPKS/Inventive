using FluentResults;
using Inventive.Core.DTO.Common;
using Inventive.Core.DTO.Equipment;
using Inventive.Core.Interfaces.Repositories;
using Inventive.Core.Interfaces.Services.Equipment;
using Inventive.Core.Models.Errors;
using Inventive.Core.Util;
using Microsoft.Extensions.Logging;
using EquipmentEntity = Inventive.Core.Models.Equipment;

namespace Inventive.Services.Equipment;

public class AdminEquipmentService(IEquipmentRepository equipmentRepository, ILogger<AdminEquipmentService> logger)
    : IAdminEquipmentService
{
    public async Task<Result<EquipmentResponseDto>> AddNewEquipment(AddEquipmentRequestDto request)
    {
        logger.LogInformationWithContext(
            "Adding {EquipmentName} (L:{Length} W:{Width} H:{Height} Wt:{Weight})",
            request.Name, request.Length, request.Width, request.Height, request.Weight);
        var equipment = new EquipmentEntity(
            request.Name,
            request.Description,
            request.Length,
            request.Width,
            request.Height,
            request.Weight
        );

        await equipmentRepository.AddAsync(equipment);
        await equipmentRepository.SaveChangesAsync();

        var response = MapToDto(equipment);

        logger.LogInformationWithContext(
            "Successfully Added {EquipmentName} with id {EquipmentId}",
            request.Name, response.Id);

        return Result.Ok(response);
    }

    public async Task<Result<PaginatedResultDto<EquipmentResponseDto>>> GetAllEquipment(PaginatedRequestDto request)
    {
        logger.LogInformationWithContext(
            "Fetching equipment page {Page}, size {PageSize}",
            request.Page, request.PageSize);

        var (items, totalCount) = await equipmentRepository.GetPaginatedAsync(
            request.Skip,
            request.PageSize);

        var equipmentDtos = items.Select(MapToDto).ToList();

        var result = new PaginatedResultDto<EquipmentResponseDto> { Items = equipmentDtos, TotalCount = totalCount };

        logger.LogInformationWithContext(
            "Returning {Count} items out of {TotalCount}",
            equipmentDtos.Count, totalCount);

        return Result.Ok(result);
    }

    public async Task<Result<EquipmentResponseDto>> GetEquipmentById(Guid id)
    {
        logger.LogInformationWithContext(
            "Fetching equipment {EquipmentId}",
            id);

        var equipment = await equipmentRepository.GetByIdAsync(id);

        if (equipment == null)
        {
            logger.LogWarningWithContext(
                "Equipment {EquipmentId} not found",
                id);
            return Result.Fail(new NotFoundError($"Equipment with ID {id} not found"));
        }

        var response = MapToDto(equipment);

        logger.LogInformationWithContext(
            "Found equipment {EquipmentName}",
            response.Name);

        return Result.Ok(response);
    }

    private static EquipmentResponseDto MapToDto(EquipmentEntity equipment)
    {
        return new EquipmentResponseDto
        {
            Id = equipment.Id,
            Name = equipment.Name,
            Description = equipment.Description,
            Status = equipment.Status,
            Length = equipment.Length,
            Width = equipment.Width,
            Height = equipment.Height,
            Weight = equipment.Weight,
            CreatedAt = equipment.CreatedAt.DateTime,
            CreatedBy = equipment.CreatedBy,
            ModifiedAt = equipment.ModifiedAt?.DateTime,
            ModifiedBy = equipment.ModifiedBy
        };
    }
}
