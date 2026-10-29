using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Domain
{
    public class ProductDetailLister
    {
        public List<ProductDetail> List { get; set; } = new List<ProductDetail>();

        public ProductDetail SearchCriteria { get; set; } = new ProductDetail();

        public Pagination Pagination { get; set; } = new Pagination();
    }
}
