using Microsoft.AspNetCore.Mvc;
using WA.Application.Contracts.Request.RequestCustomer;
using WA.Domain.Entities;

namespace WA.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerByCpf(string cpf);
        Task<Customer> GetCustomerById(int id);
        ValueTask<IActionResult> RegisterCustomer(CustomerPostRequest request);
        Task<IActionResult> UpdateCustomer(CustomerPutRequest request);
        Task<IActionResult> RemoveCustomer(int id);
        Task<IActionResult> GetAllCustomers();
    }
}
