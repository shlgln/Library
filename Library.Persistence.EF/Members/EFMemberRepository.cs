using Library.Entities;
using Library.Services.Members.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.EF.Members
{
    public class EFMemberRepository: MemberRepository
    {
        private readonly EFDataContext _context;
        private readonly DbSet<Member> _set;

        public EFMemberRepository(EFDataContext context)
        {
            _context = context;
            _set = _context.Members;
        }

        public void Add(Member member)
        {
            _set.Add(member);
        }
    }
}
