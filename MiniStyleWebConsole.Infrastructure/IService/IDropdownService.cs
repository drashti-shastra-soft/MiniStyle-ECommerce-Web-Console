using System.Threading.Tasks;

namespace MiniStyleWebConsole.Infrastructure.IService
{
    public interface IDropdownService
    {
        dynamic GetStatusOptions();
        
        dynamic GetPageSizeOptions(int selectedSize = 20);

        Task<dynamic> GetCategoriesOptionsAsync();

        Task<dynamic> GetSubCategoriesOptionsAsync();

        Task<dynamic> GetMicroCategoriesOptionsAsync();

        Task<dynamic> GetFabricOptions();

        dynamic GetPaymentStatusOptions();

        dynamic GetShippingStatusOptions();

        Task<dynamic> GetProductColorOptions(int productId);

        Task<dynamic> GetProductAgeGroupOptions(int productId);

        dynamic GetWearablePartOptions();

        //void UpdateStatus(Domain.Order mOrder);
    }
}
