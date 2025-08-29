using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TokenApi.Services;

namespace TokenApi.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        private readonly TokenService _tokenService;

        public OrderService(HttpClient httpClient, TokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public async Task GetOrdersAsync()
        {
            var token = await _tokenService.GetTokenAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var orders = new[]
            {
                new { Id = 1, Customer = "Ali", Amount = 100 },
                new { Id = 2, Customer = "Ayşe", Amount = 200 },
                new { Id = 3, Customer = "Mehmet", Amount = 150 }
            };

            Console.WriteLine("Siparişler:");
            foreach (var order in orders)
            {
                Console.WriteLine($"Id: {order.Id}, Customer: {order.Customer}, Amount: {order.Amount}");
            }

            
        }
    }
}
