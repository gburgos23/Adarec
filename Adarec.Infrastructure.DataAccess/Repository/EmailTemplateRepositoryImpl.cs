using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class EmailTemplateRepositoryImpl(adarecContext context) : RepositoryImpl<EmailTemplate>(context), IEmailTemplateRepository
    {
    }
}