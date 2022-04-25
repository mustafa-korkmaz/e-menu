
namespace Presentation.ViewModels.Order
{
    public class OrderViewModel : ViewModelBase
    {
        public decimal Price { get; set; } 

        public string Username { get; set; }

        public IReadOnlyCollection<OrderItemViewModel> Items { get; set; }
    }

    public class OrderItemViewModel
    {
        public string ProductId { get; private set; }

        public decimal UnitPrice { get; private set; }

        public int Quantity { get; private set; }
    }
}