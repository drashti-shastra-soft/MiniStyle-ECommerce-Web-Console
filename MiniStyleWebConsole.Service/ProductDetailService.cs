using MiniStyleWebConsole.Infrastructure.IService;
using System.Configuration;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Service
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;

        public ProductDetailService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
        }

        public async Task<Domain.ProductDetail> UpsertAsync(Domain.ProductDetail mProductDetail)
        {
            return await _httpClientService.PostAsync<Domain.ProductDetail>(
                url: $"{_baseUrl}/ProductDetail",
                obj: mProductDetail,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.ProductDetail> GetAsync(int productId)
        {
            var result = await _httpClientService.GetAsync<Domain.ProductDetail>(
            url: $"{_baseUrl}/ProductDetail/ProductId/{productId}",
                token: _token
            ).ConfigureAwait(false);
            return result;
        }
    }
}
