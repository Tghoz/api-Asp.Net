using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testApi_sqlServer.Dtos.Stock;
using testApi_sqlServer.Models;

namespace testApi_sqlServer.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockMedol)
        {
            return new StockDto
            {
                Id = stockMedol.Id,
                Symbol = stockMedol.Symbol,
                CompanyName = stockMedol.CompanyName,
                Purchase = stockMedol.Purchase,
                LastDiv = stockMedol.LastDiv,
                Industry = stockMedol.Industry,
                MarketCap = stockMedol.MarketCap,
                Comments = stockMedol.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStockCreateDto(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }

    }
}