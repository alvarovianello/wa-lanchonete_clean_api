using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WA.Application.Contracts.Request.RequestCustomer;
using WA.Application.Interfaces;
using WA.Application.Validators.ValidatorsCustomer;
using WA.Domain.Base;
using WA.Domain.Entities;
using WA.Domain.Interfaces;

namespace WA.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async ValueTask<IActionResult> RegisterCustomer(CustomerPostRequest customerRequest)
        {
            try
            {
                var validator = await new CustomerPostRequestValidator().ValidateAsync(customerRequest);

                if (!validator.IsValid)
                    return new ResultObject(HttpStatusCode.BadRequest, validator);

                var returnGetCustomerByCpf = await _customerRepository.GetCustomerByCPF(customerRequest.Cpf);

                if (returnGetCustomerByCpf == null)
                {
                    Customer customer = _mapper.Map<Customer>(customerRequest);
                    await _customerRepository.AddCustomer(customer);

                    if (customer != null)
                        return new ResultObject(HttpStatusCode.OK, new { Success = "Conta cadastrada com sucesso" });
                    else
                        return new ResultObject(HttpStatusCode.BadRequest, new { Error = "Houve um erro ao realizar o cadastro da conta" });
                }
                else
                    return new ResultObject(HttpStatusCode.AlreadyReported, new { Warn = "O CPF informado já possui cadastro" });
            }
            catch (Exception ex)
            {
                return new ResultObject(HttpStatusCode.InternalServerError, new { Error = ex.Message });
            }
        }

        public async Task<Customer> GetCustomerByCpf(string cpf)
        {
            return await _customerRepository.GetCustomerByCPF(cpf);
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _customerRepository.GetCustomerById(id);
        }

        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var listCustomers = await _customerRepository.GetAllCustomer();
                return new ResultObject(HttpStatusCode.OK, listCustomers);
            }
            catch (Exception ex)
            {

                return new ResultObject(HttpStatusCode.BadRequest, ex);
            }
        }

        public async Task<IActionResult> UpdateCustomer(CustomerPutRequest customerPutRequest)
        {
            var validator = await new CustomerPutRequestValidator().ValidateAsync(customerPutRequest);

            if (!validator.IsValid)
                return new ResultObject(HttpStatusCode.BadRequest, validator);

            try
            {
                var returnGetCustomerByCpf = await _customerRepository.GetCustomerByCPF(customerPutRequest.Cpf);

                if (returnGetCustomerByCpf == null)
                    return new ResultObject(HttpStatusCode.AlreadyReported, new { Warn = "Cadastro de cliente não encontrado" });

                returnGetCustomerByCpf.Name = customerPutRequest.Name;
                returnGetCustomerByCpf.Email = customerPutRequest.Email;

                await _customerRepository.UpdateCustomer(returnGetCustomerByCpf);

                if (returnGetCustomerByCpf == null)
                    return new ResultObject(HttpStatusCode.BadRequest, new { Error = "Houve um erro ao realizar o cadastro da conta" });

                return new ResultObject(HttpStatusCode.OK, new { Success = "Dados cadastrais alterados com sucesso" });
            }
            catch (Exception ex)
            {
                return new ResultObject(HttpStatusCode.InternalServerError, new { Error = ex.Message });
            }
        }

        public async Task<IActionResult> RemoveCustomer(int id)
        {
            try
            {
                _customerRepository.RemoveCustomer(id);
                return new ResultObject(HttpStatusCode.OK, new { Success = "Conta removida com sucesso" });
            }
            catch (Exception ex)
            {
                return new ResultObject(HttpStatusCode.InternalServerError, new { Error = ex.Message });
            }
        }
    }
}
