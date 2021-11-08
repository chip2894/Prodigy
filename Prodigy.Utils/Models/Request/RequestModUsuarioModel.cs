using System;
using System.Collections.Generic;
using System.Text;

namespace Prodigy.Utils.Models.Request
{
    public class RequestModUsuarioModel: RequestBody
    {
        public int idUsuarioMod { get; set; }
        public string emailMod { get; set; }
        public string firstNameMod { get; set; }
        public string lastNameMod { get; set; }
        public string? dateOfBirthMod { get; set; }
        public string passwordMod { get; set; }
        public bool isEnabledMod { get; set; }
    }
}
