using RestAPI.Application.DTOs.FilterDTOs;
using RestAPI.Application.DTOs.OrderDTOs;
using RestAPI.Application.DTOs.ProviderDTOs;

namespace RestAPI.Application.Services.OrderService
{
    public interface IOrderService
    {
        Task<List<ProviderDto>> GetAllOrderProvidersAsync();
        Task<OrderDetailDto?> GetOrderDetailOrNullAsync(int orderId);        
        Task<string?> CreateAsync(CreateOrderDto dtoModel);
        Task<List<OrderDto>> GetOrdersAsync(SearchOrdersFilterDto filter);        
        Task<bool> DeleteAsync(int orderId);
        Task<bool> UpdateAsync(int orderId, UpdateOrderDto dtoModel);
    }
}
