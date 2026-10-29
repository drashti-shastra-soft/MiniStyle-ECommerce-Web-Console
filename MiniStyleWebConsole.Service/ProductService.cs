using MiniStyleWebConsole.Infrastructure.IService;
using System.Threading.Tasks;
using System.Configuration;

namespace MiniStyleWebConsole.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;
        public ProductService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<Domain.ProductLister> GetAllAsync(Domain.ProductLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.ProductLister>(
                url: $"{_baseUrl}/Product/GetAll",
                obj: mLister,
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.Product> CreateAsync(Domain.Product mProduct)
        {
            return await _httpClientService.PostAsync<Domain.Product>(
                url: $"{_baseUrl}/Product",
                obj: mProduct,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.Product> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.Product>(
            url: $"{_baseUrl}/Product/{id}",
                token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                url: $"{_baseUrl}/Product/{id}/LoginUser/{_loginUserId}",
                token: _token
             ).ConfigureAwait(false);

        }
    }
}
