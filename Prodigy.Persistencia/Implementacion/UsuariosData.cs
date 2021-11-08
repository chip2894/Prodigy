using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Prodigy.Modelo.examSkillify;
using Prodigy.Persistencia.Interface;
using Prodigy.Utils.Models.Request;
using Prodigy.Utils.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodigy.Persistencia.Implementacion
{
    public class UsuariosData : IUsuariosData
    {
        #region Base de recursos

        private readonly IMapper _mapper;
        private readonly exam_skillifyContext _context;

        #endregion

        #region Constructor

        public UsuariosData(IMapper mapper, exam_skillifyContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        #endregion

        #region Metodos Publicos

        public async Task<ResponseUsuarioModel> consultarUsuario(RequestUsuarioModel request)
        {
            ResponseUsuarioModel response = new ResponseUsuarioModel();

            string stored = $"EXEC sp_examSkillify_cnslUsro @usuario='{request.usuario}'";

            var data = _context.Users.FromSqlRaw(stored).AsEnumerable().Select(x => x).SingleOrDefault();

            if(data != null)
            {
                response.id = data.Id;
                response.userName = data.UserName;
                response.password = data.Password;
                response.firstName = data.FirstName;
                response.lastName = data.LastName;
                response.email = data.Email;
                response.dateOfBirth = data.DateOfBirth;
                response.isEnabled = data.IsEnabled;
            }

            return await Task.FromResult(response);

        }

        public async Task<ResponseUsuarioModel> consultarUsuarioById(RequestUsuarioByIdModel request)
        {
            ResponseUsuarioModel response = new ResponseUsuarioModel();

            string stored = $"EXEC sp_examSkillify_cnslUsroById @idUsuario={request.idUsuario}";

            var data = _context.Users.FromSqlRaw(stored).AsEnumerable().Select(x => x).SingleOrDefault();

            if (data != null)
            {
                response.id = data.Id;
                response.userName = data.UserName;
                response.password = data.Password;
                response.firstName = data.FirstName;
                response.lastName = data.LastName;
                response.email = data.Email;
                response.dateOfBirth = data.DateOfBirth;
                response.isEnabled = data.IsEnabled;
            }

            return await Task.FromResult(response);

        }

        public async Task<ResponseUsuarioModel> loginUsuario(RequestLoginModel request, bool status)
        {
            ResponseUsuarioModel response = new ResponseUsuarioModel();

            string stored = $"EXEC sp_examSkillify_lginUsro @usuario='{request.usuario}', @password='{request.password}', @status={status}";

            var data = _context.Users.FromSqlRaw(stored).AsEnumerable().Select(x => x).SingleOrDefault();

            if (data != null)
            {
                response.id = data.Id;
                response.userName = data.UserName;
                response.password = data.Password;
                response.firstName = data.FirstName;
                response.lastName = data.LastName;
                response.email = data.Email;
                response.dateOfBirth = data.DateOfBirth;
                response.isEnabled = true;
            }

            return await Task.FromResult(response);
        }

        public async Task logOutUsuario(RequestBody request, bool status)
        {
            ResponseUsuarioModel response = new ResponseUsuarioModel();

            string stored = $"EXEC sp_examSkillify_lguUsro @usuario='{request.usuario}', @status={status}";

           var data = _context.Users.FromSqlRaw(stored).AsEnumerable().Select(x => x).SingleOrDefault();

            await Task.FromResult(data);
        }

        public async Task<List<ResponseUsuarioModel>> consultarListaUsuarios()
        {
            List<ResponseUsuarioModel> response = new List<ResponseUsuarioModel>();

            string stored = $"EXEC sp_examSkillify_cnslLstaUsros";

            var data = _context.Users.FromSqlRaw(stored).AsEnumerable().Select(x => x).ToList();

            if (data != null)
            {
                foreach ( var item in data )
                {
                    ResponseUsuarioModel responseData = new ResponseUsuarioModel();
                    responseData.id = item.Id;
                    responseData.userName = item.UserName;
                    responseData.password = item.Password;
                    responseData.firstName = item.FirstName;
                    responseData.lastName = item.LastName;
                    responseData.email = item.Email;
                    responseData.dateOfBirth = item.DateOfBirth;
                    responseData.isEnabled = item.IsEnabled;
                    response.Add(responseData);
                }
            }

            return await Task.FromResult(response);
        }

        public async Task agregarUsuario(RequestAddUsuarioModel request)
        {
            ResponseUsuarioModel response = new ResponseUsuarioModel();

            string stored = $"EXEC sp_examSkillify_AddUsro @usuario='{request.userName}', @correo='{request.email}', @nombres='{request.firstName}', @apellidos='{request.lastName}', @fechaCumple='{request.dateOfBirth}', @password='{request.password}', @status={request.isEnabled}";

            var data = _context.Users.FromSqlRaw(stored).AsEnumerable().Select(x => x).SingleOrDefault();

            await Task.FromResult(data);
        }

        public async Task modificarUsuario(RequestModUsuarioModel request)
        {
            ResponseUsuarioModel response = new ResponseUsuarioModel();

            string stored = $"EXEC sp_examSkillify_ModUsro @idUsuario={request.idUsuarioMod}, @correo='{request.emailMod}', @nombres='{request.firstNameMod}', @apellidos='{request.lastNameMod}', @fechaCumple='{request.dateOfBirthMod}', @password='{request.passwordMod}', @status={request.isEnabledMod}";

            var data = _context.Users.FromSqlRaw(stored).AsEnumerable().Select(x => x).SingleOrDefault();

            await Task.FromResult(data);
        }

        public async Task eliminarUsuario(RequestDltUsuarioModel request)
        {
            ResponseUsuarioModel response = new ResponseUsuarioModel();

            string stored = $"EXEC sp_examSkillify_DltUsro @idUsuario={request.idUsuarioDel}";

            var data = _context.Users.FromSqlRaw(stored).AsEnumerable().Select(x => x).SingleOrDefault();

            await Task.FromResult(data);
        }

        public async Task<bool> validarUsuario(RequestValiUsuarioModel request)
        {
            var response = await _context.Users.Where(x => x.UserName == request.usuarioValid).AnyAsync();

            return await Task.FromResult(response);
        }

        #endregion

        #region Metodos Privados

        #endregion

    }
}
