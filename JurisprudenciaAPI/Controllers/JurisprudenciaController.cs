using Microsoft.AspNetCore.Mvc;
using JurisprudenciaApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using System.Linq;
using System;
using System.Net.Http.Headers;
using System.Web;
using System.Net;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace JurisprudenciaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JurisprudenciaController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _cache;
        private readonly ILogger<JurisprudenciaController> _logger;
        private readonly CookieContainer _cookieContainer = new CookieContainer();

        // Mapeo de Órganos Judiciales
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

        // Mapa de idiomas
        private static readonly Dictionary<string, string> IdiomaMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Español", "es" }, // Guessed code
            { "Català", "ca" }, // Guessed code
            { "Galego", "gl" }, // Guessed code
            { "Euskera", "eu" }  // Guessed code
        };

        // Política de reintentos
        private static readonly AsyncRetryPolicy<HttpResponseMessage> RetryPolicy =
            Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .OrResult(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(3, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        public JurisprudenciaController(
            IHttpClientFactory httpClientFactory,
            IMemoryCache cache,
            ILogger<JurisprudenciaController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
            _logger = logger;
        }

        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<JurisprudenciaResult>>> Search([FromBody] JurisprudenciaSearchParameters parameters)
        {
            if (parameters == null)
                return BadRequest("Parámetros de búsqueda no pueden ser nulos");

            // Validación de fechas
            if (parameters.FechaDesde.HasValue && parameters.FechaHasta.HasValue &&
                parameters.FechaDesde > parameters.FechaHasta)
            {
                return BadRequest("FechaDesde no puede ser mayor que FechaHasta");
            }

            // Cache key
            var cacheKey = $"search_{System.Text.Json.JsonSerializer.Serialize(parameters)}";
            if (_cache.TryGetValue(cacheKey, out List<JurisprudenciaResult> cachedResults))
            {
                _logger.LogInformation("Retornando resultados desde caché");
                return Ok(cachedResults);
            }

            try
            {
                var client = _httpClientFactory.CreateClient("PoderJudicial");
                var requestUrl = "https://www.poderjudicial.es/search/search.action";

                // Configurar headers
                var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                ConfigureRequestHeaders(request);

                // Construir formulario
                var formData = BuildFormData(parameters);
                request.Content = new FormUrlEncodedContent(formData);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                // Intento de conexión con retry
                var response = await RetryPolicy.ExecuteAsync(async () =>
                {
                    _logger.LogDebug("Enviando petición al Poder Judicial...");
                    var res = await client.SendAsync(request);

                    if (!res.IsSuccessStatusCode)
                    {
                        var errorContent = await res.Content.ReadAsStringAsync();
                        _logger.LogWarning("Error en la respuesta: {StatusCode} - {Content}",
                            res.StatusCode, errorContent);
                    }

                    res.EnsureSuccessStatusCode();
                    return res;
                });

                string responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("Respuesta recibida. Longitud: {Length} chars", responseBody.Length);

                var parsedResults = ParseHtmlResponse(responseBody);
                _logger.LogInformation("Parseados {Count} resultados", parsedResults.Count);

                // Cachear resultados por 30 minutos
                _cache.Set(cacheKey, parsedResults, TimeSpan.FromMinutes(30));

                return Ok(parsedResults);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Error al consultar el Poder Judicial. Status: {StatusCode}", e.StatusCode);
                return StatusCode((int?)e.StatusCode ?? 500, "Error al conectar con el servicio del Poder Judicial");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al procesar la búsqueda");
                return StatusCode(500, "Error interno al procesar la solicitud");
            }
        }

        private void ConfigureRequestHeaders(HttpRequestMessage request)
        {
            request.Headers.Clear();
            request.Headers.Add("Accept", "text/html, */*; q=0.01");
            request.Headers.Add("Accept-Language", "es-ES,es;q=0.6");
            request.Headers.Add("Origin", "https://www.poderjudicial.es");
            request.Headers.Add("Referer", "https://www.poderjudicial.es/search/indexAN.jsp");
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.Headers.Add("Sec-Fetch-Dest", "empty");
            request.Headers.Add("Sec-Fetch-Mode", "cors");
            request.Headers.Add("Sec-Fetch-Site", "same-origin");
            request.Headers.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        }

        private Dictionary<string, string> BuildFormData(JurisprudenciaSearchParameters parameters)
        {
            var formData = new Dictionary<string, string>
            {
                { "action", "query" }, // Cambiado a "query" según la petición real
                { "sort", "IN_FECHARESOLUCION:decreasing" },
                { "recordsPerPage", "20" }, // Cambiado a "recordsPerPage"
                { "databasematch", "AN" }, // Añadido según petición real
                { "start", "1" } // Cambiado a "start"
            };

            // Mapeo de parámetros actualizado
            if (!string.IsNullOrEmpty(parameters.TextoLibre))
                formData["TEXT"] = parameters.TextoLibre; // Cambiado a "TEXT"

            if (!string.IsNullOrEmpty(parameters.Jurisdiccion))
                formData["JURISDICCION"] = $"|{parameters.Jurisdiccion.ToUpper()}|";

            if (parameters.TiposResolucion?.Any() == true)
                formData["TIPORESOLUCION"] = string.Join("||", parameters.TiposResolucion.Select(tr => $"|{tr.ToUpper()}|"));

            if (parameters.OrganosJudiciales?.Any() == true)
            {
                formData["TIPOORGANO"] = string.Join("|", parameters.OrganosJudiciales);
                formData["field"] = "TIPOORGANO";
            }

            // Resto de parámetros
            AddIfNotNull(formData, "ROJ", parameters.Roj);
            AddIfNotNull(formData, "ECLI", parameters.Ecli);
            AddIfNotNull(formData, "NUMERO_RESOLUCION", parameters.NumeroResolucion);
            AddIfNotNull(formData, "NUMERO_RECURSO", parameters.NumeroRecurso);
            AddIfNotNull(formData, "PONENTE", parameters.Ponente);
            AddIfNotNull(formData, "SECCION", parameters.Seccion);
            AddIfNotNull(formData, "NORMA_CITADA", parameters.Legislacion);

            if (parameters.FechaDesde.HasValue)
                formData["FECHA_DESDE"] = parameters.FechaDesde.Value.ToString("dd/MM/yyyy");

            if (parameters.FechaHasta.HasValue)
                formData["FECHA_HASTA"] = parameters.FechaHasta.Value.ToString("dd/MM/yyyy");

            if (!string.IsNullOrEmpty(parameters.Idioma) && IdiomaMap.TryGetValue(parameters.Idioma, out var idiomaCode))
                formData["IDIOMA"] = idiomaCode;

            if (parameters.Localizaciones?.Any() == true)
                formData["AMBITOTERRITORIAL"] = string.Join("||", parameters.Localizaciones.Select(l => $"|{l}|"));

            _logger.LogDebug("FormData construido: {@FormData}", formData);
            return formData;
        }

        private List<JurisprudenciaResult> ParseHtmlResponse(string html)
        {
            var results = new List<JurisprudenciaResult>();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            // Selectores mejorados para resultados
            var resultNodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'resultado')]") ??
                             htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'result-item')]") ??
                             htmlDoc.DocumentNode.SelectNodes("//li[contains(@class, 'resultado_busqueda')]");

            if (resultNodes == null || !resultNodes.Any())
            {
                _logger.LogWarning("No se encontraron nodos de resultados. HTML recibido (inicio): {HtmlStart}",
                    html.Length > 500 ? html.Substring(0, 500) + "..." : html);
                return results;
            }

            foreach (var node in resultNodes)
            {
                try
                {
                    var result = new JurisprudenciaResult
                    {
                        Roj = node.SelectSingleNode(".//span[contains(@class, 'roj')]")?.InnerText?.Trim() ??
                              node.SelectSingleNode(".//span[contains(text(), 'Roj:')]/following-sibling::text()")?.InnerText?.Trim(),

                        Ecli = node.SelectSingleNode(".//span[contains(@class, 'ecli')]")?.InnerText?.Trim() ??
                               node.SelectSingleNode(".//span[contains(text(), 'ECLI:')]/following-sibling::text()")?.InnerText?.Trim(),

                        FechaResolucion = node.SelectSingleNode(".//span[contains(@class, 'fecha')]")?.InnerText?.Trim() ??
                                         node.SelectSingleNode(".//span[contains(text(), 'Fecha:')]/following-sibling::text()")?.InnerText?.Trim(),

                        TipoResolucion = node.SelectSingleNode(".//*[contains(text(), 'Resolución')]/following-sibling::span")?.InnerText?.Trim(),
                        OrganoJudicial = node.SelectSingleNode(".//*[contains(text(), 'Órgano')]/following-sibling::span")?.InnerText?.Trim(),
                        Ponente = node.SelectSingleNode(".//*[contains(text(), 'Ponente')]/following-sibling::span")?.InnerText?.Trim(),
                        Resumen = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[contains(@class, 'resumen')]")?.InnerText?.Trim()),
                        UrlDocumento = GetAbsoluteUrl(node.SelectSingleNode(".//a[contains(@href, 'download')]")?.GetAttributeValue("href", ""))
                    if (_cache.TryGetValue(cacheKey, out List<JurisprudenciaResult>? cachedResults) && cachedResults != null)
                    {
                        _logger.LogInformation("Retornando resultados desde caché");
                        return Ok(cachedResults);
                    }
                    };

                    // Limpieza de datos
                    result.Roj = CleanText(result.Roj);
                    result.Ecli = CleanText(result.Ecli);
                    result.FechaResolucion = CleanText(result.FechaResolucion);

                    if (!string.IsNullOrWhiteSpace(result.Roj) || !string.IsNullOrWhiteSpace(result.Ecli))
                    {
                        results.Add(result);
                    }
                })
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error al parsear un resultado individual");
                }
            }

            return results;
        }

        // ... (mantener los mismos métodos auxiliares CleanText, GetAbsoluteUrl, AddIfNotNull)

        [HttpGet("initialData")]
        public ActionResult<InitialDataResponse> GetInitialData()
        {
            return Ok(new InitialDataResponse
            {
                Jurisdicciones = new List<string> { "Civil", "Penal", "Laboral", "Administrativo" },
                TiposResolucion = new List<string> { "Sentencia", "Auto", "Providencia" },
                OrganosJudiciales = TipoOrganoMap.Keys.OrderBy(x => x).ToList(),
                Localizaciones = new List<string> { "Madrid", "Barcelona", "Sevilla", "Valencia", "Bilbao" }
            });
        }
        private void AddIfNotNull(Dictionary<string, string> dict, string key, string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                dict[key] = value;
            }
        }
        private string GetAbsoluteUrl(string? relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl) || relativeUrl.StartsWith("http"))
                return relativeUrl ?? string.Empty;

            try
            {
                return new Uri(new Uri("https://www.poderjudicial.es"), relativeUrl).AbsoluteUri;
            }
            catch
            {
                return relativeUrl ?? string.Empty;
            }
        }
        private string CleanText(string? text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            return text.Replace("Roj:", "")
                       .Replace("ECLI:", "")
                       .Replace("Fecha:", "")
                       .Trim();
        }
    }

    public class InitialDataResponse
    {
        public List<string> Jurisdicciones { get; set; } = new List<string>();
        public List<string> TiposResolucion { get; set; } = new List<string>();
        public List<string> OrganosJudiciales { get; set; } = new List<string>();
        public List<string> Localizaciones { get; set; } = new List<string>();
    }
}