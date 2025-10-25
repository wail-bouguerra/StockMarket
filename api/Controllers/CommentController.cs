using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public CommentController(ICommentRepository commentrepo)
        {
            _commentrepo = commentrepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentrepo.GetALLAsync();
            var commentDto = comments.Select(c => c.ToCommentDto());
            return Ok(commentDto);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentrepo.GetByIdAsync(id);
            if (comment == null)
                return NotFound();

            return Ok(comment.ToCommentDto());
        }
    
    }
}