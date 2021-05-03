using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Salarios.Aplicacion.Models;
using Salarios.Applicacion.Interfaces;
using Salarios.Dominio.Entidades;
using Salarios.Dominio.Interfaces;

namespace Salarios.Applicacion.Aplicacion
{
    public class ApplicationSalarios : IApplicationSalarios
    {
        private readonly ISalarioRepository _repository;

        public ApplicationSalarios(ISalarioRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Salario> GetAllSalarios()
        {
            return _repository.List();
        }

        public void Registrar(SalariosRegistroViewModel reg)
        {

            var salarios = JsonConvert.DeserializeObject<ICollection<Salario>>(reg.JsonSalarios);

            foreach (var sal in salarios)
            {
                var newSal = new Salario
                {
                    EmployeeId = sal.EmployeeId,
                    Year = sal.Year,
                    Month = sal.Month,
                    BaseSalary = sal.BaseSalary,
                    ProductionBonus = sal.ProductionBonus,
                    CompensationBonus = sal.CompensationBonus,
                    Commission = sal.Commission,
                    Contributions = sal.Contributions
                };
                _repository.Add(newSal);
            }

            

        }

        public string Validar(Salario reg)
        {
            if (reg.EmployeeId == -1 || reg.EmployeeId == 0)
            {
                return "Empleado es requerido";
            }

            if (reg.Year < 1 || reg.Month < 1)
            {
                return "Año y Mes son requeridos";
            }

            if (reg.BaseSalary == 0)
            {
                return "Salario Base es requerido";
            }

            var ct = _repository.List().Where(x => x.EmployeeId == reg.EmployeeId
                                        && x.Year == reg.Year
                                        && x.Month == reg.Month).Count();


            if (ct > 0)
            {
                return "Ya existe un salario para el mes seleccionado";
            }

            var totalSalary = getTotalSalary(reg.BaseSalary, reg.Commission,
                                             reg.ProductionBonus, reg.Contributions);

            return "Success:" + totalSalary;

        }


        public decimal getTotalSalary(decimal base_salary, decimal comission, 
                                      decimal production_bonus, decimal contribution)
        {

            //Other Income = (Base Salary + Commission) *8 % +Commission
            //Total Salary = Base Salary + Production Bonus + (Compensation Bonus * 75 %) +Other Income - Contributions

            var por1 = (base_salary + comission) * 8 / 100;
            var other_income = por1 + comission;

            var por2 = production_bonus * 75 / 100;
            var total = base_salary + production_bonus + por2 + other_income - contribution;

            return total;


        }

    }    
}
