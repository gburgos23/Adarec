using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class IdentificationTypeServiceImpl(adarecContext context) : IIdentificationTypeService
    {
        private readonly IIdentificationTypeRepository _identificationTypeRepository = new IdentificationTypeRepositoryImpl(context);

        public async Task<IEnumerable<IdentificationType>> GetAllIdentificationTypesAsync()
        {
            return await _identificationTypeRepository.GetAllAsync();
        }

        public async Task<IdentificationType?> GetIdentificationTypeByIdAsync(int identificationTypeId)
        {
            return await _identificationTypeRepository.GetByIdAsync(identificationTypeId);
        }

        public async Task AddIdentificationTypeAsync(IdentificationType identificationType)
        {
            await _identificationTypeRepository.AddAsync(identificationType);
        }

        public async Task UpdateIdentificationTypeAsync(IdentificationType identificationType)
        {
            await _identificationTypeRepository.UpdateAsync(identificationType);
        }

        public async Task DeleteIdentificationTypeAsync(int identificationTypeId)
        {
            await _identificationTypeRepository.DeleteAsync(identificationTypeId);
        }
    }
}