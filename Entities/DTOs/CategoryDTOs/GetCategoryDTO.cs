using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.CategoryDTOs
{
    public class GetCategoryDTO
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string LangCode { get; set; }

    }
}
