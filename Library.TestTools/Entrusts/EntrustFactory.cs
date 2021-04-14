using Library.Services.Entrusts.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.TestTools.Entrusts
{
    public static class EntrustFactory
    {
        public static RegisterEntrustDto GenerateAddEntrustDto(int memberId, int bookId)
        {
            return new RegisterEntrustDto
            {
                MemberId = memberId,
                BookId = bookId,
                ReturnDate = DateTime.Now.AddDays(10)
            };
        }

        public static RegisterEntrustDto GenerateAddEntrust(int memberId, int bookId)
        {
            return new RegisterEntrustDto
            {
                MemberId = memberId,
                BookId = bookId,
                ReturnDate = DateTime.Now.AddDays(10)
            };
        }
    }
}
