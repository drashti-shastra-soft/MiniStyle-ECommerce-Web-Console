using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Infrastructure.IService
{
    public interface IAgeGroupService
    {
        Task<Domain.AgeGroupLister> GetAllAsync(Domain.AgeGroupLister mLister);

        Task<List<Domain.AgeGroup>> GetAllActiveAgeGroup();

        Task<Domain.AgeGroup> CreateAsync(Domain.AgeGroup mAgeGroup);

        Task<Domain.AgeGroup> GetAsync(int id);

        Task<List<Domain.ProductAgeGroup>> GetProductAgeGroup(int productId);

        Task SaveProductAgeGroup(List<Domain.ProductAgeGroup> mProductAgeGroups);

        Task DeleteAsync(int id);
    }
}
