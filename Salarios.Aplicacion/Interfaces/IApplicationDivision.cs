using Salarios.Dominio.Entidades;
using System.Collections.Generic;

namespace Salarios.Applicacion.Interfaces
{
    public interface IApplicationDivision 
    {
        IEnumerable<Division> GetDivisiones();
    }
}
