using MiniStyleWebConsole.Infrastructure.IService;
using System.Threading.Tasks;
using System.Configuration;

namespace MiniStyleWebConsole.Service
{
    public class SubCategoryService : ISubCategoryService
    {
        #region Declaration

        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public SubCategoryService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null){
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }
        #endregion

        #region Public Method
        public async Task<Domain.SubCategoryLister> GetAllAsync(Domain.SubCategoryLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.SubCategoryLister, Domain.SubCategoryLister>(
                url: $"{_baseUrl}/SubCategory/GetAll",
                obj: mLister,
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.SubCategory> CreateAsync(Domain.SubCategory mSubCategory)
        {
            return await _httpClientService.PostAsync<Domain.SubCategory, Domain.SubCategory>(
                url: $"{_baseUrl}/SubCategory",
                obj: mSubCategory,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.SubCategory> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.SubCategory>(
            url: $"{_baseUrl}/SubCategory/{id}",
                token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                url: $"{_baseUrl}/SubCategory/{id}/LoginUser/{_loginUserId}",
                token: _token
             ).ConfigureAwait(false);
        }

        #endregion
    }
}
