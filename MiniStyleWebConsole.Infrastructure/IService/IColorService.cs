using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Infrastructure.IService
{
    public interface IColorService
    {
        Task<Domain.ColorLister> GetAllAsync(Domain.ColorLister mLister);

        Task<Domain.Color> CreateAsync(Domain.Color mColor);

        Task<Domain.Color> GetAsync(int id);

        Task DeleteAsync(int id);

        Task<List<Domain.Color>> GetAllActiveColor();

        Task SaveProductColor(List<Domain.ProductColor> mProductColors);

        Task<List<Domain.ProductColor>> GetProductColor(int ProductId);
    }
}
