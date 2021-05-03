using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Salarios.Dominio.Entidades;

namespace Salarios.Dominio.Interfaces
{
    public interface IEmpleadoRepository : IRepository<Empleado>
    {

        IEnumerable<Empleado> GetEmpleados();

    }
}
