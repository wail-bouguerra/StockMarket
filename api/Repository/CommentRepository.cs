using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }


        public async Task<List<Comment>> GetALLAsync()
        {
            return await _context.Comments.Include(a=>a.AppUser).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.Include(a=>a.AppUser).FirstOrDefaultAsync(i=> i.Id==id);
            if (comment == null)
                return null;
            return comment;

        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }


        public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto commentDto)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null) 
                return null;
            _context.Entry(comment).CurrentValues.SetValues(commentDto);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return null;
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;

        }

    }
}