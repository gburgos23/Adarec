using Adarec.Application.Services;
using Adarec.Domain.Models.Abstractions;
using Adarec.Domain.Models.Entities;
using Adarec.Infrastructure.DataAccess.Repository;

namespace Adarec.Application.ServicesImpl
{
    public class OrderAssignmentServiceImpl(adarecContext context) : IOrderAssignmentService
    {
        private readonly IOrderAssignmentRepository _orderAssignmentRepository = new OrderAssignmentRepositoryImpl(context);

        public async Task<IEnumerable<OrderAssignment>> GetAllOrderAssignmentsAsync()
        {
            return await _orderAssignmentRepository.GetAllAsync();
        }

        public async Task<OrderAssignment?> GetOrderAssignmentByIdAsync(int assignmentId)
        {
            return await _orderAssignmentRepository.GetByIdAsync(assignmentId);
        }

        public async Task AddOrderAssignmentAsync(OrderAssignment assignment)
        {
            await _orderAssignmentRepository.AddAsync(assignment);
        }

        public async Task UpdateOrderAssignmentAsync(OrderAssignment assignment)
        {
            await _orderAssignmentRepository.UpdateAsync(assignment);
        }

        public async Task DeleteOrderAssignmentAsync(int assignmentId)
        {
            await _orderAssignmentRepository.DeleteAsync(assignmentId);
        }
    }
}