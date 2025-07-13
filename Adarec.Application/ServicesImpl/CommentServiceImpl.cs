using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class CommentServiceImpl(adarecContext context) : ICommentService
    {
        private readonly ICommentRepository _commentRepository = new CommentRepositoryImpl(context);

        public async Task AddCommentAsync(OrderCommentsDto comment)
        {
            await _commentRepository.AddCommentAsync(comment);
        }

        public async Task UpdateCommentAsync(OrderCommentsDto comment)
        {
            await _commentRepository.UpdateCommentAsync(comment);
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            await _commentRepository.DeleteAsync(commentId);
        }

        public async Task<List<OrderCommentsDto>> ListCommentsByOrderAsync()
        {
            return await _commentRepository.ListCommentsByOrderAsync();
        }
    }
}