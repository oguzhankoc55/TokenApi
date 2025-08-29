using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TokenApi.Models;

namespace TokenApi.Services
{
    public class TokenService
    {
        private readonly HttpClient _httpClient;
        private TokenResponse _cachedToken;
        private DateTime _tokenExpiryTime;

        public TokenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetTokenAsync()
        {
            if (_cachedToken != null && DateTime.UtcNow < _tokenExpiryTime)
                return _cachedToken.access_token;

            _cachedToken = new TokenResponse
            {
                access_token = Guid.NewGuid().ToString(),
                token_type = "Bearer",
                expires_in = 3600 // 1 saat
            };

            _tokenExpiryTime = DateTime.UtcNow.AddSeconds(_cachedToken.expires_in - 30);

            Console.WriteLine($"Yeni token alındı: {_cachedToken.access_token}");

            return _cachedToken.access_token;
        }
    }
}
