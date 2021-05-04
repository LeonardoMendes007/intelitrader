using AppCadastro.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppCadastro.Service
{
    interface IRestService
    {
        HttpStatusCode postUser(Model.UserCreate user);
        Task<IEnumerable<UserRead>> getAllUsersAsync();
    }
}
