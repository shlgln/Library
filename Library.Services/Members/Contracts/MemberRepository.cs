using Library.Entities;
using Library.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Members.Contracts
{
    public interface MemberRepository : Repository
    {
        void Add(Member member);
    }
}
