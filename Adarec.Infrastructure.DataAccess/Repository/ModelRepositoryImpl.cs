using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class ModelRepositoryImpl(adarecContext context) : RepositoryImpl<Model>(context), IModelRepository
    {
    }
}