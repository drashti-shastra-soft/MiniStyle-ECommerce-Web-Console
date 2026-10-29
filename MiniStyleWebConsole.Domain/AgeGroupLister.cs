using System.Collections.Generic;
using System;

namespace MiniStyleWebConsole.Domain
{
    public class AgeGroupLister
    {
        public List<AgeGroup> List { get; set; } = new List<AgeGroup>();

        public AgeGroup SearchCriteria { get; set; } = new AgeGroup();

        public Pagination Pagination { get; set; } = new Pagination();
    }
}
