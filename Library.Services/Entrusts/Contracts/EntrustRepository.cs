using Library.Entities;
using Library.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Entrusts.Contracts
{
    public interface EntrustRepository : Repository
    {
         void Add(Entrust entrust);
        Task<Entrust> FindEntrustById(int id);
    }
}
