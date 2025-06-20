using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;

namespace Adarec.Infrastructure.DataAccess.Repository
{
    public class CustomerRepositoryImpl(adarecContext context) : RepositoryImpl<Customer>(context), ICustomerRepository
    {
    }
}