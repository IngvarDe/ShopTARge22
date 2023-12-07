using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge22.Core.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
    }
}
