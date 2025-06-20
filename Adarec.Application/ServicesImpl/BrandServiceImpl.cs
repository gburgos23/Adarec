using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class BrandServiceImpl(adarecContext context) : IBrandService
    {
        private readonly IBrandRepository _brandRepository = new BrandRepositoryImpl(context);

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _brandRepository.GetAllAsync();
        }

        public async Task<Brand?> GetBrandByIdAsync(int brandId)
        {
            return await _brandRepository.GetByIdAsync(brandId);
        }

        public async Task AddBrandAsync(Brand brand)
        {
            await _brandRepository.AddAsync(brand);
        }

        public async Task UpdateBrandAsync(Brand brand)
        {
            await _brandRepository.UpdateAsync(brand);
        }

        public async Task DeleteBrandAsync(int brandId)
        {
            await _brandRepository.DeleteAsync(brandId);
        }
    }
}