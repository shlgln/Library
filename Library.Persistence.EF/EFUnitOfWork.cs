using Library.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence.EF
{
    public class EFUnitOfWork : UnitOfWork
    {
        private readonly EFDataContext _context;

        public EFUnitOfWork(EFDataContext dataContext)
        {
            _context = dataContext;
        }

        public void Begin()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitPartial()
        {
            _context.SaveChanges();
        }

        public void Commit()
        {
            _context.SaveChanges();
            _context.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }
    }
}
