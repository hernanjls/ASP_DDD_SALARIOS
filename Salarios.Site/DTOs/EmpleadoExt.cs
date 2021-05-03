using Salarios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Salarios.Site.DTOs
{
    public class EmpleadoExt : Empleado
    {
        [Display(Name = "División")]
        public string DivisionName { get; set; }
        [Display(Name = "Nombre Completo")]
        public string FullName { get; set; }
    }
}
