using Library.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities
{
    public class Person : Entity<int>
    {
        public Person()
        {
            Entrusts = new HashSet<Entrust>();
        }
        public string FullName { get; set; }
        public byte Age { get; set; }
        public string Address { get; set; }
        public HashSet<Entrust> Entrusts { get; set; }
    }
}
