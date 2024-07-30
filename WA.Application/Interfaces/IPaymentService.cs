using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WA.Application.Contracts.Response.ResponsePayment;
using WA.Domain.Entities;

namespace WA.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<QRCodeResponse> GenerateQrCodePaymentAsync(string orderNumber);
        Task<IActionResult> WebhookUptadeStatusPaymentAsync(string orderNumber, int statusPagamento);
        ValueTask<IActionResult> AddPaymente(Payment payment);
        Task<Payment> GetPaymentByStatus(string status);
    }
}
