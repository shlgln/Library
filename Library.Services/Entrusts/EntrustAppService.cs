using Library.Entities;
using Library.Infrastructure.Application;
using Library.Services.Books.Contracts;
using Library.Services.Entrusts.Contracts;
using Library.Services.Entrusts.Exceptions;
using Library.Services.Members.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Entrusts
{
    public class EntrustAppService : EntrustService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly EntrustRepository _repository;
        private readonly BookRepository _bookRepository;
        private readonly MemberRepository _memberRepository;

        public EntrustAppService(UnitOfWork unitOfWork,
            EntrustRepository repository,
            BookRepository bookRepository,
            MemberRepository memberRepository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
        }

        public async Task<int> Register(RegisterEntrustDto dto)
        {
            await CheckIsMemberAgeinAgeRangeOfBook(dto.BookId, dto.MemberId);
            
            var entrust = new Entrust
            {
                BookId = dto.BookId,
                MemberId = dto.MemberId,
                ReturnDate = dto.ReturnDate
            };

            _repository.Add(entrust);
            await _unitOfWork.Complete();
            return entrust.Id;
        }

        public async Task TackBackBook(int id)
        {
            var entrust = await _repository.FindEntrustById(id);

            if (entrust.ReturnDate < DateTime.Now)
                throw new TackBackDateBookIsAfterReturnDateException();
        }

        private async Task CheckIsMemberAgeinAgeRangeOfBook(int bookId, int memberId)
        {
            var book = await _bookRepository.FindBookById(bookId);
            var member = await _memberRepository.FindMemberById(memberId);
            if (book.MinimumAge > member.Age)
            {
                throw new MemberAgeIsLessThanMinimumAgeBookException();
            }
        }
    }
}
