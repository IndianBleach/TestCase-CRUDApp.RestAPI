using RestAPI.Data.Entities.OrderEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Data.Entities.OrderItemEntity
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Unit { get; set; }

        public OrderItem(int orderId,
            string name,
            decimal quantity,
            string unit)
        {
            OrderId = orderId;
            Name = name;
            Quantity = quantity;
            Unit = unit;
        }
    }
}
