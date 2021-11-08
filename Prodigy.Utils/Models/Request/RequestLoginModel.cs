using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Prodigy.Utils.Models.Request
{
    public class RequestLoginModel: RequestBody
    {
        public string password { get; set; }
    }
}
