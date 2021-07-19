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
    public class SolicitudTarjetaManager
    {

        string UrlBase = "http://localhost:49220/api/SolicitudTarjeta/";


        HttpClient GetClient(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;
        }

        public async Task<SolicitidTarjeta> ObtenerSolicitudTarjeta(string codigo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await
                httpClient.GetStringAsync(string.Concat(UrlBase, codigo));

            return JsonConvert.DeserializeObject<SolicitidTarjeta>(response);
        }

        public async Task<IEnumerable<SolicitidTarjeta>> ObtenerSolicitudTarjetas(string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await
                httpClient.GetStringAsync(UrlBase);

            return JsonConvert.DeserializeObject<IEnumerable<SolicitidTarjeta>>(response);
        }

        public async Task<SolicitidTarjeta> Ingresar(SolicitidTarjeta solicitudTarjeta, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(solicitudTarjeta), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<SolicitidTarjeta>(await response.Content.ReadAsStringAsync());
        }

        public async Task<SolicitidTarjeta> Actualizar(SolicitidTarjeta solicitudTarjeta, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(solicitudTarjeta), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<SolicitidTarjeta>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string id, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(UrlBase, id));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }


    }
}