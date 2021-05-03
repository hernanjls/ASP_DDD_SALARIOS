using System.Collections.Generic;
using Salarios.Applicacion.Interfaces;
using Salarios.Dominio.Entidades;
using Salarios.Dominio.Interfaces;

namespace Salarios.Applicacion.Aplicacion
{
    public class ApplicationDivision : IApplicationDivision
    {
        private readonly IDivisionRepository _repository;

        public ApplicationDivision(IDivisionRepository repository)
        {
            _repository = repository;
        }

       

        IEnumerable<Division> IApplicationDivision.GetDivisiones()
        {
            return _repository.List();
        }
    }    
}
