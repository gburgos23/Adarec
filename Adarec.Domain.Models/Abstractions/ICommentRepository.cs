using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;

namespace Adarec.Domain.Models.Abstractions
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<List<OrderCommentsDto>> ListCommentsByOrderAsync();
    }
}