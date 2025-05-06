using System;
using System.Collections.Generic;

namespace TFG_V0._01.Modelo;

public partial class Casosrelacionado
{
    public int Id { get; set; }

    public int IdCaso { get; set; }

    public int IdCasoRelacionado { get; set; }

    public virtual Caso IdCasoNavigation { get; set; } = null!;

    public virtual Caso IdCasoRelacionadoNavigation { get; set; } = null!;
}
