using System.Text.Json.Serialization;
using System.Web.Mvc;

namespace MiniStyleWebConsole.Domain
{
    public class ProductDetail 
    {
        public int ProductDetailId { get; set; }
        public int ProductId { get; set; }
        
        [AllowHtml]
        public string Description { get; set; }

        [AllowHtml]
        public string Specification { get; set; }
        public int FabricId { get; set; }
        public string Fabric { get; set; }
        public string Token { get; set; }

    }
}
