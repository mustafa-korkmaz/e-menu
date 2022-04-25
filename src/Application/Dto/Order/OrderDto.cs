
namespace Application.Dto.Order
{
    public class OrderDto : DtoBase
    {
        public string Username { get; set; }

        public decimal Price { get; set; }

        public ICollection<OrderItemDto> Items { get; set; }
    }
}
