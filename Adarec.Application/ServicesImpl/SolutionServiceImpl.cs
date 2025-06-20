using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class SolutionServiceImpl(adarecContext context) : ISolutionService
    {
        private readonly ISolutionRepository _solutionRepository = new SolutionRepositoryImpl(context);

        public async Task<IEnumerable<Solution>> GetAllSolutionsAsync()
        {
            return await _solutionRepository.GetAllAsync();
        }

        public async Task<Solution?> GetSolutionByIdAsync(int solutionId)
        {
            return await _solutionRepository.GetByIdAsync(solutionId);
        }

        public async Task AddSolutionAsync(Solution solution)
        {
            await _solutionRepository.AddAsync(solution);
        }

        public async Task UpdateSolutionAsync(Solution solution)
        {
            await _solutionRepository.UpdateAsync(solution);
        }

        public async Task DeleteSolutionAsync(int solutionId)
        {
            await _solutionRepository.DeleteAsync(solutionId);
        }
    }
}