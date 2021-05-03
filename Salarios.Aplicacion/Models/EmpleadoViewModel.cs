using Salarios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Salarios.Aplicacion.Models
{
    public class EmpleadoViewModel
    {
        public int? IdEmpleado { get; set; }

        [Required(ErrorMessage = "Codigo es requerido!")]
        public string EmpleadoCodigo { get; set; }
        [Required(ErrorMessage = "Número de Identificación es requerido!")]
        public string NumeroIdentificacion { get; set; }

        [Required(ErrorMessage = "Nombre es requerido!")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Apellidos es requerido!")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Division es requerido!")]
        public int DivisionId { get; set; }

        [Required(ErrorMessage = "Oficina es requerido!")]
        public int OficinaId { get; set; }
        [Required(ErrorMessage = "Posicion es requerido!")]
        public int PosicionId { get; set; }

        [Required(ErrorMessage = "Grado es requerido!")]
        public int Grado { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Fecha de Nacimiento es requerido!")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Fecha de Inicio es requerido!")]

        [DataType(DataType.Date)]
        public DateTime BeginDate { get; set; }
        public string Genero { get; set; }

    }
}
