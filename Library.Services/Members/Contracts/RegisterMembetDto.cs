using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Services.Members.Contracts
{
    public class RegisterMembetDto
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public byte Age { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
