using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Ad : BaseEntity
    {
        public int OwnerId { get; set; }
        public int CarId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Place { get; set; }
        public User Owner { get; set; }
        public Car Car { get; set; }
    }
}
