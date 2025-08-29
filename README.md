# TokenApi

**TokenApi**, her 5 dakikada bir sipariş listesini çeken ve API token yönetimini gerçekleştiren bir .NET 6/7 konsol uygulamasıdır. Proje, token önbellekleme ve geçerlilik süresini kontrol etme mekanizmaları ile token isteklerini optimize eder. Dummy verilerle kolayca test edilebilir ve gerçek API'lere hızlıca entegre edilebilir.

---

## Özellikler

- **API Token Yönetimi:** Token önbellekleme ve geçerlilik süresi kontrolü ile gereksiz istekleri engeller.  
- **Otomatik Veri Çekme:** Her 5 dakikada bir sipariş listesini çeker.  
- **Esnek Yapı:** Dummy API ile test edilebilir ve gerçek API'lere kolay geçiş sağlar.  
- **Asenkron Çalışma:** async/await kullanımı sayesinde uygulama bekleme sırasında bloklanmaz.  

---

## Proje Yapısı



- **Program.cs:** Uygulamanın ana döngüsünü ve servisleri başlatır.  
- **Models/TokenResponse.cs:** API'den dönen token verilerini temsil eden model sınıfıdır.  
- **Services/TokenService.cs:** Token alımı, önbelleğe alma ve süresini kontrol etme işlemlerini yönetir.  
- **Services/OrderService.cs:** Token'ı kullanarak sipariş verilerini çeker.  

---

## Kurulum ve Çalıştırma

1. **Projeyi klonlayın:**


git clone <repo-url>
cd TokenApi
Gerekli NuGet paketlerini yükleyin:

dotnet restore
Uygulamayı çalıştırın:


dotnet run
Konsol çıktısı, her 5 dakikada bir güncel sipariş listesini gösterecektir.

Token yönetimi tamamen otomatiktir.

Program.cs
var httpClient = new HttpClient();
var tokenService = new TokenService(httpClient);
var orderService = new OrderService(httpClient, tokenService);

Console.WriteLine("Sipariş listesi servisi başladı...");

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

  await Task.Delay(TimeSpan.FromMinutes(5));
}
Gerçek API'ye Geçiş
Projeyi kendi API'nizle kullanmak oldukça basittir:

TokenService.GetTokenAsync() metodunda, sahte token üretimi yerine kendi token endpoint'inize istek yapın.

OrderService.GetOrdersAsync() metodundaki dummy veri döndürme işlemini kaldırarak aşağıdaki gibi gerçek bir API çağrısı kullanın:

var ordersFromApi = await _httpClient.GetFromJsonAsync<Order[]>("https://api.yourapi.com/orders");
## Notlar
Token süresi expires_in parametresi ile saniye cinsinden belirtilmiştir.

Token yenilenirken 30 saniye önceden yenileme yapılır, böylece API limit hataları önlenir.

Task.Delay kullanıldığı için uygulama async olarak çalışır ve Thread’i bloklamaz.
