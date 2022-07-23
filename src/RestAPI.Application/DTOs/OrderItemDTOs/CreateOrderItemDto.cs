using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Application.DTOs.OrderItemDTOs
{
    public class CreateOrderItemDto
    {
        public int OrderId { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
    }
}
