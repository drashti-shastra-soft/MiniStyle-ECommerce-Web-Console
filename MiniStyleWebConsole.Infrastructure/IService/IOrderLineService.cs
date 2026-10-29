using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStyleWebConsole.Infrastructure.IService
{
    public interface IOrderLineService
    {
        Task<List<Domain.OrderLine>> GetByAsync(int id);
    }
}
