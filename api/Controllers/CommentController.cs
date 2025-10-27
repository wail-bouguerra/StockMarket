using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentrepo;
        private readonly IStockRepository _stockrepo;
        public CommentController(ICommentRepository commentrepo, IStockRepository stockrepo)
        {
            _commentrepo = commentrepo;
            _stockrepo = stockrepo;
        }




        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentrepo.GetALLAsync();
            var commentDto = comments.Select(c => c.ToCommentDto());
            return Ok(commentDto);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentrepo.GetByIdAsync(id);
            if (comment == null)
                return NotFound();
            return Ok(comment.ToCommentDto());
        }


        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)

        {
            var stock = await _stockrepo.GetByIdAsync(stockId);
            // var testBool = await _stockrepo.StockExists(stockId);

            if (!await _stockrepo.StockExists(stockId))
                return NotFound();

            var commentModel = commentDto.ToCommentFromCreate(stockId);
            await _commentrepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel }, commentModel.ToCommentDto());

        }
    
    }
}