using MiniStyleWebConsole.Infrastructure.IService;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Configuration;

namespace MiniStyleWebConsole.Service
{
    public class ProductImageService : IProductImageService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;

        public ProductImageService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
        }

        public async Task<Domain.ProductImage> SaveProductImage(Domain.ProductImage mProductImage)
        {
            return await _httpClientService.PostAsync<Domain.ProductImage>(
                url: $"{_baseUrl}/ProductImage",
                obj: mProductImage,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<List<Domain.ProductImage>> GetProductImages(int productId)
        {
            var result = await _httpClientService.GetAsync<List<Domain.ProductImage>>(
                url: $"{_baseUrl}/ProductImage/ProductId/{productId}",
                token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task<List<Domain.ProductImage>> GetProductImages(int productId, int productColorId)
        {
            var result = await _httpClientService.GetAsync<List<Domain.ProductImage>>(
                url: $"{_baseUrl}/ProductImage/ProductId/{productId}/ProductColorId/{productColorId}",
                token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteProductImage(int id)
        {
            await _httpClientService.DeleteAsync(
                url: $"{_baseUrl}/ProductImage/{id}",
                token: _token
             ).ConfigureAwait(false);

        }

        //public async Task<List<Domain.Color>> GetAllActiveColor()
        //{
        //    var result = await _httpClientService.GetAsync<List<Domain.Color>>(
        //        url: $"{_baseUrl}/Color/GetAllActiveColor",
        //        token: _token

        //    ).ConfigureAwait(false);
        //    return result;
        //}



    }
}
