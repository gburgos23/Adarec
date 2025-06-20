using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class ModelServiceImpl(adarecContext context) : IModelService
    {
        private readonly IModelRepository _modelRepository = new ModelRepositoryImpl(context);

        public async Task<IEnumerable<Model>> GetAllModelsAsync()
        {
            return await _modelRepository.GetAllAsync();
        }

        public async Task<Model?> GetModelByIdAsync(int modelId)
        {
            return await _modelRepository.GetByIdAsync(modelId);
        }

        public async Task AddModelAsync(Model model)
        {
            await _modelRepository.AddAsync(model);
        }

        public async Task UpdateModelAsync(Model model)
        {
            await _modelRepository.UpdateAsync(model);
        }

        public async Task DeleteModelAsync(int modelId)
        {
            await _modelRepository.DeleteAsync(modelId);
        }
    }
}