using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Infrastructure.IService
{
    public interface IProductPriceService
    {
        Task<Domain.ProductPrice> GetProductPrice(int id);

        Task<Domain.ProductPrice> SaveProductPrice(Domain.ProductPrice mProductPrice);

    }
}
