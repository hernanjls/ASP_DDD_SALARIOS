using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Salarios.Dominio.Entidades
{
    public class Salario
    {
        [Key]
        public int SalarioId { get; set; }
        public int EmployeeId { get; set; }
        [Display(Name = "Año")]
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal ProductionBonus { get; set; }
        public decimal CompensationBonus { get; set; }
        public decimal Commission { get; set; }
        public decimal Contributions { get; set; }

        

    }
}
