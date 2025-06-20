using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        Task<IEnumerable<User>> GetAllUsersAsync();

        [OperationContract]
        Task<User?> GetUserByIdAsync(int userId);

        [OperationContract]
        Task AddUserAsync(User user);

        [OperationContract]
        Task UpdateUserAsync(User user);

        [OperationContract]
        Task DeleteUserAsync(int userId);
    }
}