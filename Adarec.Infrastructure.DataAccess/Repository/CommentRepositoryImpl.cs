using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class CommentRepositoryImpl(adarecContext _context) : RepositoryImpl<Comment>(_context), ICommentRepository
    {
        private readonly adarecContext context = _context;

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