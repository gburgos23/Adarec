using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface ICommentService
    {
        [OperationContract]
        Task AddCommentAsync(OrderCommentsDto comment);

        [OperationContract]
        Task UpdateCommentAsync(OrderCommentsDto comment);

        [OperationContract]
        Task DeleteCommentAsync(int commentId);

        [OperationContract]
        Task<List<OrderCommentsDto>> ListCommentsByOrderAsync();
    }
}