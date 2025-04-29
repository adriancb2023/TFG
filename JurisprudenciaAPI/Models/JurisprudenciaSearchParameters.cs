using System;
using System.Collections.Generic;

namespace JurisprudenciaApi.Models
{
    public class JurisprudenciaSearchParameters
    {
        public string? TextoLibre { get; set; }
        public string? Jurisdiccion { get; set; } // Ejemplo: "Civil", "Penal"
        public string? TipoResolucion { get; set; } // Ejemplo: "Sentencia", "Auto"
        public string? SubTipoResolucion { get; set; }
        public string? TipoOrgano { get; set; }
        public string? Roj { get; set; } // Número ROJ
        public string? Ecli { get; set; } // Identificador ECLI
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string? NumeroRecurso { get; set; }
        public string? Ponente { get; set; }

        // Añadir más propiedades según sea necesario para cubrir todos los campos del formulario CENDOJ
    }
} 