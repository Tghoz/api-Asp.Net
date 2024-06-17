using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testApi_sqlServer.Models;

namespace testApi_sqlServer.Interfaces
{
    public interface ITokenServices
    {
        string CreateToken(AppUser user);
    }
}