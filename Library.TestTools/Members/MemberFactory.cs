using Library.Entities;
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
                Age = 19,
                Address = "شیراز، زرهی، خیابان آقایی"
            };
        }
        public static Member GenerateAddMember(byte age = 19)
        {
            return new Member
            {
                FullName = "آریاگلشن",
                Age = age,
                Address = "شیراز، زرهی، خیابان آقایی"
            };
        }
    }
}
