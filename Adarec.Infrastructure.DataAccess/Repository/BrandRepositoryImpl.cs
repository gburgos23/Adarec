using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class BrandRepositoryImpl(adarecContext context) : RepositoryImpl<Brand>(context), IBrandRepository
    {
        private readonly adarecContext _context = context;

        public async Task AddBrandAsync(Brand brand)
        {
            await _context.AddAsync(brand);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBrandAsync(Brand brand)
        {
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Brand>> GetActiveBrandsAsync()
        {
            return await _context.Brands
                .Where(b => b.Status)
                .ToListAsync();
        }
    }
}