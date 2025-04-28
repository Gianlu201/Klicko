using System.Net.Mail;
using Klicko_be.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Klicko_be.Services
{
    public class EmailService
    {
        private static readonly string ApiKey = Environment.GetEnvironmentVariable(
            "SENDGRID_API_KEY"
        );

        public static async Task<bool> SendEmailOrderConfirmationAsync(
            string toEmail,
            string subject,
            Order order
        )
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                Console.WriteLine(
                    "Errore: La variabile d'ambiente SENDGRID_API_KEY non è impostata."
                );
                return false;
            }

            try
            {
                var client = new SendGridClient(ApiKey);
                var from = new EmailAddress("noreply.gididi@gmail.com", "Klicko");
                var to = new EmailAddress(toEmail);

                var htmlContent = File.ReadAllText(
                    "../Klicko_be/EmailTemplate/OrderConfirmation.html"
                );

                var vouchersContent = "";
                if (order.Vouchers != null)
                {
                    foreach (var voucher in order.Vouchers)
                    {
                        vouchersContent +=
                            $"<div style=\"background-color: #e2e8f0; padding: 10px; border-radius: 4px; margin-top: 10px;\">\r\n<strong>{voucher.Title}</strong><br />\r\n<small>{voucher.VoucherCode}</small>\r\n</div>";
                    }
                }

                htmlContent = htmlContent
                    .Replace("order-number", order.OrderNumber.ToString())
                    .Replace("order-vouchers", vouchersContent);

                var msg = MailHelper.CreateSingleEmail(
                    from,
                    to,
                    subject,
                    plainTextContent: null,
                    htmlContent
                );
                var response = await client.SendEmailAsync(msg);

                return response.StatusCode == System.Net.HttpStatusCode.Accepted;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore invio email: {ex.Message}");
                return false;
            }
        }

        public static async Task<bool> SendNewsLetterAsync(string toEmail)
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                Console.WriteLine(
                    "Errore: La variabile d'ambiente SENDGRID_API_KEY non è impostata."
                );
                return false;
            }

            try
            {
                var client = new SendGridClient(ApiKey);
                var from = new EmailAddress("noreply.gididi@gmail.com", "Klicko");
                var to = new EmailAddress(toEmail);

                // Qui inserisci l'HTML creato prima
                var htmlContent = File.ReadAllText(
                    "../Klicko_be/EmailTemplate/NewsLetterSubscription.html"
                );

                var msg = MailHelper.CreateSingleEmail(
                    from,
                    to,
                    "Benvenuto nella nostra NewsLetter",
                    plainTextContent: null,
                    htmlContent
                );
                var response = await client.SendEmailAsync(msg);

                return response.StatusCode == System.Net.HttpStatusCode.Accepted;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore invio email: {ex.Message}");
                return false;
            }
        }
    }
}
