using Microsoft.AspNetCore.Mvc;
using WA.Application.Contracts.Request.RequestCustomer;
using WA.Application.Interfaces;

namespace WA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCustomer(CustomerPostRequest customer)
        {
            var returnRegisterCustomer = await _customerService.RegisterCustomer(customer);

            if (returnRegisterCustomer == null)
                return NotFound();

            return returnRegisterCustomer;
        }

        [HttpGet("GetByCpf/{cpf}")]
        public async Task<IActionResult> GetCustomerByCpf(string cpf)
        {
            var customer = await _customerService.GetCustomerByCpf(cpf);

            if (customer == null)
                return NotFound(new { Info = "CPF não encontrado" });

            return Ok(customer);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerById(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            return await _customerService.GetAllCustomers();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(CustomerPutRequest customer)
        {
            var returnUpdateCustomer = await _customerService.UpdateCustomer(customer);

            if (returnUpdateCustomer == null)
                return NotFound();

            return returnUpdateCustomer;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCustomer(int id)
        {
            var returnUpdateCustomer = await _customerService.RemoveCustomer(id);

            if (returnUpdateCustomer == null)
                return NotFound();

            return Ok();
        }
    }
}

