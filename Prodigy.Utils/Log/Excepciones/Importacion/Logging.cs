using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prodigy.Utils.Log.Excepciones.Importacion
{
    public class Logging : ILogging
    {
        private readonly ILogger _logger;

        public Logging(ILogger<Logging> logger)
        {
            _logger = logger;
        }

        public async Task EscribirLogAsync(Exception ex, Enums.EventLogEntryType type, string idUsuario)
        {
            //var st = new StackTrace(ex, true);
            var traceString = new System.Text.StringBuilder();
            traceString.AppendLine(Environment.NewLine + "*****************************START********************************");
            traceString.AppendLine("IdUsuario: " + idUsuario);
            switch (type)
            {
                case Enums.EventLogEntryType.Error:
                    traceString.AppendLine("Error: " + ex.Message);
                    break;
                case Enums.EventLogEntryType.Warning:
                    traceString.AppendLine("Warning: " + ex.Message);
                    break;
            }
            traceString.AppendLine("Inner: " + ex.InnerException);
            traceString.AppendLine("Trace: " + ex.StackTrace);
            traceString.AppendLine(Environment.NewLine + "*****************************END********************************");
            switch (type)
            {
                case Enums.EventLogEntryType.Error:
                    await Task.Run(() => { _logger.LogError(traceString.ToString()); });
                    break;
                case Enums.EventLogEntryType.Warning:
                    await Task.Run(() => { _logger.LogWarning(traceString.ToString()); });
                    break;
            }
        }

    }
}
