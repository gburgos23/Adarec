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
            var brandEntity = new Brand
            {
                Name = brand.Name,
                Status = brand.Status
            };
            await _brandRepository.AddAsync(brandEntity);
        }

        public async Task UpdateBrandAsync(BrandDto brand)
        {
            var brandEntity = new Brand
            {
                BrandId = brand.BrandId,
                Name = brand.Name,
                Status = brand.Status
            };
            await _brandRepository.AddAsync(brandEntity);
        }

        public async Task<List<BrandDto>> GetActiveBrandsAsync()
        {
            return await _brandRepository.GetActiveBrandsAsync();
        }
    }
}