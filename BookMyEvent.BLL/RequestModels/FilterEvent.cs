

namespace BookMyEvent.BLL.RequestModels
{
    public class FilterEvent
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public decimal startPrice { get; set; }
        public decimal endPrice { get; set; }
        public string? location { get; set; }
        public bool isFree { get; set; }
        public List<int>? categoryIds { get; set; }
        public int pageNumber  {get;set;}
        public int pageSize { get; set; }
    }
}
