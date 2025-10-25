using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetALLAsync();
        Task<Stock?> GetById(int id);
        Task<Stock> CreateAsync(CreateStockRequestDto StockDto);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto StockDto);
        Task<Stock?> DeleteAsync(int id);
    }
}