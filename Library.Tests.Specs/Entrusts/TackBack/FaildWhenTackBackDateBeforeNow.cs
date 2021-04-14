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
using Library.Services.Entrusts.Exceptions;
using Library.Services.Members.Contracts;
using Library.TestTools.BookCategoreis;
using Library.TestTools.Books;
using Library.TestTools.Entrusts;
using Library.TestTools.Members;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Library.Tests.Specs.Entrusts.TackBack
{
    public class FaildWhenTackBackDateBeforeNow
    {

        EFDataContext _contex;
        EFDataContext _readDataContext;
        EntrustService _sut;
        UnitOfWork _unitofwork;
        EntrustRepository _repository;
        MemberRepository _memberRepository;
        BookRepository _bookRepository;
        Book book;
        Member member;
        Entrust entrust;
        Func<Task> expected;
        public FaildWhenTackBackDateBeforeNow()
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

        //[Given("در لیست امانات، کتاب با نام درجستجوی زمان از دست رفته و عضو کتابخانه با نام آریا گلشن و تاریخ برگشت 20/01/1400 وجود دارد")]
        private void Given()
        {
            var bookCategory = BookCategoryFactory.GenerateBookCategory();
            book = new BookBuilder().GenerateAddBookWithBookCategory(bookCategory).Build();
            _contex.Manipulate(_ => _.Books.Add(book));
            member = MemberFactory.GenerateAddMember(15);
            _contex.Manipulate(_ => _.Members.Add(member));
            entrust = new  EntrustBuilder().GenerateAddEntrustWithReturnDate(DateTime.Now.AddDays(-1)).Build();
            _contex.Manipulate(_ => _.Entrusts.Add(entrust));
        }

        //[When("کتاب با نام درجستجوی زمان از دست‌رفته از عضو کتابخانه با نام آریا گلشن در تاریخ 21/01/1400 تحویل می‌گیرم")]
        private void When()
        {
            expected = () => _sut.TackBackBook(entrust.Id);
        }

        //[Then("باید خطای "زمان تحویل کتاب بعد از زمان برگشت کتاب می‌باشد"، دیده شود")]
        private void Then()
        {
            expected.Should().Throw<TackBackDateBookIsAfterReturnDateException>();
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
