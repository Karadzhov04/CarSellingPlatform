using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class CategoryAd : BaseEntity
    {
        public int AdId { get; set; }
        public int CategoryId { get; set; }
        public Ad Ad { get; set; }
        public Category Category { get; set; }
    }
}
