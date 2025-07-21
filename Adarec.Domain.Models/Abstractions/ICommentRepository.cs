using Adarec.Domain.Models.Entities;

namespace Adarec.Domain.Models.Abstractions
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task AddCommentAsync(Comment comment);

        Task UpdateCommentAsync(Comment comment);

        Task<List<Comment>> ListCommentsAsync();
    }
}