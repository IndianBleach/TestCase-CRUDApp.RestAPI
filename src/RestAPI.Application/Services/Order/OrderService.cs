using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestAPI.Application.DTOs.FilterDTOs;
using RestAPI.Application.DTOs.OrderDTOs;
using RestAPI.Application.DTOs.OrderItemDTOs;
using RestAPI.Application.DTOs.ProviderDTOs;
using RestAPI.Data.Data;
using RestAPI.Data.Entities.OrderEntity;
using RestAPI.Data.Entities.ProviderEntity;
using RestAPI.Domain.Enums;

namespace RestAPI.Application.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _appContext;

        public OrderService(ApplicationDbContext context)
            => _appContext = context;

        /// <summary>
        /// Создание нового заказа
        /// </summary>
        /// <param name="dtoModel"></param>
        /// <returns>Номер созданного заказа (не id)</returns>
        public async Task<string?> CreateAsync(CreateOrderDto dtoModel)
        {
            Provider? getProvider = await _appContext.Providers
                .FirstOrDefaultAsync(x => x.Id.Equals(dtoModel.ProviderId));

            if (getProvider == null) return null;

            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<CreateOrderDto, Order>()
                .ForMember(x => x.Number, opt => opt.MapFrom(x => x.Number))
                .ForMember(x => x.Provider, opt => opt.MapFrom(x => getProvider))
                .ForMember(x => x.ProviderId, opt => opt.MapFrom(x => getProvider.Id))
                .ForMember(x => x.Date, opt => opt.MapFrom(x => x.Date));
            });

            Order order = new Mapper(config)
                .Map<CreateOrderDto, Order>(dtoModel);

            await _appContext.Orders.AddAsync(order);
            await _appContext.SaveChangesAsync();

            return order.Number;
        }

        /// <summary>
        /// Получение заказа с подробной информацией о нём
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Найденный заказ, либо null в ином случае</returns>
        public async Task<OrderDetailDto?> GetOrderDetailOrNullAsync(int? orderId)
        {
            Order? getOrder = await _appContext.Orders
                .Include(x => x.Provider)
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id.Equals((int)orderId));

            if (getOrder == null) return null;

            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<Order, OrderDetailDto>()
                .ForMember(x => x.Number, opt => opt.MapFrom(x => x.Number))
                .ForMember(x => x.Date, opt => opt.MapFrom(x => x.Date))
                .ForMember(x => x.Items, opt => opt.MapFrom(x =>
                    x.OrderItems.Select(e => new OrderItemDto()
                    {
                        Name = e.Name,
                        Quantity = e.Quantity.ToString(),
                        Unit = e.Unit,
                        Id = e.Id
                    })
                    .ToList()
                ))
                .ForMember(x => x.Provider, opt => opt.MapFrom(x => new ProviderDto()
                {
                    Name = x.Provider != null ? x.Provider.Name : "",
                    Id = x.Provider != null ? x.Provider.Id : -1
                }));
            });

            OrderDetailDto dto = new Mapper(config)
                .Map<Order, OrderDetailDto>(getOrder);

            return dto;
        }

        /// <summary>
        /// Удаление заказа по Id
        /// </summary>
        /// <param name="dtoModel"></param>
        /// <returns>True, если заказ был успешно удалён, false - в ином случае</returns>
        public async Task<bool> DeleteAsync(int? orderId)
        {
            Order? getOrder = await _appContext.Orders
                .Include(x => x.OrderItems)
                .Include(x => x.Provider)
                .FirstOrDefaultAsync(x => x.Id.Equals((int)orderId));

            if (getOrder == null) return false;

            _appContext.Orders.Remove(getOrder);
            await _appContext.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Обновление заказа
        /// </summary>
        /// <param name="dtoModel"></param>
        /// <returns>True, если обновление прошло успешно, иначе - false</returns>
        public async Task<bool> UpdateAsync(UpdateOrderDto dtoModel)
        {
            Order? getOrder = await _appContext.Orders
                .Include(x => x.Provider)
                .FirstOrDefaultAsync(x => x.Id.Equals(dtoModel.OrderId));

            if (getOrder == null) return false;

            if (dtoModel.ProviderId.HasValue)
            {
                Provider? getProvider = await _appContext.Providers
                    .FirstOrDefaultAsync(x => x.Id.Equals((int)dtoModel.ProviderId));

                if (getProvider != null) getOrder.Provider = getProvider;
            }

            getOrder.Number = string.IsNullOrEmpty(dtoModel.Number) == false ?
                    dtoModel.Number :
                    getOrder.Number;

            getOrder.Date = dtoModel.Date ?? getOrder.Date;

            _appContext.Orders.Update(getOrder);
            await _appContext.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Получает список заказов
        /// </summary>
        /// <param name="page"></param>
        /// <returns>Список заказов</returns>
        public async Task<List<OrderDto>> GetOrdersAsync(SearchOrdersFilterDto filter)
        {
            IQueryable<Order> orders = _appContext.Orders
                .Include(x => x.OrderItems)
                .Include(x => x.Provider);

            if (filter.SelectedProviders.Count > 0 &&
                filter.SelectedUnits.Count > 0)
            {
                List<Order> tmp = new();

                foreach (string unit in filter.SelectedUnits)
                {
                    List<Order> t = _appContext.Orders
                        .Include(x => x.Provider)
                        .Include(x => x.OrderItems)
                        .Where(x => filter.SelectedProviders.Any(e => e == x.ProviderId) &&
                            x.OrderItems.Any(x => x.Unit.Equals(unit)))
                        .ToList();

                    tmp.AddRange(t);
                }

                orders = Enumerable.Empty<Order>().AsQueryable();

                foreach (Order ord in tmp)
                {
                    orders = orders.Append(await _appContext.Orders
                        .FirstOrDefaultAsync(x => x.Id.Equals(ord.Id)));
                }
            }
            else if (filter.SelectedProviders.Count > 0)
            {
                orders = orders
                    .Where(x => filter.SelectedProviders.Any(e => e == x.ProviderId));
            }
            else if (filter.SelectedUnits.Count > 0)
            {
                List<Order> tmp = new();

                foreach (string unit in filter.SelectedUnits)
                {
                    List<Order> t = _appContext.Orders
                        .Include(x => x.Provider)
                        .Include(x => x.OrderItems)
                        .Where(x => x.OrderItems
                        .Any(x => x.Unit.Equals(unit)))
                        .ToList();

                    tmp.AddRange(t);
                }

                orders = Enumerable.Empty<Order>().AsQueryable();

                foreach (Order ord in tmp)
                {
                    orders = orders.Append(await _appContext.Orders
                        .FirstOrDefaultAsync(x => x.Id.Equals(ord.Id)));
                }

                orders.Distinct();
            }

            if (filter.DateEnd.HasValue && filter.DateStart.HasValue)
            {
                orders = orders
                    .Where(x => x.Date >= filter.DateStart.Value && x.Date <= filter.DateEnd.Value);
            }

            if (filter.SelectedSortValue.HasValue)
            {
                switch ((OrdersSortEnum)filter.SelectedSortValue)
                {
                    case OrdersSortEnum.New:
                        orders = orders.OrderByDescending(x => x.Date);
                        break;

                    case OrdersSortEnum.Old:
                        orders = orders.OrderBy(x => x.Date);
                        break;

                    default:
                        break;
                }
            }

            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>()
                .ForMember(x => x.Number, opt => opt.MapFrom(x => x.Number))
                .ForMember(x => x.ProviderName, opt => opt.MapFrom(x => x.Provider != null ?
                    x.Provider.Name : ""))
                .ForMember(x => x.CountItems, opt => opt.MapFrom(x => x.OrderItems.Count))
                .ForMember(x => x.Date, opt => opt.MapFrom(x => x.Date));
            });

            List<OrderDto> orderDtos = new Mapper(config)
                .Map<IQueryable<Order>, List<OrderDto>>(orders);

            return orderDtos;
        }

        /// <summary>
        /// Получает список всех поставщиков
        /// </summary>
        /// <returns>Список поставщиков</returns>
        public async Task<List<ProviderDto>> GetAllOrderProvidersAsync()
        {
            List<Provider> providers = await _appContext.Providers
                .ToListAsync();

            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<Provider, ProviderDto>();
            });

            List<ProviderDto> providerDtos = new Mapper(config)
                .Map<List<Provider>, List<ProviderDto>>(providers);

            return providerDtos;
        }
    }
}
