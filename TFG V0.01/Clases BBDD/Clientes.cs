using System.Text.Json.Serialization;

namespace TFG_V0._01.Clases_BBDD
{
    class Clientes
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("apellido1")]
        public string Apellido1 { get; set; }

        [JsonPropertyName("apellido2")]
        public string Apellido2 { get; set; }

        [JsonPropertyName("telefono")]
        public string Telefono { get; set; }

        [JsonPropertyName("telefono2")]
        public string Telefono2 { get; set; }

        [JsonPropertyName("direccion")]
        public string Direccion { get; set; }

        [JsonPropertyName("correo")]
        public string Correo { get; set; }

        [JsonPropertyName("correo2")]
        public string Correo2 { get; set; }
    }
}
