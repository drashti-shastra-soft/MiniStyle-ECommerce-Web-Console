using MiniStyleWebConsole.Infrastructure.IService;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Service
{
    public class AgeGroupService : IAgeGroupService
    {
        #region Declaration
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public AgeGroupService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null) { 
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }
        #endregion

        #region Public Method

        public async Task<Domain.AgeGroupLister> GetAllAsync(Domain.AgeGroupLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.AgeGroupLister>(
                url: $"{_baseUrl}/AgeGroup/GetAll",
                obj: mLister,
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<List<Domain.AgeGroup>> GetAllActiveAgeGroup()
        {
            var result = await _httpClientService.GetAsync<List<Domain.AgeGroup>>(
                url: $"{_baseUrl}/AgeGroup/GetAllActiveAgeGroup",
                token: _token

            ).ConfigureAwait(false);
            return result;
        }

        public async Task<List<Domain.ProductAgeGroup>> GetProductAgeGroup(int productId)
        {
            var result = await _httpClientService.GetAsync<List<Domain.ProductAgeGroup>>(
                url: $"{_baseUrl}/ProductAgeGroup/ProductId/{productId}",
                token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task SaveProductAgeGroup(List<Domain.ProductAgeGroup> mProductAgeGroups)
        {
            await _httpClientService.PutAsync<List<Domain.ProductAgeGroup>>(
                url: $"{_baseUrl}/ProductAgeGroup",
                obj: mProductAgeGroups,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.AgeGroup> CreateAsync(Domain.AgeGroup mAgeGroup)
        {
            return await _httpClientService.PostAsync<Domain.AgeGroup>(
                url: $"{_baseUrl}/AgeGroup",
                obj: mAgeGroup,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.AgeGroup> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.AgeGroup>(
            url: $"{_baseUrl}/AgeGroup/{id}",
                token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                url: $"{_baseUrl}/AgeGroup/{id}/LoginUser/{_loginUserId}",
                token: _token
             ).ConfigureAwait(false);

        }

        #endregion
    }
}
