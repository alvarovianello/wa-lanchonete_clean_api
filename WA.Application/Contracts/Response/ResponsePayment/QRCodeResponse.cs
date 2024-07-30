using System.Text.Json.Serialization;

namespace WA.Application.Contracts.Response.ResponsePayment
{
    public class QRCodeResponse
    {
        [JsonPropertyName("in_store_order_id")]
        public string? InStoreOrderId { get; set; }

        [JsonPropertyName("qr_data")]
        public string? QrData { get; set; }
    }
}
