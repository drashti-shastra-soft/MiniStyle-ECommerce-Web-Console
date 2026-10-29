using MiniStyleWebConsole.Infrastructure.IService;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Configuration;
using System;

namespace MiniStyleWebConsole.Service
{
    public class FabricService : IFabricService
    {
        #region Declaration

        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public FabricService(IHttpClientService httpClientService)
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

        public async Task<Domain.FabricLister> GetAllAsync(Domain.FabricLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.FabricLister>(
                url: $"{_baseUrl}/Fabric/GetAll",
                obj: mLister,
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.Fabric> CreateAsync(Domain.Fabric mFabric)
        {
            return await _httpClientService.PostAsync<Domain.Fabric>(
                url: $"{_baseUrl}/Fabric",
                obj: mFabric,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.Fabric> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.Fabric>(
            url: $"{_baseUrl}/Fabric/{id}",
                token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                url: $"{_baseUrl}/Fabric/{id}/LoginUser/{_loginUserId}",
                token: _token
             ).ConfigureAwait(false);

        }

        public async Task<List<Domain.Fabric>> GetAllActiveFabric()
        {
            var result = await _httpClientService.GetAsync<List<Domain.Fabric>>(
                url: $"{_baseUrl}/Fabric/GetAllActiveFabric",
                token: _token
            ).ConfigureAwait(false);
            return result;
        }

    }
}
