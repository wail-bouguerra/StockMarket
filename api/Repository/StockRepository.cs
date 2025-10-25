using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }


        public async Task<List<Stock>> GetALLAsync()
        {
            return await _context.Stocks.Include(c=>c.Comments).ToListAsync();
        }




        public async Task<Stock?> GetById(int id)
        {
            var stock = await _context.Stocks.Include(c=>c.Comments).FirstOrDefaultAsync(i=> i.Id==id);
            if (stock == null)
            {
                return null;
            }
            return stock;
        }


        public async Task<Stock> CreateAsync(CreateStockRequestDto StockDto)
        {
            var stockModel = StockDto.ToStockFromCreatedDto();
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }


        
        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto StockDto)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
                return null;

            _context.Entry(stock).CurrentValues.SetValues(StockDto);
            await _context.SaveChangesAsync();

            return stock;
        }




        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
                return null;

            _context.Stocks.Remove(stock);

            await _context.SaveChangesAsync();

            return stock;
        }

    }
}