using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using ReiDoChopp.Domain.Emails.Repositories;
using ReiDoChopp.Domain.Emails.Repositories.Models;

namespace ReiDoChopp.Infra.Emails.Repositories
{
    public class EmailsRepository : IEmailsRepository
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly MailjetClient mailjetClient;

        public EmailsRepository(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            this.mailjetClient = new MailjetClient(configuration["Email:ApiKeys:Public"], configuration["Email:ApiKeys:Private"]);
        }

        public async Task SendEmailAsync(EmailSendCommand emailSendModel)
        {
            object email = new
            {
                Messages = new[]
                {
                    new {
                        From = new {Email = configuration["Email:Sender:Adress"], Name = configuration["Email:Sender:Name"]},
                        To = new[] {new { Email = emailSendModel.RecipientAdress, Name = emailSendModel.RecipientName } },
                        Subject = emailSendModel.Subject,
                        TextPart = emailSendModel.IsHtml ? null : emailSendModel.Text,
                        HTMLPart = !emailSendModel.IsHtml ? null : emailSendModel.Text,
                    }
                }
            };

            MailjetRequest request = new MailjetRequest { Body = JObject.FromObject(email), Resource = SendV31.Resource };

            MailjetResponse response = await mailjetClient.PostAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Erro ao enviar e-mail: {response.StatusCode} - {response.GetErrorMessage()}");
            }

        }
    }
}
