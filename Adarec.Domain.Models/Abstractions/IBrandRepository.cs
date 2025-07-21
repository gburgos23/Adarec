using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;

namespace Adarec.Domain.Models.Abstractions
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task UpdateBrandAsync(Brand brand);

        Task AddBrandAsync(Brand brand);

        Task<List<Brand>> GetActiveBrandsAsync();
    }
}