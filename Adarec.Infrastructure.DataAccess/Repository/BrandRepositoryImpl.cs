using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class BrandRepositoryImpl(adarecContext context) : RepositoryImpl<Brand>(context), IBrandRepository
    {
    }
}