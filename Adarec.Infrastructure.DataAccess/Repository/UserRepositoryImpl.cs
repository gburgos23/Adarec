using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class UserRepositoryImpl(adarecContext context) : RepositoryImpl<User>(context), IUserRepository
    {
    }
}