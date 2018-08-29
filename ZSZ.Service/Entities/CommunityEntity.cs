using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Service.Entities
{
    public class CommunityEntity:BaseEntity
    {
        public string Name { get; set; }
        public long RegionId { get; set; }

        public virtual RegionEntity Region { get; set; }
        public string Location { get; set; }
        public string Traffic { get; set; }
        public int? BuiltYear { get; set; }
    }
}
