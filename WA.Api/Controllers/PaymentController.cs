using Microsoft.AspNetCore.Mvc;
using SkiaSharp;
using WA.Application.Contracts.Request.RequestPayment;
using WA.Application.Contracts.Response.ResponsePayment;
using WA.Application.Interfaces;
using ZXing;
using ZXing.SkiaSharp;
using ZXing.SkiaSharp.Rendering;

namespace WA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
                
        }

        [HttpGet("{orderNumber}/qrCodePayment")]
        public async Task<IActionResult> GetOrderByOrderNumber(string orderNumber)
        {
            QRCodeResponse createdOrder = await _paymentService.GenerateQrCodePaymentAsync(orderNumber);

            return Ok(createdOrder);

            //var qrCodeImage = GenerateQRCodeImage(createdOrder.QrData);
            //return File(qrCodeImage, "image/png");
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> WebhookUptadeStatusPaymentAsync(PaymentRequest.PaymentWebhook request)
        {
            var returnUpdatePayment = await _paymentService.WebhookUptadeStatusPaymentAsync(request.orderNumber, request.statusPagamento);

            if (returnUpdatePayment == null)
                return NotFound();

            return returnUpdatePayment;
        }
        private byte[] GenerateQRCodeImage(string text)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = 300,
                    Width = 300
                },
                Renderer = new SKBitmapRenderer()
            };

            using (var bitmap = writer.Write(text))
            {
                using (var image = SKImage.FromBitmap(bitmap))
                {
                    using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                    {
                        return data.ToArray();
                    }
                }
            }
        }
    }
}
