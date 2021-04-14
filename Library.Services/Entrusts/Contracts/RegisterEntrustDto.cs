using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Services.Entrusts.Contracts
{
    public class RegisterEntrustDto
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
    }
}
