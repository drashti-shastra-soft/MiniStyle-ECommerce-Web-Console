using System.Collections.Generic;

namespace MiniStyleWebConsole.Domain
{
    public class FabricLister
    {
        public List<Fabric> List { get; set; } = new List<Fabric>();

        public Fabric SearchCriteria { get; set; } = new Fabric();

        public Pagination Pagination { get; set; } = new Pagination();
    }
}
