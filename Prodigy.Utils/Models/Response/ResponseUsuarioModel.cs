using System;
using System.Collections.Generic;
using System.Text;

namespace Prodigy.Utils.Models.Response
{
    public class ResponseUsuarioModel
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public string password { get; set; }
        public bool? isEnabled { get; set; }
    }
}
