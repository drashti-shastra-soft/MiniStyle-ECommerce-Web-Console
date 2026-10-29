using MiniStyleWebConsole.Infrastructure.IService;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Service
{
    public class OrderService : IOrderService
    {

        #region Declaration
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;

        public OrderService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
        }
        #endregion

        #region Public Method
        public async Task<Domain.OrderLister> GetAllAsync(Domain.OrderLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.OrderLister>(
                url: $"{_baseUrl}/Order/GetAll",
                obj: mLister,
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.Order> CreateAsync(Domain.Order mOrder)
        {
            return await _httpClientService.PostAsync<Domain.Order>(
                url: $"{_baseUrl}/Order",
                obj: mOrder,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.Order> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.Order>(
            url: $"{_baseUrl}/Order/{id}",
                token: _token
            ).ConfigureAwait(false);
            return result;
        }
        #endregion

    }
}
