using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Criterion;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using WA.Application.Contracts.Response.ResponsePayment;
using WA.Application.Interfaces;
using WA.Domain.Entities;
using WA.Domain.Interfaces;
using static WA.Application.Contracts.Request.RequestPayment.PaymentRequest;
using WA.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.Net;
using WA.Domain.Base;

namespace WA.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly HttpClient _httpclient;

        public PaymentService(IMapper mapper, IPaymentRepository paymentRepository, IOrderRepository orderRepository, HttpClient httpclient)
        {
            _mapper = mapper;
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _httpclient = httpclient;
        }

        public async Task<QRCodeResponse> GenerateQrCodePaymentAsync(string orderNumber)
        {

            var order = await _orderRepository.GetOrderByFilterAsync(o => o.OrderNumber == orderNumber);
            var paymentQr = await _paymentRepository.GetPaymentByOrderNumberAsync(order.Id);

            if (paymentQr != null)
            {
                return new QRCodeResponse() { InStoreOrderId = paymentQr.InStoreOrderId, QrData = paymentQr.QrData };
            }

            OrderDto mercado = new OrderDto();
            mercado.total_amount = order.Orderitems.Sum(x => x.Price.Value * x.Quantity.Value);
            mercado.description = "Pedido compra alimentos";
            mercado.title = $"Venda lanchonete - pedido {orderNumber}";
            mercado.external_reference = $"reference_{order.OrderNumber}";
            mercado.cash_out.amount = 0;
            mercado.notification_url = "https://www.yourserver.com/notifications";

            var orderItems = order.Orderitems.ToList();
            mercado.items = orderItems.Select(orderItem => new OrderDto.Item
            {
                sku_number = orderItem.Product.Id.ToString(),
                category = "marketplace",
                title = orderItem.Product.Name,
                description = orderItem.Product.Description,
                unit_price = (decimal)orderItem.Price,
                quantity = (int)orderItem.Quantity,
                unit_measure = "unit",
                total_amount = orderItem.Price.Value * orderItem.Quantity.Value
            }).ToList();

            try
            {
                var apiUrl = "https://api.mercadopago.com/instore/orders/qr/seller/collectors/186249149/pos/wa001POS001/qrs";
                var jsonData = JsonSerializer.Serialize(mercado);
                var content = new StringContent(jsonData, Encoding.UTF8, "aplication/json");
                // Adiciona o cabeçalho Authorization
                _httpclient.DefaultRequestHeaders.Add("Authorization", "Bearer TEST-7611799853194381-052816-ced911c8c14e503ce144bc29765a32e8-186249149");

                HttpResponseMessage response = await _httpclient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var qrCodeDtoResponse = JsonSerializer.Deserialize<QRCodeResponse>(responseData);

                    Payment payment = new Payment();
                    payment.PaymentStatus = (int)EnumStatusPayment.Pendente;
                    payment.OrderId = order.Id;
                    payment.InStoreOrderId = qrCodeDtoResponse.InStoreOrderId;
                    payment.QrData = qrCodeDtoResponse.QrData;
                    payment.PaymentDate = DateTime.Now;
                    payment.PaymentMethod = "Pix";

                    await _paymentRepository.CreateAsync(payment);

                    return qrCodeDtoResponse;
                }
                else
                    throw new HttpRequestException($"{(int)response.StatusCode} - Erro ao acessar a API externa");
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        public async Task<IActionResult> WebhookUptadeStatusPaymentAsync(string orderNumber, int statusPagamento)
        {
            var order = await _orderRepository.GetOrderByFilterAsync(o => o.OrderNumber == orderNumber);
            if(order == null)
            {
                return new ResultObject(HttpStatusCode.NotFound, new { Error = $"Pedido com número: {orderNumber}, não encontrado." });

            }
            var payment = await _paymentRepository.GetPaymentByOrderNumberAsync(order.Id);

            var status = (EnumStatusPayment)statusPagamento;
            if (Enum.IsDefined(typeof(EnumStatusPayment), statusPagamento))
            {
                payment.PaymentStatus = statusPagamento;
                payment.PaymentDateProcessed = DateTime.Now;

                await _paymentRepository.UpdateAsync(payment);
            }
            else
            {
                return new ResultObject(HttpStatusCode.BadRequest, new { Error = "Status de pagamento não mapeado, status válidos: 1 - Pagamento Pendente | 2 - Aprovado | 3 - Recusado" });
            }
            return new ResultObject(HttpStatusCode.OK, new {Message = $"Pagamento do Pedido {orderNumber} com status: {status}, atualizado com sucesso!"});
        }


        public ValueTask<IActionResult> AddPaymente(Payment payment)
        {
            throw new NotImplementedException();
        }

        public Task<Payment> GetPaymentByStatus(string status)
        {
            throw new NotImplementedException();
        }
    }
}
