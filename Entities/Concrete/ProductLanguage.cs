using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProductLanguage:BaseEntity
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string LangCode { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

    }
}
