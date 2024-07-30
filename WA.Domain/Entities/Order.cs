namespace WA.Domain.Entities
{
    public partial class Order
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; }

        public string? OrderNumber { get; set; }

        public decimal? TotalPrice { get; set; }

        public string? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual Customer? Customer { get; set; }

        public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

        public virtual ICollection<Orderstatus> Orderstatuses { get; set; } = new List<Orderstatus>();

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }

}
