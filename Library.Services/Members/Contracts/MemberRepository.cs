using Library.Entities;
using Library.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Members.Contracts
{
    public interface MemberRepository : Repository
    {
        void Add(Member member);
        Task<Member> FindMemberById(int id);
    }
}
