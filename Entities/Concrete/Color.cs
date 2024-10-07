using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Color:BaseEntity
    {
        public string Name { get; set; }
        public string HexCode { get; set; }  // Stores color as hex code (e.g., #FF0000 for red)
    }
}
