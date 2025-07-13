using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class EmailConfigRepositoryImpl(adarecContext context) : RepositoryImpl<EmailConfig>(context), IEmailConfigRepository
    {
    }
}