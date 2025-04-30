using Microsoft.AspNetCore.Mvc;
using JurisprudenciaApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using System.Linq;
using System;
using System.Net.Http.Headers;
using System.Web; // Added for HtmlDecode

namespace JurisprudenciaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JurisprudenciaController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // Mapeo de Nombre de Órgano a Código (extraído del HTML)
        // Note: This map might need updates based on the exact values provided by the CENDOJ website's filters.
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

        // Potential map for Idioma if CENDOJ expects codes (needs verification)
        private static readonly Dictionary<string, string> IdiomaMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Español", "es" }, // Guessed code
            { "Català", "ca" }, // Guessed code
            { "Galego", "gl" }, // Guessed code
            { "Euskera", "eu" }  // Guessed code
            // "Todos" would likely mean omitting the parameter
        };

        public JurisprudenciaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("search")]
        // Changed return type
        public async Task<ActionResult<IEnumerable<JurisprudenciaResult>>> Search([FromBody] JurisprudenciaSearchParameters parameters)
        {
            if (parameters == null)
            {
                return BadRequest("Search parameters cannot be null.");
            }

            // var results = new List<string>(); // Old return type
            var client = _httpClientFactory.CreateClient();
            // Base URL might change, verify this is the correct endpoint for POST searches
            var requestUrl = "https://www.poderjudicial.es/search/index.action"; // Adjusted endpoint based on observation, needs verification

            try
            {
                var formData = new Dictionary<string, string>
                {
                    // Parámetros fijos necesarios (Verify these are correct for 'index.action')
                    { "action", "search" }, // Action might be different
                    { "sort", "IN_FECHARESOLUCION:decreasing" },
                    { "page", "1" }, // Use 'page' instead of 'start' perhaps?
                    { "pageSize", "20" }, // Use 'pageSize' instead of 'recordsPerPage' perhaps? CENDOJ uses 10/20/50
                    { "searchType", "jurisprudencia" }, // Likely needed
                    { "historic", "false" } // Often needed
                    // { "databasematch", "AN" }, // May not be needed with index.action
                    // { "idtab", "jurisprudencia" } // May not be needed with index.action
                };

                // --- Mapeo Actualizado de Parámetros ---

                // TextoLibre (Free text search) - Verify field name (e.g., "TEXTOLIBRE" or "query")
                if (!string.IsNullOrEmpty(parameters.TextoLibre))
                {
                     formData["TEXTOLIBRE"] = parameters.TextoLibre; // ASSUMED field name
                }

                // Jurisdiccion (Single value) - Verify field name (e.g., "JURISDICCION")
                if (!string.IsNullOrEmpty(parameters.Jurisdiccion))
                {
                    formData["JURISDICCION"] = $"|{parameters.Jurisdiccion.ToUpper()}|"; // Format assumption
                }

                // TiposResolucion (List) - Verify field name (e.g., "TIPORESOLUCION") and format
                if (parameters.TiposResolucion?.Any() == true)
                {
                    // Assuming format is |Value1||Value2|...|
                    formData["TIPORESOLUCION"] = string.Join("||", parameters.TiposResolucion.Select(tr => $"|{tr.ToUpper()}|")); // ASSUMED field name and format
                }

                // OrganosJudiciales (List of Codes) - Verify field name (e.g., "TIPOORGANO" or "ORGANOJURISDICCIONAL")
                // Using TIPOORGANOPUB and field=TIPOORGANO as before, but with joined codes
                 if (parameters.OrganosJudiciales?.Any() == true)
                 {
                     // Assuming OrganosJudiciales contains the CODES (e.g., "11", "37") directly from mapping/UI
                     formData["TIPOORGANOPUB"] = $"|{string.Join("|", parameters.OrganosJudiciales)}|"; // ASSUMED format (|code1|code2|)
                     formData["field"] = "TIPOORGANO"; // Seems required when using TIPOORGANOPUB
                 }


                // Roj
                if (!string.IsNullOrEmpty(parameters.Roj))
                {
                     formData["ROJ"] = parameters.Roj;
                }

                // Ecli
                 if (!string.IsNullOrEmpty(parameters.Ecli))
                {
                     formData["ECLI"] = parameters.Ecli;
                }

                // FechaDesde
                if (parameters.FechaDesde.HasValue)
                {
                     // Verify field name (e.g., "FECHA_DESDE" or "fechaDesde")
                     formData["FECHA_DESDE"] = parameters.FechaDesde.Value.ToString("dd/MM/yyyy"); // ASSUMED field name
                }

                // FechaHasta
                if (parameters.FechaHasta.HasValue)
                {
                     // Verify field name (e.g., "FECHA_HASTA" or "fechaHasta")
                     formData["FECHA_HASTA"] = parameters.FechaHasta.Value.ToString("dd/MM/yyyy"); // ASSUMED field name
                }

                // NumeroResolucion - Verify field name (e.g., "NUMERO_RESOLUCION")
                if (!string.IsNullOrEmpty(parameters.NumeroResolucion))
                {
                     formData["NUMERO_RESOLUCION"] = parameters.NumeroResolucion; // ASSUMED field name
                }


                // NumeroRecurso - Verify field name (e.g., "NUMERO_RECURSO")
                if (!string.IsNullOrEmpty(parameters.NumeroRecurso))
                {
                     formData["NUMERO_RECURSO"] = parameters.NumeroRecurso; // ASSUMED field name
                }

                // Ponente - Verify field name (e.g., "PONENTE")
                if (!string.IsNullOrEmpty(parameters.Ponente))
                {
                    formData["PONENTE"] = parameters.Ponente; // ASSUMED field name
                }

                // Seccion - Verify field name (e.g., "SECCION")
                 if (!string.IsNullOrEmpty(parameters.Seccion))
                {
                     formData["SECCION"] = parameters.Seccion; // ASSUMED field name
                }

                // Idioma - Verify field name (e.g., "IDIOMA")
                if (!string.IsNullOrEmpty(parameters.Idioma) && !parameters.Idioma.Equals("Todos", StringComparison.OrdinalIgnoreCase))
                {
                    if (IdiomaMap.TryGetValue(parameters.Idioma, out string? idiomaCode))
                    {
                         formData["IDIOMA"] = idiomaCode; // ASSUMED field name, using mapped code
                    }
                    // else { // Handle case where language isn't mapped? Maybe pass raw string? }
                }

                // Legislacion - Verify field name (e.g., "NORMA_CITADA") - How is this used? Exact match? Contains?
                if (!string.IsNullOrEmpty(parameters.Legislacion))
                {
                     formData["NORMA_CITADA"] = parameters.Legislacion; // ASSUMED field name
                }

                // Localizaciones (List) - Verify field name (e.g., "AMBITOTERRITORIAL") and format
                if (parameters.Localizaciones?.Any() == true)
                {
                    // Format is a wild guess - CENDOJ might use specific codes here too
                    formData["AMBITOTERRITORIAL"] = string.Join("||", parameters.Localizaciones.Select(l => $"|{l}|")); // ASSUMED field name and format
                }

                // --- Fin Mapeo ---

                var content = new FormUrlEncodedContent(formData);

                // Simular cabeceras de navegador comunes (mantener, potentially add more like Referer if needed)
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36"); // Updated UA
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("es-ES,es;q=0.9"));
                // client.DefaultRequestHeaders.Referrer = new Uri("https://www.poderjudicial.es/search/index.action"); // May be required

                // Realizar la petición POST
                HttpResponseMessage response = await client.PostAsync(requestUrl, content);
                response.EnsureSuccessStatusCode(); // Throw exception for non-2xx status codes earlier

                string responseBody = await response.Content.ReadAsStringAsync();

                // --- Lógica de Parseo con HtmlAgilityPack ---
                var parsedResults = new List<JurisprudenciaResult>();
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(responseBody);

                // *** CRITICAL ASSUMPTION ***: The structure below needs verification against actual CENDOJ results HTML.
                // Find the container for all results. Adjust XPath as needed.
                var resultNodes = htmlDoc.DocumentNode.SelectNodes("//li[contains(@class, 'resultado_busqueda')]"); // ASSUMED XPath for result items

                if (resultNodes != null)
                {
                    foreach (var node in resultNodes)
                    {
                        var result = new JurisprudenciaResult();

                        // Extract data using XPath relative to the current result node (.)
                        // Adjust XPaths based on actual HTML structure and classes/IDs.
                        result.Roj = node.SelectSingleNode(".//span[contains(@class, 'roj')]")?.InnerText.Trim(); // ASSUMED class
                        result.Ecli = node.SelectSingleNode(".//span[contains(@class, 'ecli')]")?.InnerText.Trim(); // ASSUMED class
                        result.FechaResolucion = node.SelectSingleNode(".//span[contains(@class, 'fecha')]")?.InnerText.Trim(); // ASSUMED class
                        result.TipoResolucion = node.SelectSingleNode(".//strong[contains(text(), 'Resolución:')]/following-sibling::span")?.InnerText.Trim(); // ASSUMED structure
                        result.OrganoJudicial = node.SelectSingleNode(".//strong[contains(text(), 'Órgano:')]/following-sibling::span")?.InnerText.Trim(); // ASSUMED structure
                        result.Ponente = node.SelectSingleNode(".//strong[contains(text(), 'Ponente:')]/following-sibling::span")?.InnerText.Trim(); // ASSUMED structure
                        result.Resumen = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[contains(@class, 'resumen_contenido')]")?.InnerText.Trim()); // ASSUMED class, decode HTML entities
                        result.UrlDocumento = node.SelectSingleNode(".//a[contains(@class, 'btn-primary') and contains(text(), 'Documento')]")?.Attributes["href"]?.Value; // ASSUMED class and text

                        // Make URL absolute if it's relative
                        if (!string.IsNullOrEmpty(result.UrlDocumento) && !result.UrlDocumento.StartsWith("http"))
                        {
                           result.UrlDocumento = new Uri(new Uri("https://www.poderjudicial.es"), result.UrlDocumento).ToString();
                        }


                        // Clean up extracted data if necessary (e.g., remove "Roj: ", "Ecli: ")
                        result.Roj = result.Roj?.Replace("Roj:","").Trim();
                        result.Ecli = result.Ecli?.Replace("ECLI:","").Trim();


                        parsedResults.Add(result);
                    }
                }
                // --- Fin Lógica de Parseo ---

                return Ok(parsedResults);

            }
            catch (HttpRequestException e)
            {
                // Log the error details
                Console.WriteLine($"Request Exception: {e.Message}");
                Console.WriteLine($"Status Code: {e.StatusCode}");
                 if (e.InnerException != null) Console.WriteLine($"Inner Exception: {e.InnerException.Message}");
                // Return a specific error response
                return StatusCode((int?)e.StatusCode ?? 500, $"Error communicating with CENDOJ service: {e.Message}");
            }
            catch (Exception ex)
            {
                 // Log the error details
                 Console.WriteLine($"Generic Exception: {ex.Message}");
                 if (ex.InnerException != null) Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                 // Return a generic server error
                 return StatusCode(500, $"An internal server error occurred: {ex.Message}");
            }
        }
        [HttpGet("initialData")]
        public ActionResult<InitialDataResponse> GetInitialData()
        {
            var response = new InitialDataResponse
            {
                Jurisdicciones = new List<string> { "Civil", "Penal", "Laboral", "Administrativo" },
                TiposResolucion = new List<string> { "Sentencia", "Auto", "Providencia" },
                OrganosJudiciales = TipoOrganoMap.Keys.ToList(),
                Localizaciones = new List<string> { "Madrid", "Barcelona", "Sevilla" }
            };

            return Ok(response);
        }

        // Ya no necesitamos la función MapearTipoOrganoACodigo, usamos el diccionario directamente.
    }
    public class InitialDataResponse
    {
        public List<string> Jurisdicciones { get; set; }
        public List<string> TiposResolucion { get; set; }
        public List<string> OrganosJudiciales { get; set; }
        public List<string> Localizaciones { get; set; }
    }

}