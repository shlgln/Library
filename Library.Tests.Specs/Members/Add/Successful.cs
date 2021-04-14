using FluentAssertions;
using Library.Infrastructure.Application;
using Library.Infrastructure.Test;
using Library.Persistence.EF;
using Library.Persistence.EF.Members;
using Library.Services.Members;
using Library.Services.Members.Contracts;
using Library.TestTools.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Library.Tests.Specs.Members.Add
{
    public class Successful
    {
        EFDataContext _contex;
        EFDataContext _readDataContext;
        MemberService _sut;
        UnitOfWork _unitofwork;
        MemberRepository _repository;
        private int memberId;
        public Successful()
        {
            var db = new EFInMemoryDatabase();
            _contex = db.CreateDataContext<EFDataContext>();
            _readDataContext = db.CreateDataContext<EFDataContext>();
            _unitofwork = new EFUnitOfWork(_contex);
            _repository = new EFMemberRepository(_contex);
            _sut = new MemberAppService(_unitofwork, _repository);
        }

        //[Given("در لیست اعضای کتابخانه عضوی وجود ندارد")]
        private void Given()
        {

        }

        //[When("عضوی با نام و نام‌خانوادگی آریا گلشن و سن 19 سال و آدرس شیراز، زرهی، خیابان آقایی تعریف می‌کنم")]
        private async void When()
        {
            var dto = MemberFactory.GenerateAddMemberDto();
            memberId = await _sut.Register(dto);
        }

        //[Then("باید عضوی با نام آریا گلشن و سن 19 سال و آدرس شیراز، زرهی، خیابان آقایی در لیست اعضای کتابخانه وجود داشته باشد")]
        private void Then()
        {
            var expected = _readDataContext.Members.Single(_ => _.Id == memberId);
            expected.Id.Should().Be(memberId);
        }

        [Fact]
        public void Run()
        {
            Given();
            When();
            Then();
        }
    }
}
