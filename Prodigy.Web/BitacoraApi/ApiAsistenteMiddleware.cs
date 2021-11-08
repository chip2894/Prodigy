using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Prodigy.Logica.Interface;
using Prodigy.Utils.Models.Request;
using Prodigy.Utils.Models.Response;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Prodigy.Web.BitacoraApi
{
    public class ApiAsistenteMiddleware : IMiddleware
    {

        #region Base de recursos

        private readonly IUsuariosLogic _usuariosLogic;

        #endregion

        public ApiAsistenteMiddleware(IUsuariosLogic usuariosLogic)
        {
            this._usuariosLogic = usuariosLogic;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string requestBodyText = "";
            RequestBody requestModel;
            RequestUsuarioModel requestUsuario = new RequestUsuarioModel();
            ResponseBody responseUsuario = new ResponseBody();

            //Se obtiene la ruta de la peticion
            string ruta = context.Request.Path.Value;

            //Se asegura que el body de la peticion http se pueda leer varias veces
            context.Request.EnableBuffering();
            context.Response.ContentType = "application/json; charset=utf-8";

            using (var reader = new StreamReader( context.Request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, bufferSize: 1024, leaveOpen: true) )
            {
                requestBodyText = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            //Se convierte la peticion http a un modelo tipo RequestBody, para obtener el id del usuario
            requestModel = JsonConvert.DeserializeObject<RequestBody>(requestBodyText.ToString());

            #region Validaciones al usuario

            requestUsuario.usuario = requestModel.usuario;

            var consultarUsuario = await _usuariosLogic.consultarUsuario(requestUsuario);

            //Validamos que el usuario exista en la BD
            if (consultarUsuario != null)
            {
                if (ruta != "/api/examSkillify/Usuarios/Login")
                {
                    //Validamos que el usuario tenga sesion iniciada
                    if (consultarUsuario.isEnabled == false)
                    {
                        responseUsuario.exito = false;
                        responseUsuario.mensaje = "El Usuario no se encuentra con sesión iniciada";
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(responseUsuario));
                    }
                }
            }
            else
            {
                responseUsuario.exito = false;
                responseUsuario.mensaje = "El Usuario no se encuentra en el sistema, favor de contactar al administrador";
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(responseUsuario));
            }

            #endregion

            await next(context);
        }

    }
}
