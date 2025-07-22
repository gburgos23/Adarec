using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class ModelRepositoryImpl(adarecContext context) : RepositoryImpl<Model>(context), IModelRepository
    {
        private readonly adarecContext _context = context;

        public async Task<List<Model>> GetActiveModelsAsync()
        {
            return await _context.Models
                .Where(m => m.Status)
                .ToListAsync();
        }
    }
}