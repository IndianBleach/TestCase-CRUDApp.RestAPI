using RestAPI.Application.DTOs.OrderItemDTOs;
using RestAPI.Application.DTOs.ProviderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Application.DTOs.OrderDTOs
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public ProviderDto Provider { get; set; }
    }
}
