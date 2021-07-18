﻿using AppIBULACIT.Models;
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
    public class TipoPrestamoManager
    {
        string UrlBase = "http://localhost:49220/api/TipoPrestamo/";

        HttpClient GetClient(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;
        }

        public async Task<Tipo_Prestamo> ObtenerTipoPrestamo(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(string.Concat(UrlBase, codigo));

            return JsonConvert.DeserializeObject<Tipo_Prestamo>(response);
        }

        public async Task<IEnumerable<Tipo_Prestamo>> ObtenerTiposPrestamos(string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(UrlBase);

            return JsonConvert.DeserializeObject<IEnumerable<Tipo_Prestamo>>(response);
        }

        public async Task<Tipo_Prestamo> Ingresar(Tipo_Prestamo tipo_prestamo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(tipo_prestamo),
                Encoding.UTF8,
                "application/json"));

            return JsonConvert.DeserializeObject<Tipo_Prestamo>(await
                response.Content.ReadAsStringAsync());
        }

        public async Task<Tipo_Prestamo> Actualizar(Tipo_Prestamo tipo_prestamo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(tipo_prestamo),
                Encoding.UTF8,
                "application/json"));

            return JsonConvert.DeserializeObject<Tipo_Prestamo>(await response.
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