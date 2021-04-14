using FluentAssertions;
using Library.Infrastructure.Application;
using Library.Infrastructure.Test;
using Library.Persistence.EF;
using Library.Persistence.EF.Members;
using Library.Services.Members;
using Library.Services.Members.Contracts;
using Library.TestTools.Members;
using System.Linq;
using Xunit;

namespace Library.Services.Tests.Unit.Memebers
{
    public class MemberServicesTests
    {
        EFDataContext _contex;
        EFDataContext _readDataContext;
        MemberService _sut;
        UnitOfWork _unitofwork;
        MemberRepository _repository;
        public MemberServicesTests()
        {
            var db = new EFInMemoryDatabase();
            _contex = db.CreateDataContext<EFDataContext>();
            _readDataContext = db.CreateDataContext<EFDataContext>();
            _unitofwork = new EFUnitOfWork(_contex);
            _repository = new EFMemberRepository(_contex);
            _sut = new MemberAppService(_unitofwork, _repository);
        }
        [Fact]
        public async void Register_registers_a_member_properly()
        {
            var dto = MemberFactory.GenerateAddMemberDto();
            var memberId = await _sut.Register(dto);

            var expected = _readDataContext.Members.Single(_ => _.Id == memberId);
            expected.Id.Should().Be(memberId);
        }
    }
}
