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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Tests.Specs.Entrusts.Add
{
    public class FailedWhenReturnDateBeforeNow
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
        Func<Task> expected;
        public FailedWhenReturnDateBeforeNow()
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

        //[Given("کتاب با نام درجستجوی زمان از دست رفته و رنج سنی بالای 18 در لیست کتاب‌ها
        //و عضو کتابخانه با نام آریا گلشن و سن 15 سال در لیست اعضای کتابخانه وجود دارد")]
        private void Given()
        {
            var bookCategory = BookCategoryFactory.GenerateBookCategory();
            book = new BookBuilder().GenerateAddBookWithBookCategory(bookCategory).Build();
            _contex.Manipulate(_ => _.Books.Add(book));
            member = MemberFactory.GenerateAddMember(15);
            _contex.Manipulate(_ => _.Members.Add(member));
        }

        //[When("می‌خواهم کتاب با نام درجستجوی زمان از دست‌رفته
        //و رنج سنی بالای 18 سال را به عضو کتابخانه با نام آریا گلشن و سن 15 سال به امانت بدهم")]
        private async void When()
        {
            var dto = EntrustFactory.GenerateAddEntrustDto(member.Id, book.Id);
            expected = () =>  _sut.Register(dto);
        }

        //[Then("باید خطای "سن عضو کتابخانه برای امانت بردن کتاب مناسب نیست"، رخ دهد")]
        private void Then()
        {
            expected.Should().Throw<MemberAgeIsLessThanMinimumAgeBookException> ();
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
