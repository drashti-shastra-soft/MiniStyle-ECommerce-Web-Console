using MiniStyleWebConsole.Infrastructure.IService;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Service
{
    public class ProductSizeChartService : IProductSizeChartService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;

        public ProductSizeChartService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
        }

        public async Task<List<Domain.ProductSizeChart>> Get(int productId, string unitType)
        {
            var result = await _httpClientService.GetAsync<List<Domain.ProductSizeChart>>(
                url: $"{_baseUrl}/ProductSizeChart/ProductId/{productId}/UnitType/{unitType}",
                token: _token
                ).ConfigureAwait(false);
            return result;
        }

        public async Task<Domain.ProductSizeChart> Upsert(Domain.ProductSizeChart mProductSizeChart)
        {
            return await _httpClientService.PostAsync<Domain.ProductSizeChart>(
                url: $"{_baseUrl}/ProductSizeChart",
                obj: mProductSizeChart,
                token: _token
            ).ConfigureAwait(false);
        }

    }
}
