using System.Threading.Tasks;

namespace MiniStyleWebConsole.Infrastructure.IService
{
    public interface IMicroCategoryService
    {
        Task<Domain.MicroCategoryLister> GetAllAsync(Domain.MicroCategoryLister mLister);

        Task<Domain.MicroCategory> CreateAsync(Domain.MicroCategory mMicroCategory);

        Task<Domain.MicroCategory> GetAsync(int id);

        Task DeleteAsync(int id);
    }
}
