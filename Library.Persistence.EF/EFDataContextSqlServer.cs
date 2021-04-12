using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Persistence.EF
{
    public class EFDataContextSqlServer:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder _)
        {
            base.OnConfiguring(_);
            _.UseSqlServer("Server=.;Database=Library;Trusted_Connection=True;");
        }
    }
}
