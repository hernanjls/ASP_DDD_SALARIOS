using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Salarios.Dominio.Entidades
{
    public partial class Division
    {
        [Key]
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }

    }
}
