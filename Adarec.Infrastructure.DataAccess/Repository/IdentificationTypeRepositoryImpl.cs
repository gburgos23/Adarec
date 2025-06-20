using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class IdentificationTypeRepositoryImpl(adarecContext context) : RepositoryImpl<IdentificationType>(context), IIdentificationTypeRepository
    {
    }
}