using MiniStyleWebConsole.Infrastructure.IService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Service
{
    public class ProductPriceService : IProductPriceService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;

        public ProductPriceService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
        }

        public async Task<Domain.ProductPrice> GetProductPrice(int productId)
        {
            var result = await _httpClientService.GetAsync<Domain.ProductPrice>(
            url: $"{_baseUrl}/ProductPrice/ProductId/{productId}",
                token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task<Domain.ProductPrice> SaveProductPrice(Domain.ProductPrice mProductPrice)
        {
            return await _httpClientService.PostAsync<Domain.ProductPrice>(
                url: $"{_baseUrl}/ProductPrice",
                obj: mProductPrice,
                token: _token
            ).ConfigureAwait(false);
        }


    }
}
