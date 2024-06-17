using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testApi_sqlServer.Models;

namespace testApi_sqlServer.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
        Task<Portfolio> CreateAsync(Portfolio portfolio);

        Task<Portfolio> DeletePortfolioAsync(AppUser appUser, string symbol);
    }
}