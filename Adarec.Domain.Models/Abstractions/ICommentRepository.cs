using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;

namespace Adarec.Domain.Models.Abstractions
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task AddCommentAsync(OrderCommentsDto comment);
        Task UpdateCommentAsync(OrderCommentsDto comment);
        Task<List<OrderCommentsDto>> ListCommentsByOrderAsync();
    }
}