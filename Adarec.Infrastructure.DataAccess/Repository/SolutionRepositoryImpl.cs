using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class SolutionRepositoryImpl(adarecContext context) : RepositoryImpl<Solution>(context), ISolutionRepository
    {
    }
}