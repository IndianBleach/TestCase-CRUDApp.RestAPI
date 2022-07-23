using RestAPI.Application.DTOs.OrderItemDTOs;

namespace RestAPI.Application.Services.OrderItemService;

public interface IOrderItemService
{
    Task<ICollection<UnitDto>> GetAllDistinctUnitsAsync();
    Task<bool> CreateOrderItemAsync(CreateOrderItemDto dtoModel);
    Task<bool> DeleteOrderItemAsync(int itemId);
    Task<bool> UpdateOrderItemAsync(int itemId, UpdateOrderItemDto dtoModel);
}
