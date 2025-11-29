using FluentResults;
using Inventive.Core.DTO.Equipment;
using Inventive.Core.Interfaces.Services.Equipment;

namespace Inventive.Services.Equipment;

public class AdminEquipmentService : IAdminEquipmentService
{
    public Task<Result<EquipmentResponseDto>> AddNewEquipment(AddEquipmentRequestDto request) =>
        throw new NotImplementedException();
}
