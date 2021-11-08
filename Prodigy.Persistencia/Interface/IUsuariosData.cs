using Prodigy.Utils.Models.Request;
using Prodigy.Utils.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prodigy.Persistencia.Interface
{
    public interface IUsuariosData
    {
        Task<ResponseUsuarioModel> consultarUsuario(RequestUsuarioModel request);
        Task<ResponseUsuarioModel> consultarUsuarioById(RequestUsuarioByIdModel request);
        Task<ResponseUsuarioModel> loginUsuario(RequestLoginModel request, bool status);
        Task logOutUsuario(RequestBody request, bool status);
        Task<List<ResponseUsuarioModel>> consultarListaUsuarios();
        Task agregarUsuario(RequestAddUsuarioModel request);
        Task modificarUsuario(RequestModUsuarioModel request);
        Task eliminarUsuario(RequestDltUsuarioModel request);
        Task<bool> validarUsuario(RequestValiUsuarioModel request);
    }
}
