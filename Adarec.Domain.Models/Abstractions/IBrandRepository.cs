using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;

namespace Adarec.Domain.Models.Abstractions
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task UpdateBrandAsync(BrandDto brand);
        Task AddBrandAsync(BrandDto brand);
        Task<List<BrandDto>> GetActiveBrandsAsync();
    }
}