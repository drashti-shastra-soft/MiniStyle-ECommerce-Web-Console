using System.Threading.Tasks;


namespace MiniStyleWebConsole.Infrastructure.IService
{
    public interface ISubCategoryService
    {
        Task<Domain.SubCategoryLister> GetAllAsync(Domain.SubCategoryLister mLister);

        Task<Domain.SubCategory> CreateAsync(Domain.SubCategory mUser);

        Task<Domain.SubCategory> GetAsync(int id);

        Task DeleteAsync(int id);

    }
}
