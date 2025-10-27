using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController: ControllerBase

    {  
        private readonly IStockRepository _stockRepo;
        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetALLAsync();
            var StockDTO = stocks.Select(s => s.ToStockDto());
            return Ok(StockDTO);
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }




        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto createStockDto)
        {
            var stockModel = await _stockRepo.CreateAsync(createStockDto);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockDto)
        {
            var stock = await _stockRepo.UpdateAsync(id, updateStockDto);
            if (stock == null)
                return NotFound($"Stock with ID {id} not found.");

            return Ok(stock.ToStockDto());
        }
        



        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _stockRepo.DeleteAsync(id);

            if (stock == null)
                return NotFound($"Stock with ID {id} not found.");


            return NoContent();
        }
    }
}