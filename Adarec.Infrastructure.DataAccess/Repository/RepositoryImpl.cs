using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class RepositoryImpl<T> : IRepository<T> where T : class
    {
        private readonly adarecContext _context;
        private readonly DbSet<T> _dbSet;

        public RepositoryImpl(adarecContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        async Task IRepository<T>.AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Entity of type {typeof(T).Name} added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding entity: {ex.Message}");
            }
        }

        async Task IRepository<T>.DeleteAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting entity with ID {id}: {ex.Message}");
            }
        }

        async Task<IEnumerable<T>> IRepository<T>.GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all entities: {ex.Message}");
            }
        }

        async Task<T?> IRepository<T>.GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving entity with ID {id}: {ex.Message}");
            }
        }

        async Task IRepository<T>.UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating entity: {ex.Message}");
            }
        }
    }
}
