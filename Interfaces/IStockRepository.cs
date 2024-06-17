using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testApi_sqlServer.Dtos.Stock;
using testApi_sqlServer.Helpers;
using testApi_sqlServer.Models;

namespace testApi_sqlServer.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> GetBySymbolAsync(string symbol);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock> UpdateAsync(int id, UpdateStockRequesDto stockDto);
        Task<Stock> DeleteAsync(int id);
        Task<bool> StockExists(int id);
        Task GetBySymbolAsync(object symbol);
    }
}