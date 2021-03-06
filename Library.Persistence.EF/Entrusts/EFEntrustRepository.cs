using Library.Entities;
using Library.Services.Entrusts.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence.EF.Entrusts
{
    public class EFEntrustRepository: EntrustRepository
    {
        private readonly EFDataContext _context;
        private readonly DbSet<Entrust> _set;

        public EFEntrustRepository(EFDataContext context)
        {
            _context = context;
            _set = _context.Entrusts;
        }

        public void Add(Entrust entrust)
        {
            _set.Add(entrust);
        }

        public async Task<Entrust> FindEntrustById(int id)
        {
            return await _set.FirstOrDefaultAsync(_ => _.Id == id);
        }
    }
}
