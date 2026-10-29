using MiniStyleWebConsole.Infrastructure.IService;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Service
{
    public class OrderLineService : IOrderLineService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;

        public OrderLineService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
        }


        public async Task<List<Domain.OrderLine>> GetByAsync(int id)
        {
            return await _httpClientService.GetAsync<List<Domain.OrderLine>>(
            url: $"{_baseUrl}/OrderLine/{id}",
                token: _token
            ).ConfigureAwait(false);
        }


    }
}
