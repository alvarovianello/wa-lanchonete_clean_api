namespace WA.Application.Contracts.Request.RequestCategory
{
    public class CategoryPutRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
