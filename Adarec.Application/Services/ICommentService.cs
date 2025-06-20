using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface ICommentService
    {
        [OperationContract]
        Task<IEnumerable<Comment>> GetAllCommentsAsync();

        [OperationContract]
        Task<Comment?> GetCommentByIdAsync(int commentId);

        [OperationContract]
        Task AddCommentAsync(Comment comment);

        [OperationContract]
        Task UpdateCommentAsync(Comment comment);

        [OperationContract]
        Task DeleteCommentAsync(int commentId);
    }
}