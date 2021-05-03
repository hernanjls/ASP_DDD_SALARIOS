using Microsoft.EntityFrameworkCore;
using Salarios.Dominio.Entidades;
using Salarios.Dominio.Interfaces;
using Salarios.Infraestructura.Datos;
using System.Collections.Generic;
using System.Linq;

namespace Salarios.Infraestructura.Repositorio
{
    public class SalarioRepository : Repository<Salario>, ISalarioRepository
    {

    }
}
