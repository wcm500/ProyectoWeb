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
    public class PrestamoManager
    {
        string UrlBase = "http://localhost:49220/api/Prestamos/";

        HttpClient GetClient(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;
        }

        public async Task<Prestamos> ObtenerPrestamo(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(string.Concat(UrlBase, codigo));

            return JsonConvert.DeserializeObject<Prestamos>(response);
        }

        public async Task<IEnumerable<Prestamos>> ObtenerPrestamos(string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(UrlBase);

            return JsonConvert.DeserializeObject<IEnumerable<Prestamos>>(response);
        }

        public async Task<Prestamos> Ingresar(Prestamos prestamo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(prestamo),
                Encoding.UTF8,
                "application/json"));

            return JsonConvert.DeserializeObject<Prestamos>(await
                response.Content.ReadAsStringAsync());
        }

        public async Task<Prestamos> Actualizar(Prestamos prestamo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(prestamo),
                Encoding.UTF8,
                "application/json"));

            return JsonConvert.DeserializeObject<Prestamos>(await response.
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