﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Application.DTOs.OrderItemDTOs
{
    public class UpdateOrderItemDto
    {        
        public string? Unit { get; set; }
        public string? Name { get; set; }
        public string? Quantity { get; set; }
    }
}