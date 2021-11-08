using System;
using System.Collections.Generic;
using System.Text;

namespace Prodigy.Utils.DTO
{
    public class DTOUsers
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Password { get; set; }
        public bool? IsEnabled { get; set; }
    }
}
