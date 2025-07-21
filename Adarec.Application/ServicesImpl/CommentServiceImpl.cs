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

        public async Task AddCommentAsync(OrderCommentsDto commentDto)
        {
            if (commentDto.Comments == null || commentDto.Comments.Count == 0)
                throw new ArgumentException("No hay comentarios para agregar.");

            var detail = commentDto.Comments.First();

            var commentEntity = new Comment
            {
                OrderId = commentDto.OrderId ?? 0,
                UserId = detail.UserId,
                Comment1 = detail.Comment ?? string.Empty,
                CreatedAt = detail.CreatedAt
            };

            await _commentRepository.AddCommentAsync(commentEntity);
        }

        public async Task UpdateCommentAsync(OrderCommentsDto commentDto)
        {
            if (commentDto.Comments == null || commentDto.Comments.Count == 0)
                throw new ArgumentException("No hay comentarios para actualizar.");

            var detail = commentDto.Comments.First();

            var commentEntity = await context.Comments.FindAsync(detail.CommentId);
            if (commentEntity == null)
                throw new InvalidOperationException("Comentario no encontrado.");

            commentEntity.UserId = detail.UserId;
            commentEntity.Comment1 = detail.Comment ?? string.Empty;
            commentEntity.CreatedAt = detail.CreatedAt;

            await _commentRepository.UpdateCommentAsync(commentEntity);
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            await _commentRepository.DeleteAsync(commentId);
        }

        public async Task<List<OrderCommentsDto>> ListCommentsByOrderAsync()
        {
            var comments = await _commentRepository.ListCommentsAsync();

            var commentsDto = comments
                .GroupBy(c => c.OrderId)
                .Select(g =>
                {
                    var order = g.First().Order;
                    return new OrderCommentsDto
                    {
                        OrderId = g.Key,
                        OrderDescription = order?.Description,
                        Comments = g.Select(c => new CommentDetailDto
                        {
                            CommentId = c.CommentId,
                            UserName = c.User?.Name,
                            UserId = c.UserId ?? 0,
                            Comment = c.Comment1,
                            CreatedAt = c.CreatedAt
                        }).ToList()
                    };
                })
                .ToList();

            return commentsDto;
        }
    }
}