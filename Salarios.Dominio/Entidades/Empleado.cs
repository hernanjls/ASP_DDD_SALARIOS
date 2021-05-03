using System;
using System.ComponentModel.DataAnnotations;

namespace Salarios.Dominio.Entidades
{
    public class Empleado 
    {

        [Key]
        [Display(Name = "Id")]
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "Nombre es Obligatorio")]

        [Display(Name = "Nombre")]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "Apellidos es Obligatorio")]
        [Display(Name = "Apellidos")]
        public string EmployeeSurName { get; set; }
        public int DivisionId { get; set; }
        public int PositionId { get; set; }
        public int Office { get; set; }
        public int Grade { get; set; }

        [Display(Name = "Codigo")]
        public string EmployeeCode { get; set; }
        public string IdentificationNumber { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Fecha de Nacimiento es requerido!")]
        public  DateTime Birthday { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Fecha de Inicio es requerido!")]
        public DateTime BeginDate { get; set; }
        public string Gender { get; set; }


    }
}
