using FluentResults;
using Inventive.Core.DTO.Equipment;
using Inventive.Core.Interfaces.Repositories;
using Inventive.Core.Interfaces.Services.Equipment;
using Microsoft.Extensions.Logging;
using EquipmentEntity = Inventive.Core.Models.Equipment;

namespace Inventive.Services.Equipment;

public class AdminEquipmentService(IEquipmentRepository equipmentRepository, ILogger<AdminEquipmentService> logger)
    : IAdminEquipmentService
{
    public async Task<Result<EquipmentResponseDto>> AddNewEquipment(AddEquipmentRequestDto request)
    {
        logger.LogInformation(
            "{AdminEquipmentService}.{AddNewEquipment}: Adding {EquipmentName} (L:{Length} W:{Width} H:{Height} Wt:{Weight})",
            nameof(AdminEquipmentService), nameof(AddNewEquipment), request.Name, request.Length, request.Width,
            request.Height, request.Weight);

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

        var response = new EquipmentResponseDto
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

        logger.LogInformation(
            "{AdminEquipmentService}.{AddNewEquipment}: Successfully Added {EquipmentName} with id {EquipmentId}",
            nameof(AdminEquipmentService), nameof(AddNewEquipment), request.Name, response.Id);

        return Result.Ok(response);
    }
}
