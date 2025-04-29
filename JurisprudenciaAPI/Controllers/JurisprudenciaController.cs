using Microsoft.AspNetCore.Mvc;
using JurisprudenciaApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using System.Linq;
using System;
using System.Net.Http.Headers;

namespace JurisprudenciaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JurisprudenciaController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // Mapeo de Nombre de Órgano a Código (extraído del HTML)
        private static readonly Dictionary<string, string> TipoOrganoMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Tribunal Supremo", "11|12|13|14|15|16" },
            { "Tribunal Supremo. Sala de lo Civil", "11" },
            { "Tribunal Supremo. Sala de lo Penal", "12" },
            { "Tribunal Supremo. Sala de lo Contencioso", "13" },
            { "Tribunal Supremo. Sala de lo Social", "14" },
            { "Tribunal Supremo. Sala de lo Militar", "15" },
            { "Tribunal Supremo. Sala de lo Especial", "16" },
            { "Audiencia Nacional", "22|2264|23|24|25|26|27|28|29" },
            { "Audiencia Nacional. Sala de lo Penal", "22" },
            { "Sala de Apelación de la Audiencia Nacional", "2264" },
            { "Audiencia Nacional. Sala de lo Contencioso", "23" },
            { "Audiencia Nacional. Sala de lo Social", "24" },
            { "Audiencia Nacional. Juzgados Centrales de Instrucción", "27" },
            { "Audiencia Nacional. Juzgado Central de Menores", "26" },
            { "Audiencia Nacional. Juzgado Central de Vigilancia Penitenciaria", "25" },
            { "Audiencia Nacional. Juzgados Centrales de lo Contencioso", "29" },
            { "Audiencia Nacional. Juzgados Centrales de lo Penal", "28" },
            { "Tribunal Superior de Justicia", "31|31201202|33|34" },
            { "Tribunal Superior de Justicia. Sala de lo Civil y Penal", "31" },
            { "Sección de Apelación Penal. TSJ Sala de lo Civil y Penal", "31201202" },
            { "Tribunal Superior de Justicia. Sala de lo Contencioso", "33" },
            { "Tribunal Superior de Justicia. Sala de lo Social", "34" },
            { "Audiencia Provincial", "37" },
            { "Audiencia Provincial. Tribunal Jurado", "38" },
            { "Tribunal de Marca de la UE", "1001" },
            { "Juzgado de Primera Instancia", "42" },
            { "Juzgado de Instrucción", "43" },
            { "Juzgado de lo Contencioso Administrativo", "45" },
            { "Juzgado de Menores", "53" },
            { "Juzgado de Primera Instancia e Instrucción", "41" },
            { "Juzgado de lo Mercantil", "47" },
            { "Juzgados de Marca de la UE", "1002" },
            { "Juzgado de lo Penal", "51" },
            { "Juzgado de lo Social", "44" },
            { "Juzgado de Vigilancia Penitenciaria", "52" },
            { "Juzgado de Violencia sobre la Mujer", "48" },
            { "Tribunal Militar Territorial", "83" },
            { "Tribunal Militar Central", "85" },
            { "Consejo Supremo de Justicia Militar", "75" },
            { "Audiencia Territorial", "36" }
        };

        public JurisprudenciaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<string>>> Search([FromBody] JurisprudenciaSearchParameters parameters)
        {
            if (parameters == null)
            {
                return BadRequest("Search parameters cannot be null.");
            }

            var results = new List<string>();
            var client = _httpClientFactory.CreateClient();
            var requestUrl = "https://www.poderjudicial.es/search/search.action";

            try
            {
                var formData = new Dictionary<string, string>
                {
                    // Parámetros fijos necesarios
                    { "action", "getQueryAllTagValues" },
                    { "sort", "IN_FECHARESOLUCION:decreasing" },
                    { "recordsPerPage", "10" },
                    { "databasematch", "AN" },
                    { "start", "1" },
                    { "idtab", "jurisprudencia" }
                };

                // --- Mapeo Actualizado de Parámetros ---
                // TextoLibre omitido según solicitud
                if (!string.IsNullOrEmpty(parameters.Jurisdiccion))
                {
                    formData["JURISDICCION"] = $"|{parameters.Jurisdiccion.ToUpper()}|";
                }
                if (!string.IsNullOrEmpty(parameters.TipoResolucion))
                {
                     // Mapeamos al tipo principal. Formato y nombre asumidos.
                     formData["TIPORESOLUCION"] = $"|{parameters.TipoResolucion.ToUpper()}|";
                }
                if (!string.IsNullOrEmpty(parameters.SubTipoResolucion))
                {
                    formData["SUBTIPORESOLUCION"] = $"|{parameters.SubTipoResolucion.ToUpper()}|";
                }
                if (!string.IsNullOrEmpty(parameters.TipoOrgano))
                {
                    // Buscar el código correspondiente en el diccionario
                    if (TipoOrganoMap.TryGetValue(parameters.TipoOrgano, out string? codigoOrgano))
                    {
                         formData["TIPOORGANOPUB"] = $"|{codigoOrgano}|"; // <-- Mapeado con código y pipes
                         formData["field"] = "TIPOORGANO"; // Parece necesario cuando se usa TIPOORGANOPUB
                    }
                    else
                    {
                         results.Add($"WARN: TipoOrgano '{parameters.TipoOrgano}' not found in mapping dictionary. Please use exact names from the dropdown.");
                    }
                }
                if (!string.IsNullOrEmpty(parameters.Roj))
                {
                     formData["ROJ"] = parameters.Roj; // <-- Mapeado
                }
                 if (!string.IsNullOrEmpty(parameters.Ecli))
                {
                     formData["ECLI"] = parameters.Ecli; // <-- Mapeado
                }
                if (parameters.FechaDesde.HasValue)
                {
                     formData["FECHARESOLUCIONDESDE"] = parameters.FechaDesde.Value.ToString("dd/MM/yyyy"); // <-- Mapeado
                }
                if (parameters.FechaHasta.HasValue)
                {
                     formData["FECHARESOLUCIONHASTA"] = parameters.FechaHasta.Value.ToString("dd/MM/yyyy"); // <-- Mapeado
                }
                 if (!string.IsNullOrEmpty(parameters.NumeroRecurso))
                {
                     formData["NUMERORECURSO"] = parameters.NumeroRecurso; // <-- Mapeado
                }
                if (!string.IsNullOrEmpty(parameters.Ponente))
                {
                    formData["PONENTE"] = parameters.Ponente; // <-- Mapeado
                }
                // --- Fin Mapeo ---

                var content = new FormUrlEncodedContent(formData);

                // Simular cabeceras de navegador comunes (mantener)
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html")); // Quizás debería ser application/json si la respuesta es JSON? A confirmar.
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("es-ES"));
                // Podría ser necesario añadir 'X-Requested-With': 'XMLHttpRequest' si es una petición AJAX
                // client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

                // Realizar la petición POST
                HttpResponseMessage response = await client.PostAsync(requestUrl, content);

                // ... (Manejo de la respuesta igual que antes: StatusCode, HTML/Error Content) ...
                results.Add($"Status Code: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    results.Add($"Response Content-Type: {response.Content.Headers.ContentType}"); // Ver si es HTML o JSON
                    results.Add("Response Body (first 500 chars):");
                    results.Add(responseBody.Length > 500 ? responseBody.Substring(0, 500) + "..." : responseBody);

                     // --- Lógica de Parseo Pendiente ---
                    // Si Content-Type es HTML -> Usar HtmlAgilityPack
                    // Si Content-Type es JSON -> Usar System.Text.Json o Newtonsoft.Json
                }
                else
                {
                     results.Add($"Error: {response.ReasonPhrase}");
                     string errorContent = await response.Content.ReadAsStringAsync();
                     results.Add($"Error Content (first 500 chars): { (errorContent.Length > 500 ? errorContent.Substring(0, 500) + "..." : errorContent)}");
                }
            }
            // ... (catch blocks igual que antes) ...
            catch (HttpRequestException e)
            {
                results.Add($"Request Exception: {e.Message}");
                if (e.InnerException != null)
                {
                    results.Add($"Inner Exception: {e.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                 results.Add($"Generic Exception: {ex.Message}");
                 if (ex.InnerException != null)
                {
                    results.Add($"Inner Exception: {ex.InnerException.Message}");
                }
            }


            return Ok(results);
        }

        // Ya no necesitamos la función MapearTipoOrganoACodigo, usamos el diccionario directamente.
    }
} 