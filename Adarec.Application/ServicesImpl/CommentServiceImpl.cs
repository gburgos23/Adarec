using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class CommentServiceImpl(adarecContext context) : ICommentService
    {
        private readonly ICommentRepository _commentRepository = new CommentRepositoryImpl(context);

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await _commentRepository.GetAllAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int commentId)
        {
            return await _commentRepository.GetByIdAsync(commentId);
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _commentRepository.AddAsync(comment);
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            await _commentRepository.UpdateAsync(comment);
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            await _commentRepository.DeleteAsync(commentId);
        }
    }
}