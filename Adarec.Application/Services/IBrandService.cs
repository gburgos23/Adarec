using Adarec.Application.DTO.DTOs;
using Adarec.Domain.Models.Entities;
using System.ServiceModel;

namespace Adarec.Application.Services
{
    [ServiceContract]
    public interface IBrandService
    {
        [OperationContract]
        Task AddBrandAsync(BrandDto brand);

        [OperationContract]
        Task UpdateBrandAsync(BrandDto brand);

        [OperationContract]
        Task<List<BrandDto>> GetActiveBrandsAsync();
    }
}