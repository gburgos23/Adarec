using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IOrderAssignmentService
    {
        [OperationContract]
        Task<IEnumerable<OrderAssignment>> GetAllOrderAssignmentsAsync();

        [OperationContract]
        Task<OrderAssignment?> GetOrderAssignmentByIdAsync(int assignmentId);

        [OperationContract]
        Task AddOrderAssignmentAsync(OrderAssignment assignment);

        [OperationContract]
        Task UpdateOrderAssignmentAsync(OrderAssignment assignment);

        [OperationContract]
        Task DeleteOrderAssignmentAsync(int assignmentId);
    }
}