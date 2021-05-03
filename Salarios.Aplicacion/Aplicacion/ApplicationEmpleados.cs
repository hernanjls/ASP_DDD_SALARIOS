using System.Collections.Generic;
using Salarios.Aplicacion.Models;
using Salarios.Applicacion.Interfaces;
using Salarios.Dominio.Entidades;
using Salarios.Dominio.Interfaces;

namespace Salarios.Applicacion.Aplicacion
{
    public class ApplicationEmpleados : IApplicationEmpleados
    {
        private readonly IEmpleadoRepository _repositoryEmpleados;
        private readonly IDivisionRepository _repositoryDivison;

        public ApplicationEmpleados(IEmpleadoRepository repositoryEmpleados,
                                    IDivisionRepository repositoryDivision)
        {
            _repositoryEmpleados = repositoryEmpleados;
            _repositoryDivison = repositoryDivision;
        }

        public EmpleadoViewModel CargarRegistro(int codigoEmpleado)
        {
            var registro = _repositoryEmpleados.GetEntity(codigoEmpleado);

            EmpleadoViewModel cliente = new EmpleadoViewModel()
            {
                IdEmpleado = registro.EmployeeID,
                EmpleadoCodigo = registro.EmployeeCode,
                NumeroIdentificacion = registro.IdentificationNumber,
                Nombre = registro.EmployeeName,
                Apellidos = registro.EmployeeSurName,
                DivisionId = registro.DivisionId,
                PosicionId = registro.PositionId,
                OficinaId = registro.Office,
                Grado = registro.Grade,
                Birthday = registro.Birthday,
                BeginDate = registro.BeginDate,
                Genero = registro.Gender
            };

            return cliente;
        }

        public IEnumerable<Empleado> GetEmpleados()
        {
            return _repositoryEmpleados.GetEmpleados();
        }

        public void Registrar(EmpleadoViewModel reg)
        {
            Empleado item = new Empleado()
            {
                EmployeeCode = reg.EmpleadoCodigo,
                IdentificationNumber = reg.NumeroIdentificacion,
                EmployeeName = reg.Nombre,
                EmployeeSurName = reg.Apellidos,
                DivisionId = reg.DivisionId,
                PositionId = reg.PosicionId,
                Office = reg.OficinaId,
                Grade = reg.Grado,
                Birthday = reg.Birthday,
                BeginDate = reg.BeginDate,
                Gender = reg.Genero
            };

            if (reg.IdEmpleado != null)
            {
                item.EmployeeID = (int)reg.IdEmpleado;
                _repositoryEmpleados.Update(item);
            }
            else {
                _repositoryEmpleados.Add(item);
            }

            
        }

        public Dictionary<int, string> getOficinas()
        {
            var myDict = new Dictionary<int, string>
            {
                { 1, "C" },
                { 2, "D" },
                { 3, "A" },
                { 4, "ZZ" },
            };

            return myDict;
        }

        public Dictionary<int, string> getPosiciones()
        {
            var myDict = new Dictionary<int, string>
            {
                { 1, "CARGO MANAGER" },
                { 2, "HEAD OF CARGO" },
                { 3, "CARGO ASSISTANT" },
                { 4, "SALES MANAGER" },
                { 5, "ACCOUNT EXECUTIVE" },
                { 6, "MARKETING ASSISTANT" },
                { 7, "CUSTOMER DIRECTOR" },
                { 8, "CUSTOMER ASSISTANT" },
            };

            return myDict;
        }


        public Dictionary<string, string> getGeneros()
        {
            var myDict = new Dictionary<string, string>
            {
                { "hombre", "Hombre" },
                { "mujer", "Mujer" },
                { "otro", "Otro" }
            };

            return myDict;
        }

    }    
}
