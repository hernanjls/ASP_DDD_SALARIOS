using Salarios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Salarios.Site.DTOs
{
    public class SalarioExt : Salario
    {
        
        [Display(Name = "Mes")]
        public string MonthDescription { get; set; }

        [Display(Name = "Empleado")]
        public string FullNameEmployee { get; set; }
        
        [Display(Name = "Codigo")]

        public string EmployeeCode { get; set; }

        [Display(Name = "Salario Total")]
        public decimal TotalSalary { get; set; }
    }


    public class ConsultaBono
    {
        [Display(Name = "Empleado")]
        public string EmployeeCode { get; set; }

        [Display(Name = "Bono del Empleado")]
        public string Bono { get; set; }

        public List<SalarioExt> Lista { get; set; } // lista para mostrar la lista de empleados y sus salarios
    }

}
