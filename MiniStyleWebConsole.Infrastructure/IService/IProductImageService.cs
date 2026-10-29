using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Infrastructure.IService
{
    public interface IProductImageService
    {
        Task<List<Domain.ProductImage>> GetProductImages(int productId);

        Task<List<Domain.ProductImage>> GetProductImages(int productId, int productColorId);

        Task<Domain.ProductImage> SaveProductImage(Domain.ProductImage mProductImage);

        Task DeleteProductImage(int id);

        //Task<List<Domain.Color>> GetAllActiveColor();
    }
}
