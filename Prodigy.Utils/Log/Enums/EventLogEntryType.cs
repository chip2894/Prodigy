using System;
using System.Collections.Generic;
using System.Text;

namespace Prodigy.Utils.Log.Enums
{
    public enum EventLogEntryType
    {
        Trace = 0,

        //
        // Resumen:
        //     Un evento de información. Indica una operación importante y correcta.
        Information = 2,
        //
        // Resumen:
        //     Un evento de advertencia. Esto indica un problema que no es importante, pero
        //     que puede indicar las condiciones que pueden causar problemas futuros.
        Warning = 3,
        //
        // Resumen:
        //     Un evento de error. Esto indica un problema importante que debe conocer el usuario;
        //     Normalmente, una pérdida de funcionalidad o datos.
        Error = 4,

        Critical = 5
    }
}