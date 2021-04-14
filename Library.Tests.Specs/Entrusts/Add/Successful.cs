using FluentAssertions;
using Library.Entities;
using Library.Infrastructure.Application;
using Library.Infrastructure.Test;
using Library.Persistence.EF;
using Library.Persistence.EF.Books;
using Library.Persistence.EF.Entrusts;
using Library.Persistence.EF.Members;
using Library.Services.Books.Contracts;
using Library.Services.Entrusts;
using Library.Services.Entrusts.Contracts;
using Library.Services.Members.Contracts;
using Library.TestTools.BookCategoreis;
using Library.TestTools.Books;
using Library.TestTools.Entrusts;
using Library.TestTools.Members;
using System.Linq;
using Xunit;

namespace Library.Tests.Specs.Entrusts.Add
{
    public class Successful
    {
        EFDataContext _contex;
        EFDataContext _readDataContext;
        EntrustService _sut;
        UnitOfWork _unitofwork;
        EntrustRepository _repository;
        Book book;
        Member member;
        private int entrustId;
        MemberRepository _memberRepository;
        BookRepository _bookRepository;
        public Successful()
        {
            var db = new EFInMemoryDatabase();
            _contex = db.CreateDataContext<EFDataContext>();
            _readDataContext = db.CreateDataContext<EFDataContext>();
            _unitofwork = new EFUnitOfWork(_contex);
            _repository = new EFEntrustRepository(_contex);
            _memberRepository = new EFMemberRepository(_contex);
            _bookRepository = new EFBookRepository(_contex);
            _sut = new EntrustAppService(_unitofwork, _repository, _bookRepository, _memberRepository);
        }

        //[Given("کتابی با نام درجستجوی زمان از دست رفته و عضوی از کتابخانه با نام آریا گلشن وجود دارد")]
        private void Given() 
        {
            var bookCategory = BookCategoryFactory.GenerateBookCategory();
            book = new BookBuilder().GenerateAddBookWithBookCategory(bookCategory).Build();
            _contex.Manipulate(_ => _.Books.Add(book));
            member = MemberFactory.GenerateAddMember();
            _contex.Manipulate(_ => _.Members.Add(member));
        }

        //[When("کتابی با نام درجستجوی زمان از دست‌رفته را به عضوی از کتابخانه
        //با نام آریا گلشن و تاریخ برگشت  20/01/1400 به امانت می‌دهم")]
        private async void When()
        {
            var dto = EntrustFactory.GenerateAddEntrustDto(member.Id, book.Id);  
            entrustId = await _sut.Register(dto);
        }
        private void THen() 
        {
            var expected = _readDataContext.Entrusts.Single(_ => _.Id == entrustId);
            expected.Id.Should().Be(entrustId);
        }

        [Fact]
        public void Run()
        {
            Given();
            When();
            THen();
        }
    }
}
