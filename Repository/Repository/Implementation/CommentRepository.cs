using DomainModels.Models.Common;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Repository.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Implementation
{
    public class CommentRepository : EfCoreRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context):base(context)
        {

        }
        public async Task<List<Comment>> GetSomethingInclude()
        {
            return await _context.Comments.Include(c => c.Blog).ToListAsync();
        }
        public async Task<Comment> ExistsComment(int? id)
        {
            var comment = await _context.Comments
                .Include(c => c.Blog)
                .FirstOrDefaultAsync(m => m.Id == id);
            return comment;
        }

        public async Task<bool> DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public AppDbContext appDbContext { get { return _context as AppDbContext; } }

    }
}
