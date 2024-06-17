
using Microsoft.AspNetCore.Identity;

namespace testApi_sqlServer.Models
{

    public class AppUser : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}