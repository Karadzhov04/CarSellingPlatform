using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public enum CategoryType
    {
        Sale,
        Rent,
        Leasing
    }
    public class Category : BaseEntity
    {
        public CategoryType CategoryName { get; set; }
    }
}
