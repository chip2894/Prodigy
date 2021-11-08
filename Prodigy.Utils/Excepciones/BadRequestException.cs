using System;
using System.Collections.Generic;
using System.Text;

namespace Prodigy.Utils.Excepciones
{
    public class BadRequestException : Exception
    {
        #region Constructor
        public BadRequestException(string mensaje) : base(mensaje)
        {

        }
        #endregion

    }
}
