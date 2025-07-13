using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class CommentRepositoryImpl(adarecContext _context) : RepositoryImpl<Comment>(_context), ICommentRepository
    {
        private readonly adarecContext context = _context;

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

            await context.AddAsync(commentEntity);
            await context.SaveChangesAsync();
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

            context.Comments.Update(commentEntity);
            await context.SaveChangesAsync();
        }

        async Task<List<OrderCommentsDto>> ICommentRepository.ListCommentsByOrderAsync()
        {
            try
            {
                var result = await (
                    from c in context.Comments
                    join o in context.Orders on c.OrderId equals o.OrderId
                    join u in context.Users on c.UserId equals u.UserId
                    group new { c, u } by new { o.OrderId, o.Description } into grupo
                    select new OrderCommentsDto
                    {
                        OrderId = grupo.Key.OrderId,
                        OrderDescription = grupo.Key.Description,
                        Comments = grupo.Select(g => new CommentDetailDto
                        {
                            CommentId = g.c.CommentId,
                            UserName = g.u.Name,
                            Comment = g.c.Comment1,
                            CreatedAt = g.c.CreatedAt
                        }).ToList()
                    }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while listing comments by order.", ex);
            }
        }
    }
}