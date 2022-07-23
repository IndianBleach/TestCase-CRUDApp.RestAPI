using AutoMapper;
using RestAPI.Application.DTOs.OrderItemDTOs;
using Microsoft.EntityFrameworkCore;
using RestAPI.Data.Data;
using RestAPI.Data.Entities.OrderEntity;
using RestAPI.Data.Entities.OrderItemEntity;
using System.Globalization;

namespace RestAPI.Application.Services.OrderItemService
{
    public class OrderItemService : IOrderItemService
    {
        private readonly ApplicationDbContext _appContext;
        public OrderItemService(ApplicationDbContext context)
            => _appContext = context;

        /// <summary>
        /// Добавить элемент в заказ
        /// </summary>
        /// <param name="dtoModel"></param>
        /// <returns>True, если элемент успешно добавлен, иначе - false</returns>
        public async Task<bool> CreateOrderItemAsync(CreateOrderItemDto dtoModel)
        {
            Order? getOrder = await _appContext.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id.Equals(dtoModel.OrderId));

            if (getOrder != null)
            {
                IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                decimal.TryParse(dtoModel.Quantity, NumberStyles.Any, formatter, out decimal quantity);

                OrderItem orderItem = new(dtoModel.OrderId, dtoModel.Name, quantity, dtoModel.Unit);

                await _appContext.OrderItems.AddAsync(orderItem);
                await _appContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        /// <summary>
        /// Удаление элемента из заказа
        /// </summary>
        /// <param name="dtoModel"></param>
        /// <returns>True, если операция прошла успешно, иначе - false</returns>
        public async Task<bool> DeleteOrderItemAsync(int? itemId)
        {
            OrderItem? getItem = await _appContext.OrderItems
                .FirstOrDefaultAsync(x => x.Id.Equals((int)itemId));

            if (getItem == null) return false;

            _appContext.OrderItems.Remove(getItem);
            await _appContext.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Все уникальные единицы измерения у элементов
        /// </summary>
        /// <returns>Список dto объектов единиц измерения</returns>
        public async Task<ICollection<UnitDto>> GetAllDistinctUnitsAsync()
        {
            return await _appContext.OrderItems
                .Select(x => new UnitDto()
                { 
                    Name = x.Unit
                })
                .Distinct()
                .ToListAsync();            
        }

        /// <summary>
        /// Обновить элемент заказа
        /// </summary>
        /// <param name="dtoModel"></param>
        /// <returns>True, если обновление прошло успешно, иначе - false</returns>
        public async Task<bool> UpdateOrderItemAsync(UpdateOrderItemDto dtoModel)
        {
            OrderItem? getItem = await _appContext.OrderItems
                .FirstOrDefaultAsync(x => x.OrderId.Equals(dtoModel.OrderId) &&
                x.Id.Equals(dtoModel.ItemId));

            if (getItem != null)
            {
                if (!string.IsNullOrEmpty(dtoModel.Quantity))
                {
                    IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                    decimal.TryParse(dtoModel.Quantity, NumberStyles.Any, formatter, out decimal newQuantity);
                    getItem.Quantity = newQuantity;
                }

                if (!string.IsNullOrEmpty(dtoModel.Unit))
                    getItem.Unit = dtoModel.Unit;

                if (!string.IsNullOrWhiteSpace(dtoModel.Name))
                    getItem.Name = dtoModel.Name;

                _appContext.OrderItems.Update(getItem);
                await _appContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
