using Library.Entities;
using Library.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Entrusts.Contracts
{
    public interface EntrustService : Service
    {
        Task<int> Register(RegisterEntrustDto dto);
        Task TackBackBook(int id);
    }
}
