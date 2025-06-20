using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class EmailConfigServiceImpl(adarecContext context) : IEmailConfigService
    {
        private readonly IEmailConfigRepository _emailConfigRepository = new EmailConfigRepositoryImpl(context);

        public async Task<IEnumerable<EmailConfig>> GetAllEmailConfigsAsync()
        {
            return await _emailConfigRepository.GetAllAsync();
        }

        public async Task<EmailConfig?> GetEmailConfigByIdAsync(int configId)
        {
            return await _emailConfigRepository.GetByIdAsync(configId);
        }

        public async Task AddEmailConfigAsync(EmailConfig config)
        {
            await _emailConfigRepository.AddAsync(config);
        }

        public async Task UpdateEmailConfigAsync(EmailConfig config)
        {
            await _emailConfigRepository.UpdateAsync(config);
        }

        public async Task DeleteEmailConfigAsync(int configId)
        {
            await _emailConfigRepository.DeleteAsync(configId);
        }
    }
}