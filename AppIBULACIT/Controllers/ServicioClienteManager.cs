using AppIBULACIT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AppIBULACIT.Controllers
{
    public class ServicioClienteManager
    {
        string UrlBase = "http://localhost:49220/api/ServicioCliente/";

        HttpClient GetClient(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;
        }

        public async Task<ServicioCliente> ObtenerServicioCliente(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(string.Concat(UrlBase, codigo));

            return JsonConvert.DeserializeObject<ServicioCliente>(response);
        }

        public async Task<IEnumerable<ServicioCliente>> ObtenerServicios(string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(UrlBase);

            return JsonConvert.DeserializeObject<IEnumerable<ServicioCliente>>(response);
        }

        public async Task<ServicioCliente> Ingresar(ServicioCliente servicioCliente, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(servicioCliente),
                Encoding.UTF8,
                "application/json"));

            return JsonConvert.DeserializeObject<ServicioCliente>(await
                response.Content.ReadAsStringAsync());
        }

        public async Task<ServicioCliente> Actualizar(ServicioCliente servicioCliente, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(servicioCliente),
                Encoding.UTF8,
                "application/json"));

            return JsonConvert.DeserializeObject<ServicioCliente>(await response.
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