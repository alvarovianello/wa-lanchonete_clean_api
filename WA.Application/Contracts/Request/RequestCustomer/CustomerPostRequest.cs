namespace WA.Application.Contracts.Request.RequestCustomer
{
    public class CustomerPostRequest
    {
        public string Name { get; set; } = null!;

        public string Cpf { get; set; } = null!;

        public string? Email { get; set; }
    }
}
