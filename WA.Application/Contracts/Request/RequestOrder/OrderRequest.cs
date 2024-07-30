namespace WA.Application.Contracts.Request.RequestOrder
{
    public class OrderRequest
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public ICollection<OrderItemRequest> OrderItems { get; set; }
    }
}
