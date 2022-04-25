
namespace Domain.Aggregates.Order
{
    /// <summary>
    /// value type object should be immutable and should not have an identity
    /// </summary>
    public class OrderItem
    {
        public string ProductId { get; private set; }

        public decimal UnitPrice { get; private set; }

        public int Quantity { get; private set; }

        internal OrderItem(string productId, decimal unitPrice, int quantity)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public decimal GetPrice()
        {
            return Quantity * UnitPrice;
        }
    }
}