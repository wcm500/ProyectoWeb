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
    public class TipoTarjetaManager
    {
        string UrlBase = "http://localhost:49220/api/TipoTarjeta/";

        HttpClient GetClient(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;
        }

        public async Task<TipoTarjeta> ObtenerTipoTarjeta(string codigo, string token)
        {
            HttpClient httpClient = GetClient(token);
            var response = await
                httpClient.GetStringAsync(string.Concat(UrlBase, codigo));

            return JsonConvert.DeserializeObject<TipoTarjeta>(response);
        }

        public async Task<IEnumerable<TipoTarjeta>> ObtenerTipoTarjetas(string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await
                httpClient.GetStringAsync(UrlBase);

            return JsonConvert.DeserializeObject<IEnumerable<TipoTarjeta>>(response);
        }

        public async Task<TipoTarjeta> Ingresar(TipoTarjeta tipoTarjeta, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(tipoTarjeta), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<TipoTarjeta>(await response.Content.ReadAsStringAsync());
        }

        public async Task<TipoTarjeta> Actualizar(TipoTarjeta tipoTarjeta, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(tipoTarjeta), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<TipoTarjeta>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string id, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(UrlBase, id));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}