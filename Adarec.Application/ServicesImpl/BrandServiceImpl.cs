using Adarec.Application.DTO.DTOs;
using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

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

            await _brandRepository.AddBrandAsync(brandEntity);
        }

        public async Task UpdateBrandAsync(BrandDto brand)
        {
            var brandEntity = new Brand
            {
                BrandId = brand.BrandId,
                Name = brand.Name,
                Status = brand.Status
            };

            await _brandRepository.UpdateBrandAsync(brandEntity);
        }

        public async Task<List<BrandDto>> GetActiveBrandsAsync()
        {
            var brands = await _brandRepository.GetActiveBrandsAsync();
            return brands.Select(b => new BrandDto
            {
                BrandId = b.BrandId,
                Name = b.Name,
                Status = b.Status
            }).ToList();
        }
    }
}