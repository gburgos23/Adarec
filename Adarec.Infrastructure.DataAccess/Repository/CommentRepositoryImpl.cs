using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class CommentRepositoryImpl(adarecContext _context) : RepositoryImpl<Comment>(_context), ICommentRepository
    {
        private readonly adarecContext context = _context;

        public async Task AddCommentAsync(Comment comment)
        {
            await context.AddAsync(comment);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            context.Comments.Update(comment);
            await context.SaveChangesAsync();
        }

        public async Task<List<Comment>> ListCommentsAsync()
        {
            return await context.Comments.ToListAsync();
        }
    }
}