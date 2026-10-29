using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Infrastructure.IService
{
    public interface IFabricService
    {
        Task<Domain.FabricLister> GetAllAsync(Domain.FabricLister mLister);

        Task<Domain.Fabric> CreateAsync(Domain.Fabric mFabric);

        Task<Domain.Fabric> GetAsync(int id);

        Task DeleteAsync(int id);

        Task<List<Domain.Fabric>> GetAllActiveFabric();
    }
}
