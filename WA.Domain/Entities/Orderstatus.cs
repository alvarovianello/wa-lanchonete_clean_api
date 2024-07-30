namespace WA.Domain.Entities
{
    public partial class Orderstatus
    {
        public int Id { get; set; }

        public int? OrderId { get; set; }

        public string? Status { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual Order? Order { get; set; }
    }

}
