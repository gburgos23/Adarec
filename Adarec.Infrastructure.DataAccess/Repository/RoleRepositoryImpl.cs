using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class RoleRepositoryImpl(adarecContext context) : RepositoryImpl<Role>(context), IRoleRepository
    {
    }
}