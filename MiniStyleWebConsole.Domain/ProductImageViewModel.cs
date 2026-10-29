using System.Collections.Generic;

namespace MiniStyleWebConsole.Domain
{
    public class ProductImageViewModel
    {
        public int ProductColorId { get; set; } = 0;

        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}
