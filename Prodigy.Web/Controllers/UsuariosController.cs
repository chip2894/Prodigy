using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prodigy.Logica.Interface;
using Prodigy.Utils.Excepciones;
using Prodigy.Utils.Log.Excepciones;
using Prodigy.Utils.Log.Web;
using Prodigy.Utils.Models.Request;
using Prodigy.Utils.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodigy.Web.Controllers
{
    [ApiController]
    [Route("api/examSkillify/[controller]")]
    public class UsuariosController : Control
    {

        #region Base de recursos

        private readonly IUsuariosLogic _usuariosLogic;

        #endregion

        #region Constructor

        public UsuariosController(ILogging logger , IUsuariosLogic usuariosLogic) : base(logger)
        {
            this._usuariosLogic = usuariosLogic;
        }

        #endregion

        /// <summary>
        /// Método para realizar el login
        /// </summary>
        /// <param name="requestData">RequestLoginModel</param>
        /// <returns>
        /// ResponseLoginModel
        /// </returns>
        [HttpPost, Route("Login")]
        [ProducesResponseType(200, Type = typeof(ResponseUsuarioModel))]
        [Produces("application/json")]
        public async Task<ActionResult> Login([FromBody] RequestLoginModel requestData)
        {
            return await this.HandleOperationExecutionAsync(async () =>
            {

                ActionResult result = BadRequest();

                var response = await this._usuariosLogic.loginUsuario(requestData,true);

                if (response != null)
                {
                    result = Ok(response);
                }
                else
                {
                    throw new BadRequestException("La información no se procesó correctamente, favor de volver a intentar");
                }

                return result;

            }, requestData.usuario.ToString());
        }

        /// <summary>
        /// Método para realizar el logout
        /// </summary>
        /// <param name="requestData">RequestLoginModel</param>
        /// <returns>
        /// ResponseLoginModel
        /// </returns>
        [HttpPost, Route("Logout")]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        public async Task<ActionResult> Logout([FromBody] RequestBody requestData)
        {
            return await this.HandleOperationExecutionAsync(async () =>
            {

                ActionResult result = BadRequest();

                await this._usuariosLogic.logoutUsuario(requestData,false);
                
                result = Ok();

                return result;

            }, requestData.usuario.ToString());
        }

        /// <summary>
        /// Méodo para consultar lista de usuarios
        /// </summary>
        /// <param name="requestData">RequestBody</param>
        /// <returns>
        /// Lista de Usuarios
        /// </returns>
        [HttpPost, Route("Consultar/Usuarios")]
        [ProducesResponseType(200, Type = typeof(List<ResponseUsuarioModel>))]
        [Produces("application/json")]
        public async Task<ActionResult> consultarUsuarios([FromBody] RequestBody requestData)
        {
            return await this.HandleOperationExecutionAsync(async () =>
            {

                ActionResult result = BadRequest();

                var response = await this._usuariosLogic.consultarListaUsuarios();

                if (response != null)
                {
                    result = Ok(response);
                }
                else
                {
                    throw new BadRequestException("La información no se procesó correctamente, favor de volver a intentar");
                }

                return result;

            }, requestData.usuario.ToString());
        }

        /// <summary>
        /// Méodo para consultar un usuario por ID
        /// </summary>
        /// <param name="requestData">RequestBody</param>
        /// <returns>
        /// Lista de Usuarios
        /// </returns>
        [HttpPost, Route("Consultar/Usuario")]
        [ProducesResponseType(200, Type = typeof(ResponseUsuarioModel))]
        [Produces("application/json")]
        public async Task<ActionResult> consultarUsuarioById([FromBody] RequestUsuarioByIdModel requestData)
        {
            return await this.HandleOperationExecutionAsync(async () =>
            {

                ActionResult result = BadRequest();

                var response = await this._usuariosLogic.consultarUsuarioById(requestData);

                if (response != null)
                {
                    result = Ok(response);
                }
                else
                {
                    throw new BadRequestException("La información no se procesó correctamente, favor de volver a intentar");
                }

                return result;

            }, requestData.usuario.ToString());
        }

        /// <summary>
        /// Agregar nuevo usuario
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost, Route("Agregar")]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        public async Task<ActionResult> insertarUsuario([FromBody] RequestAddUsuarioModel requestData)
        {
            return await this.HandleOperationExecutionAsync(async () =>
            {

                ActionResult result = BadRequest();

                await this._usuariosLogic.agregarUsuario(requestData);
                
                result = Ok();

                return result;

            }, requestData.usuario.ToString());
        }

        /// <summary>
        /// Modificar usuario por ID
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost, Route("Modificar")]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        public async Task<ActionResult> modificarUsuario([FromBody] RequestModUsuarioModel requestData)
        {
            return await this.HandleOperationExecutionAsync(async () =>
            {

                ActionResult result = BadRequest();

                await this._usuariosLogic.modificarUsuario(requestData);
                
                result = Ok();

                return result;

            }, requestData.usuario.ToString());
        }

        /// <summary>
        /// Eliminar usuario por ID (Eliminación Física)
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost, Route("Eliminar")]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        public async Task<ActionResult> EliminarUsuario([FromBody] RequestDltUsuarioModel requestData)
        {
            return await this.HandleOperationExecutionAsync(async () =>
            {

                ActionResult result = BadRequest();

                await this._usuariosLogic.eliminarUsuario(requestData);

                result = Ok();

                return result;

            }, requestData.usuario.ToString());
        }

        [HttpPost, Route("Validar")]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        public async Task<ActionResult> validUsuario([FromBody] RequestValiUsuarioModel requestData)
        {
            return await this.HandleOperationExecutionAsync(async () =>
            {

                ActionResult result = BadRequest();

                var response = await this._usuariosLogic.validarUsuario(requestData);

                result = Ok(response);

                return result;

            }, requestData.usuario.ToString());
        }
    }
}
