using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Application.DTOs.OrderDTOs
{
    public class UpdateOrderDto
    {
        public string? Number { get; set; }
        public DateTime? Date { get; set; }
        public int? ProviderId { get; set; }
    }
}
