using Salarios.Aplicacion.Models;
using Salarios.Dominio.Entidades;
using System.Collections.Generic;

namespace Salarios.Applicacion.Interfaces
{
    public interface IApplicationSalarios
    {
        IEnumerable<Salario> GetAllSalarios();

        void Registrar(SalariosRegistroViewModel reg);

        string Validar(Salario reg);

        decimal getTotalSalary(decimal base_salary, decimal comission, decimal production_bonus, decimal contribution);

    }
}
