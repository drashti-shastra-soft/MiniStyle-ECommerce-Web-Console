using System.Threading.Tasks;
using System.Configuration;
using MiniStyleWebConsole.Infrastructure.IService;

namespace MiniStyleWebConsole.Infrastructure.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _apiBaseUrl;
        public AuthenticationService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _apiBaseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");
        }

        public async Task<Domain.User> AuthenticateAsync(Domain.Credential mCredential)
        {
            return await _httpClientService.PostAsync<Domain.Credential, Domain.User>(
                url: $"{_apiBaseUrl}/Authentication",
                obj: mCredential
            );
        }
    }
}
