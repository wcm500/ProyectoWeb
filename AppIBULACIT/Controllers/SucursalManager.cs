using AppIBULACIT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppIBULACIT.Controllers
{
    public class SucursalManager
    {
        string UrlBase = "http://localhost:49220/api/Sucursal/";

        HttpClient GetClient(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;
        }

        public async Task<Sucursal> ObtenerServicio(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(string.Concat(UrlBase, codigo));

            return JsonConvert.DeserializeObject<Sucursal>(response);
        }

        public async Task<IEnumerable<Sucursal>> ObtenerServicios(string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(UrlBase);

            return JsonConvert.DeserializeObject<IEnumerable<Sucursal>>(response);
        }

        public async Task<Sucursal> Ingresar(Sucursal sucursal, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(sucursal),
                Encoding.UTF8,
                "application/json"));

            return JsonConvert.DeserializeObject<Sucursal>(await
                response.Content.ReadAsStringAsync());
        }

        public async Task<Sucursal> Actualizar(Sucursal sucursal, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(sucursal),
                Encoding.UTF8,
                "application/json"));

            return JsonConvert.DeserializeObject<Sucursal>(await response.
                Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string id, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(UrlBase, id));

            return JsonConvert.DeserializeObject<string>(await
                response.Content.ReadAsStringAsync());
        }

    }
}
