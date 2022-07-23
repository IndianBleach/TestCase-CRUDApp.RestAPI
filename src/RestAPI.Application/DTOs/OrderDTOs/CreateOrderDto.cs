using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Application.DTOs.OrderDTOs
{
    public class CreateOrderDto
    {
        [Required]
        public string? Number { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public int? ProviderId { get; set; }
    }
}
