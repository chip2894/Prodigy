using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prodigy.Utils.Excepciones;
using Prodigy.Utils.Log.Excepciones;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Prodigy.Utils.Log.Web
{
    public class Control : ControllerBase
    {
        private readonly ILogging _logger;
        public Control(ILogging logger)
        {
            this._logger = logger;
        }
        public Control() { }


        protected virtual ActionResult CreateStringResponseMessage(int statusCode, string message)
        {
            var result = new ObjectResult(new { statusCode = statusCode, currentDate = DateTime.Now, message = message });
            result.StatusCode = statusCode;
            return result;
        }


        protected virtual async Task<ActionResult> HandleOperationExecutionAsync(Func<Task<ActionResult>> operationBody, string IdUsuario)
        {
            try
            {
                var response = await operationBody();

                return response;
            }
            catch (BadRequestException ex)
            {
                await _logger.EscribirLogAsync(ex, Enums.EventLogEntryType.Warning, IdUsuario);

                return CreateStringResponseMessage((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                await _logger.EscribirLogAsync(ex, Enums.EventLogEntryType.Error, IdUsuario);

                return CreateStringResponseMessage((int)HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(ex.Message));

            }
        }
    }
}