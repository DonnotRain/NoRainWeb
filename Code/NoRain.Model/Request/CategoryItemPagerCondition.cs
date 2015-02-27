using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoRain.Business.Model.Request
{
    public class CategoryItemPagerCondition
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Guid? CategoryId { get; set; }

        public Guid? ParentId { get; set; }
    }
}
