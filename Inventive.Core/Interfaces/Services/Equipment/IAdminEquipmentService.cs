using FluentResults;
using Inventive.Core.DTO.Equipment;

namespace Inventive.Core.Interfaces.Services.Equipment;

public interface IAdminEquipmentService
{
    public Task<Result<EquipmentResponseDto>> AddNewEquipment(AddEquipmentRequestDto request);
}
