using Salarios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Salarios.Aplicacion.Models
{
    public class SalariosRegistroViewModel
    {
        [Required(ErrorMessage = "Codigo de Empleado es requerido!")]
        public int CodigoEmpleado { get; set; }

        public string JsonSalarios { get; set; }
        

    }
}
