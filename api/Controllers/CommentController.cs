using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Extensions;
using api.interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentrepo;
        private readonly IStockRepository _stockrepo;

        private readonly UserManager<AppUser> _userManager;
        public CommentController(ICommentRepository commentrepo, IStockRepository stockrepo, UserManager<AppUser> userManager)
        {
            _commentrepo = commentrepo;
            _stockrepo = stockrepo;
            _userManager = userManager;
        }




        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comments = await _commentrepo.GetALLAsync();
            var commentDto = comments.Select(c => c.ToCommentDto());
            return Ok(commentDto);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comment = await _commentrepo.GetByIdAsync(id);
            if (comment == null)
                return NotFound();
            return Ok(comment.ToCommentDto());
        }




        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)

        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stock = await _stockrepo.GetByIdAsync(stockId);

            if (!await _stockrepo.StockExists(stockId))
                return NotFound("Stock not found");

            var username = User.GetUsername();
            var appUser = _userManager.FindByNameAsync(username);

            var commentModel = commentDto.ToCommentFromCreate(stockId);
            commentModel.AppUserId = appUser.Id.ToString();
            await _commentrepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());

            

        }



        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comment = await _commentrepo.UpdateAsync(id, commentDto);
            if (comment == null)
                return NotFound();

            return Ok(comment.ToCommentDto());
        }
        

        [HttpDelete("{id:int}")]
        
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var commentModel = await _commentrepo.GetByIdAsync(id);
            if (commentModel == null)
                return NotFound();
            await _commentrepo.DeleteAsync(id);
            return NoContent();
        }

    
    }
}