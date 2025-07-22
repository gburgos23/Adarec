using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class ModelServiceImpl(adarecContext context) : IModelService
    {
        private readonly IModelRepository _modelRepository = new ModelRepositoryImpl(context);

        public async Task<List<ModelDto>> GetActiveModelsAsync()
        {
            var models = await _modelRepository.GetActiveModelsAsync();
            return [.. models.Where(m => m.Status)
                 .Select(m => new ModelDto
                 {
                     ModelId = m.ModelId,
                     Name = m.Name,
                     Description = m.Description,
                     BrandId = m.BrandId,
                     DeviceTypeId = m.DeviceTypeId
                 })];
        }
    }
}