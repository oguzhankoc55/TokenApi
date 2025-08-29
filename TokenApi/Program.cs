using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TokenApi.Services;

var httpClient = new HttpClient();
var tokenService = new TokenService(httpClient);
var orderService = new OrderService(httpClient, tokenService);

Console.WriteLine("Sipari� listesi servis ba�lad�...");

while (true)
{
    try
    {
        await orderService.GetOrdersAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Hata: {ex.Message}");
    }

    // 5 dakika bekle
    Thread.Sleep(TimeSpan.FromMinutes(5));
}
