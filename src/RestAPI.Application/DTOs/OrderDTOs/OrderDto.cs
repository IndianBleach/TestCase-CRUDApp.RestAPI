using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Application.DTOs.OrderDTOs
{
    public class OrderDto
    {
        public string Id { get; set; }
        public string CountItems { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public string ProviderName { get; set; }
    }
}
