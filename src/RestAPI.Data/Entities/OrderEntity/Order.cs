using RestAPI.Data.Entities.OrderItemEntity;
using RestAPI.Data.Entities.ProviderEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.Data.Entities.OrderEntity
{
    public class Order : BaseEntity
    {
        [Column(TypeName = "nvarchar(max)")]
        public string Number { get; set; }
        [Column(TypeName = "datetime2(7)")]
        public DateTime Date { get; set; } 
        public int ProviderId { get; set; }
        public Provider? Provider { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        public Order(string number,
            DateTime date,
            int providerId)
        {
            
            Number = number;
            Date = date;
            ProviderId = providerId;
            OrderItems = new List<OrderItem>();
        }
    }
}