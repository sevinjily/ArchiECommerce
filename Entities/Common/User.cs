using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Common
{
    public class User:AppUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public override string? PhotoUrl { get => base.PhotoUrl; set => base.PhotoUrl = value; }

    }
}
