using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class CommentRepositoryImpl(adarecContext context) : RepositoryImpl<Comment>(context), ICommentRepository
    {
    }
}