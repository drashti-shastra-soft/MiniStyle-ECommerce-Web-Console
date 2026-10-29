using System;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Infrastructure.IService
{
    public interface IProductDetailService
    {
        Task<Domain.ProductDetail> UpsertAsync(Domain.ProductDetail mDetail);

        Task<Domain.ProductDetail> GetAsync(int id);
    }
}
