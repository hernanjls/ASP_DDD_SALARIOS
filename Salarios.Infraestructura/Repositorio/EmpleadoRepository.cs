using Microsoft.EntityFrameworkCore;
using Salarios.Dominio.Entidades;
using Salarios.Dominio.Interfaces;
using Salarios.Infraestructura.Datos;
using System.Collections.Generic;
using System.Linq;

namespace Salarios.Infraestructura.Repositorio
{
    public class EmpleadoRepository : Repository<Empleado>, IEmpleadoRepository
    {

        public IEnumerable<Empleado> GetEmpleados()
        {

            using (var banco = new ContextBase(this._OptionsBuider.Options))
            {
                //var listaClientes = banco.TblEmployee.Where(x => x.EmployeeID > 0).ToList();
                var listaClientes = banco.TblEmployee.ToList();

                return listaClientes;

            }

            
        }
    }
}
