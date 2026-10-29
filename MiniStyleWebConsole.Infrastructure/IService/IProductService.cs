using System.Threading.Tasks;

namespace MiniStyleWebConsole.Infrastructure.IService
{
    public interface IProductService
    {
        Task<Domain.ProductLister> GetAllAsync(Domain.ProductLister mLister);

        Task<Domain.Product> CreateAsync(Domain.Product mProduct);

        Task<Domain.Product> GetAsync(int id);

        Task DeleteAsync(int id);
    }
}
