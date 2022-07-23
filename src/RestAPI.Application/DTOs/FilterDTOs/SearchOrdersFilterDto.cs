using RestAPI.Domain.Enums;

namespace RestAPI.Application.DTOs.FilterDTOs
{    
    public class OrderSortType
    { 
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class SearchOrdersFilterDto
    {
        public ICollection<string> SelectedUnits { get; set; }
        public ICollection<int> SelectedProviders { get; set; }
        public int? SelectedSortValue { get; set; }
        public ICollection<OrderSortType> SortTypes { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? DateStart { get; set; }

        public SearchOrdersFilterDto()
        {
            SortTypes = Enum.GetValues(typeof(OrdersSortEnum)).Cast<OrdersSortEnum>().ToList()
                .Select(x => new OrderSortType()
                { 
                    Name = x.ToString(),
                    Value = (int)x
                })
                .ToList();

            DateEnd = DateTime.Now;
            DateStart = DateTime.Now.AddMonths(-1);
            SelectedSortValue = null;
            SelectedProviders = new List<int>();
            SelectedUnits = new List<string>();
        }
    }
}
