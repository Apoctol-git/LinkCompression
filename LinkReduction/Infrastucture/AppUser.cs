using Microsoft.AspNetCore.Identity;
using System;

namespace LinkReduction.Infrastucture
{
    public class AppUser : IdentityUser<Guid>
    {
        public string DisplayName { get; set; }
    }
}
