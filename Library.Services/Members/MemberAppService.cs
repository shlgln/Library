using Library.Entities;
using Library.Infrastructure.Application;
using Library.Services.Members.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Members
{
    public class MemberAppService: MemberService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly MemberRepository _repository;

        public MemberAppService(UnitOfWork unitOfWork, MemberRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<int> Register(RegisterMembetDto dto)
        {
            var newMember = new Member
            {
                FullName = dto.FullName,
                Age = dto.Age,
                Address = dto.Address
            };

            _repository.Add(newMember);
            await _unitOfWork.Complete();

            return newMember.Id;
        }
    }
}
