using Prodigy.Utils.Log.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prodigy.Utils.Log.Excepciones
{
    public interface ILogging
    {
        Task EscribirLogAsync(Exception exception, EventLogEntryType type, string idUsuario);
    }
}
