using System;
using System.Collections.Generic;
using System.Text;

namespace Prodigy.Utils.Models.Request
{
    public class RequestAddUsuarioModel: RequestBody
    {
        public string userName { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string? dateOfBirth { get; set; }
        public string password { get; set; }
        public bool isEnabled { get; set; }
    }
}
