using Prodigy.Logica.Interface;
using Prodigy.Persistencia.Interface;
using Prodigy.Utils.Excepciones;
using Prodigy.Utils.Models.Request;
using Prodigy.Utils.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prodigy.Logica.Implementacion
{
    public class UsuariosLogic : IUsuariosLogic
    {
        #region Base de recursos
        private readonly IUsuariosData _usuariosData;
        #endregion

        #region Constructor

        public UsuariosLogic(IUsuariosData usuariosData)
        {
            _usuariosData = usuariosData;
        }

        #endregion

        #region Metodos Publicos

        public async Task<ResponseUsuarioModel> consultarUsuario(RequestUsuarioModel request)
        {
            var consultaUsuario = await _usuariosData.consultarUsuario(request);

            return consultaUsuario;
        }

        public async Task<ResponseUsuarioModel> consultarUsuarioById(RequestUsuarioByIdModel request)
        {
            var result = await _usuariosData.consultarUsuarioById(request);

            return result;
        }

        public async Task<ResponseUsuarioModel> loginUsuario(RequestLoginModel request, bool status)
        {
            var result = await _usuariosData.loginUsuario(request, status);

            if (result == null)
            {
                throw new BadRequestException("La información no se procesó correctamente, favor de volver a intentar");
            }

            return result;
        }

        public async Task logoutUsuario(RequestBody request, bool status)
        {
            await _usuariosData.logOutUsuario(request, status);
        }

        public async Task<List<ResponseUsuarioModel>> consultarListaUsuarios()
        {
            var result = await _usuariosData.consultarListaUsuarios();

            return result;

        }

        public async Task agregarUsuario(RequestAddUsuarioModel request)
        {
            await _usuariosData.agregarUsuario(request);
        }

        public async Task modificarUsuario(RequestModUsuarioModel request)
        {
            await _usuariosData.modificarUsuario(request);
        }

        public async Task eliminarUsuario(RequestDltUsuarioModel request)
        {
            await _usuariosData.eliminarUsuario(request);
        }

        public async Task<bool> validarUsuario(RequestValiUsuarioModel request)
        {
            var result = await _usuariosData.validarUsuario(request);

            return result;
        }

        #endregion

        #region Metodos Privados

        #endregion
    }
}
