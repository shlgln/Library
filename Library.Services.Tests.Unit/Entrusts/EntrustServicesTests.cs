using FluentAssertions;
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

namespace Library.Services.Tests.Unit.Entrusts
{
    public class EntrustServicesTests
    {
        EFDataContext _contex;
        EFDataContext _readDataContext;
        EntrustService _sut;
        UnitOfWork _unitofwork;
        EntrustRepository _repository;
        private int entrustId;
        MemberRepository _memberRepository;
        BookRepository _bookRepository;
        public EntrustServicesTests()
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

        [Fact]
        public async void Register_registers_a_entrust_properly()
        {
            var bookCategory = BookCategoryFactory.GenerateBookCategory();
            var book = new BookBuilder().GenerateAddBookWithBookCategory(bookCategory).Build();
            _contex.Manipulate(_ => _.Books.Add(book));
            var member = MemberFactory.GenerateAddMember();
            _contex.Manipulate(_ => _.Members.Add(member));

            var dto = EntrustFactory.GenerateAddEntrustDto(member.Id, book.Id);
            var entrustId = await _sut.Register(dto);

            var expected = _readDataContext.Entrusts.Single(_ => _.Id == entrustId);
            expected.Id.Should().Be(entrustId);
        }

        [Fact]
        public void Register_throws_exception_when_memberAge_lessThan_minimumAgeBook()
        {
            var bookCategory = BookCategoryFactory.GenerateBookCategory();
            var book = new BookBuilder().GenerateAddBookWithBookCategory(bookCategory).Build();
            _contex.Manipulate(_ => _.Books.Add(book));
            var member = MemberFactory.GenerateAddMember(15);
            _contex.Manipulate(_ => _.Members.Add(member));

            var dto = EntrustFactory.GenerateAddEntrustDto(member.Id, book.Id);
            Func<Task> expected = () => _sut.Register(dto);

            expected.Should().Throw<MemberAgeIsLessThanMinimumAgeBookException>();
        }
    }
}
