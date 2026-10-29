using MiniStyleWebConsole.Infrastructure.IService;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Service
{
    public class ColorService : IColorService
    {
        #region Declaration

        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public ColorService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        #endregion

        #region Public Method
        public async Task<Domain.ColorLister> GetAllAsync(Domain.ColorLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.ColorLister, Domain.ColorLister>(
                url: $"{_baseUrl}/Color/GetAll",
                obj: mLister,
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.Color> CreateAsync(Domain.Color mColor)
        {
            return await _httpClientService.PostAsync<Domain.Color, Domain.Color>(
                url: $"{_baseUrl}/Color",
                obj: mColor,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.Color> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.Color>(
            url: $"{_baseUrl}/Color/{id}",
                token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                url: $"{_baseUrl}/Color/{id}/LoginUser/{_loginUserId}",
                token: _token
             ).ConfigureAwait(false);

        }


        public async Task<List<Domain.Color>> GetAllActiveColor()
        {
            var result = await _httpClientService.GetAsync<List<Domain.Color>>(
                url: $"{_baseUrl}/Color/GetAllActiveColor",
                token: _token

            ).ConfigureAwait(false);
            return result;
        }



        public async Task<List<Domain.ProductColor>> GetProductColor(int productId)
        {
            var result = await _httpClientService.GetAsync<List<Domain.ProductColor>>(
                url: $"{_baseUrl}/ProductColor/ProductId/{productId}",
                token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task SaveProductColor(List<Domain.ProductColor> mProductColors)
        {
            await _httpClientService.PutAsync<List<Domain.ProductColor>>(
                url: $"{_baseUrl}/ProductColor",
                obj: mProductColors,
                token: _token
            ).ConfigureAwait(false);
        }



        #endregion
    }
}
