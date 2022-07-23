using RestAPI.Application.DTOs.FilterDTOs;
using RestAPI.Application.DTOs.OrderDTOs;
using RestAPI.Application.DTOs.OrderItemDTOs;
using RestAPI.Application.DTOs.ProviderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Application.DTOs.HomeDTOs
{
    public class HomeDto
    {
        public ICollection<UnitDto> Units { get; set; }
        public ICollection<ProviderDto> Providers { get; set; }
        public ICollection<OrderDto> Orders { get; set; }
        public SearchOrdersFilterDto Filter { get; set; }

        public HomeDto(ICollection<OrderDto> orderDtos,
            ICollection<ProviderDto> providers,
            SearchOrdersFilterDto filter,
            ICollection<UnitDto> units)
        {
            Units = units;
            Filter = filter;
            Orders = orderDtos;
            Providers = providers;
        }
    }
}
