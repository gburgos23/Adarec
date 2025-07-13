using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class BrandServiceImpl(adarecContext context) : IBrandService
    {
        private readonly IBrandRepository _brandRepository = new BrandRepositoryImpl(context);

        public async Task AddBrandAsync(BrandDto brand)
        {
            await _brandRepository.AddBrandAsync(brand);
        }

        public async Task UpdateBrandAsync(BrandDto brand)
        {
            await _brandRepository.UpdateBrandAsync(brand);
        }

        public async Task<List<BrandDto>> GetActiveBrandsAsync()
        {
            return await _brandRepository.GetActiveBrandsAsync();
        }
    }
}