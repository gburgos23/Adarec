using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class EmailTemplateServiceImpl(adarecContext context) : IEmailTemplateService
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository = new EmailTemplateRepositoryImpl(context);

        public async Task<IEnumerable<EmailTemplate>> GetAllEmailTemplatesAsync()
        {
            return await _emailTemplateRepository.GetAllAsync();
        }

        public async Task<EmailTemplate?> GetEmailTemplateByIdAsync(int templateId)
        {
            return await _emailTemplateRepository.GetByIdAsync(templateId);
        }

        public async Task AddEmailTemplateAsync(EmailTemplate template)
        {
            await _emailTemplateRepository.AddAsync(template);
        }

        public async Task UpdateEmailTemplateAsync(EmailTemplate template)
        {
            await _emailTemplateRepository.UpdateAsync(template);
        }

        public async Task DeleteEmailTemplateAsync(int templateId)
        {
            await _emailTemplateRepository.DeleteAsync(templateId);
        }
    }
}