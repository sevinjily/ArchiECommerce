using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProductSize

    {
        public Guid SizeId { get; set; }
        public Size Size { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
