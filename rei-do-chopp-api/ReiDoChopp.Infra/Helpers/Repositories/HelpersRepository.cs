using Microsoft.Extensions.Configuration;
using ReiDoChopp.Infra.Helpers.Repositories;
using System.Text.Json;
using System.Text;

namespace ReiDoChopp.Domain.Helpers.Repositories
{
    public class HelpersRepository : IHelpersRepository
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public HelpersRepository(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<bool> PrintAsync(string text)
        {
            try
            {
                httpClient.BaseAddress = new Uri(configuration["Helper:Url"]!);

                var content = new StringContent(
                    JsonSerializer.Serialize(new { text }),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await httpClient.PostAsync("/api/print", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
