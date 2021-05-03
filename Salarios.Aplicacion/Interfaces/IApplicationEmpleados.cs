using Salarios.Aplicacion.Models;
using Salarios.Dominio.Entidades;
using System.Collections.Generic;

namespace Salarios.Applicacion.Interfaces
{
    public interface IApplicationEmpleados 
    {

        IEnumerable<Empleado> GetEmpleados();

        EmpleadoViewModel CargarRegistro(int codigoEmpleado);

        void Registrar(EmpleadoViewModel empleado);

        Dictionary<int, string> getOficinas();
        Dictionary<int, string> getPosiciones();

        Dictionary<string, string> getGeneros();

    }
}
