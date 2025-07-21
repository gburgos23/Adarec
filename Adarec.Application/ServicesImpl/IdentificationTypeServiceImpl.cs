using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class IdentificationTypeServiceImpl(adarecContext context) : IIdentificationTypeService
    {
        private readonly IIdentificationTypeRepository _identificationTypeRepository = new IdentificationTypeRepositoryImpl(context);

        public async Task<List<RolDto>> GetAllIdentificationTypesAsync()
        {
            var result = await _identificationTypeRepository.GetAllAsync();
            return [.. result.Select(x => new RolDto
            {
                RolId = x.IdentificationTypeId,
                Name = x.Name,
                Status = x.Status
            })];
        }
    }
}