using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Infrastructure.IService
{
    public interface IProductSizeChartService
    {
        Task<List<Domain.ProductSizeChart>> Get(int productId, string unitType);

        Task<Domain.ProductSizeChart> Upsert(Domain.ProductSizeChart mProductSizeChart);
    }
}
