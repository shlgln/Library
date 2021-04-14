using Library.Entities;
using Library.Services.Entrusts.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.TestTools.Entrusts
{
    public class EntrustBuilder
    {
        private int _BookId = 1;
        private int _MemberId = 1;
        private DateTime _ReturnDate = DateTime.Now.AddDays(10);
        public EntrustBuilder GenerateAddEntrustWithReturnDate(DateTime returnDate)
        {
            _ReturnDate = returnDate;
            return this;
        }
        public RegisterEntrustDto Build()
        {
            return new RegisterEntrustDto
            {
                MemberId = _MemberId,
                BookId = _BookId,
                ReturnDate = _ReturnDate
            };
        }

    }
}
