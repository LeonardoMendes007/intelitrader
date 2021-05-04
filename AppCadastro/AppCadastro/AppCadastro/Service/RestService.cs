using AppCadastro.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppCadastro.Service
{
    class RestService : IRestService
    {
        private Uri _uri = new Uri("http://192.168.0.204:8080/Users");
        private HttpClient _client = new HttpClient();
        public RestService()
        {

        }

        public async Task<IEnumerable<UserRead>> getAllUsersAsync()
        {
            var response = _client.GetAsync(_uri);
            if(response.Result.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception();
            }

            string content = await response.Result.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<List<UserRead>>(content);

            return resultado;
        }

        public HttpStatusCode postUser(UserCreate user)
        {
            var json = JsonConvert.SerializeObject(user);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _client.PostAsync(_uri, contentJson);

            return response.Result.StatusCode;
        }
    }
}
