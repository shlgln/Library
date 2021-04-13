using Library.Services.Members.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.TestTools.Members
{
    public static class MemberFactory
    {
        public static RegisterMembetDto GenerateAddMemberDto()
        {
            return new RegisterMembetDto
            {
                FullName = "آریاگلشن",
                Age = 15,
                Address = "شیراز، زرهی، خیابان آقایی"
            };
        }
    }
}
