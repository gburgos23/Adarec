using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class BrandRepositoryImpl(adarecContext context) : RepositoryImpl<Brand>(context), IBrandRepository
    {
        private readonly adarecContext _context = context;

        public async Task<List<BrandDto>> GetActiveBrandsAsync()
        {
            return await _context.Brands
                .Where(b => b.Status)
                .Select(b => new BrandDto
                {
                    BrandId = b.BrandId,
                    Name = b.Name
                }).ToListAsync();
        }
    }
}