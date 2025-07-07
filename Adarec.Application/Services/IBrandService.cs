using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IBrandService
    {
        [OperationContract]
        Task<IEnumerable<Brand>> GetAllBrandsAsync();

        [OperationContract]
        Task<Brand?> GetBrandByIdAsync(int brandId);

        [OperationContract]
        Task AddBrandAsync(Brand brand);

        [OperationContract]
        Task UpdateBrandAsync(Brand brand);

        [OperationContract]
        Task DeleteBrandAsync(int brandId);

        [OperationContract]
        Task<List<BrandDto>> GetActiveBrandsAsync();
    }
}