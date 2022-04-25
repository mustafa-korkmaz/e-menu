using Presentation.Middlewares.Validations;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.Order
{
    public class AddEditOrderViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public ICollection<AddEditOrderItemViewModel> Items { get; set; }
    }

    public class AddEditOrderItemViewModel
    {
        [ObjectIdValidation]
        public string ProductId { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
