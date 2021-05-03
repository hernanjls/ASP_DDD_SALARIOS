using Salarios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Salarios.Site.DTOs
{
    public class SalarioPromedio 
    {
        [Display(Name = "Id")]
        public int EmployeeId { get; set; }

        [Display(Name = "Codigo")]
        public string EmployeeCode { get; set; }
        [Display(Name = "Empleado")]
        public string EmployeeFullName { get; set; }

        public string Division { get; set; }
        public string Posicion { get; set; }
        public string Oficina { get; set; }
        public string Grado { get; set; }
        public string FechaInicio { get; set; }

        [Display(Name = "Cumpleaños")]
        public string FechaCumple { get; set; }
        public int Edad { get; set; }
        public string NumeroIdentificacion { get; set; }
        [Display(Name = "Ultimo Salario")]
        public decimal UltimoSalario { get; set; }

        [Display(Name = "Salario Promedio")]
        public decimal AverageSalary { get; set; }


    }

    public class ConsultaSalarios
    {
        [Display(Name = "Empleado")]
        public string EmployeeId { get; set; }

        [Display(Name = "Filtro")]
        public string Filtro { get; set; }
        public List<SalarioPromedio> Lista {get; set;} // lista para mostrar la lista de empleados y sus salarios
    }

}
