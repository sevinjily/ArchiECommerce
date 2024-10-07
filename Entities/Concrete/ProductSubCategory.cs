using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProductSubCategory
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid  SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }

    }
}
