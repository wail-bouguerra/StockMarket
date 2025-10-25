using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(s=>s.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStockFromCreatedDto(this CreateStockRequestDto createStockDto)
        {
            return new Stock
            {
                Symbol = createStockDto.Symbol,
                CompanyName = createStockDto.CompanyName,
                Purchase = createStockDto.Purchase,
                LastDiv = createStockDto.LastDiv,
                Industry = createStockDto.Industry,
                MarketCap = createStockDto.MarketCap
            };
        }
        public static Stock ToStockFromUpdatedDto(this UpdateStockRequestDto updateStockDto)
        {
            return new Stock
            {
                Symbol = updateStockDto.Symbol,
                CompanyName = updateStockDto.CompanyName,
                Purchase = updateStockDto.Purchase,
                LastDiv = updateStockDto.LastDiv,
                Industry = updateStockDto.Industry,
                MarketCap = updateStockDto.MarketCap
            };
        }
    }
}