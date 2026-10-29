using MiniStyleWebConsole.Infrastructure.IService;
using System.Threading.Tasks;
using System.Configuration;
using System;
using System.Linq;

namespace MiniStyleWebConsole.Service
{
    public class MicroCategoryService : IMicroCategoryService
    {
        #region Declaration

        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public MicroCategoryService(IHttpClientService httpClientService)
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
        public async Task<Domain.MicroCategoryLister> GetAllAsync(Domain.MicroCategoryLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.MicroCategoryLister, Domain.MicroCategoryLister>(
                url: $"{_baseUrl}/MicroCategory/GetAll",
                obj: mLister,
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.MicroCategory> CreateAsync(Domain.MicroCategory mMicroCategory)
        {
            return await _httpClientService.PostAsync<Domain.MicroCategory, Domain.MicroCategory>(
                url: $"{_baseUrl}/MicroCategory",
                obj: mMicroCategory,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.MicroCategory> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.MicroCategory>(
            url: $"{_baseUrl}/MicroCategory/{id}",
                token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                url: $"{_baseUrl}/MicroCategory/{id}/LoginUser/{_loginUserId}",
                token: _token
             ).ConfigureAwait(false);

        }

        #endregion

    }
}
