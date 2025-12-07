using FluentResults;
using Inventive.Core.DTO.Common;
using Inventive.Core.DTO.Equipment;

namespace Inventive.Core.Interfaces.Services.Equipment;

public interface IAdminEquipmentService
{
    public Task<Result<EquipmentResponseDto>> AddNewEquipment(AddEquipmentRequestDto request);
    public Task<Result<PaginatedResultDto<EquipmentResponseDto>>> GetAllEquipment(PaginatedRequestDto request);
    public Task<Result<EquipmentResponseDto>> GetEquipmentById(Guid id);
}
