namespace WA.Application.Contracts.Request.RequestOrder
{
    public class OrderPostRequest
    {
        public int CustomerId { get; set; }
        public ICollection<OrderItemRequest> OrderItems { get; set; }
    }
}
