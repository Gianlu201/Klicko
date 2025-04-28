using Klicko_be.DTOs.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Klicko_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly string _stripeSecretKey;

        //private readonly IConfiguration _configuration;

        public PaymentsController(IConfiguration configuration)
        {
            _stripeSecretKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");

            if (string.IsNullOrEmpty(_stripeSecretKey))
            {
                throw new Exception("Stripe secret key is not configured.");
            }

            //_configuration = configuration;
            //StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            StripeConfiguration.ApiKey = _stripeSecretKey;
        }

        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] PaymentRequestDto request)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = request.Amount,
                Currency = "eur",
                PaymentMethodTypes = new List<string> { "card" },
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return Ok(new { clientSecret = paymentIntent.ClientSecret });
        }
    }
}
