using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Salarios.Aplicacion.Models;
using Salarios.Applicacion.Interfaces;
using Salarios.Dominio.Entidades;
using Salarios.Site.DTOs;
using Salarios.Site.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Salarios.Site.Controllers
{
    public class SalariosController : Controller
    {
        private readonly IApplicationEmpleados _applicationEmpleados;
        private readonly IApplicationSalarios _applicationSalarios;
        private readonly IApplicationDivision _applicationDivision;

        private List<SelectListItem> _empleadosItems;
        

        public SalariosController(IApplicationEmpleados applicationEmpleados,
                                   IApplicationSalarios applicationSalarios,
                                   IApplicationDivision applicationDivision)
        {
            _applicationEmpleados = applicationEmpleados;
            _applicationSalarios = applicationSalarios;
            _applicationDivision = applicationDivision;
        }

        public IActionResult Index()
        {
            var lista_salarios = _applicationSalarios.GetAllSalarios();
            var lista_empleados = _applicationEmpleados.GetEmpleados();

            var meses = getMeses();

            var query = from dr in lista_empleados
                        join dv in lista_salarios on dr.EmployeeID equals dv.EmployeeId
                        select new SalarioExt
                        {
                            SalarioId = dv.SalarioId,
                            EmployeeId = dr.EmployeeID,
                            EmployeeCode = dr.EmployeeCode,
                            FullNameEmployee = dr.EmployeeName + " " + dr.EmployeeSurName,
                            Year = dv.Year,
                            Month = dv.Month,
                            MonthDescription = meses[dv.Month],
                            TotalSalary = _applicationSalarios.getTotalSalary(dv.BaseSalary, dv.Commission, dv.ProductionBonus, dv.Contributions),
                        };


            return View(query);
        }

        [HttpGet]
        public IActionResult AverageSalary()
        {

            List<SalarioPromedio> lista = new List<SalarioPromedio>();

            var lista_salarios = _applicationSalarios.GetAllSalarios();
            var lista_empleados = _applicationEmpleados.GetEmpleados();

            foreach (var emp in lista_empleados)
            {
                var query = from dr in lista_salarios
                            where dr.EmployeeId == emp.EmployeeID
                            select new
                            {
                                EmployeeId = dr.EmployeeId,
                                TotalSalary = _applicationSalarios.getTotalSalary(dr.BaseSalary, dr.Commission, dr.ProductionBonus, dr.Contributions),
                                DateSalary = new DateTime(dr.Year, dr.Month, 1)
                            };

                DateTime? fecha_prev = null;
                decimal sum_sal = 0;
                int ct_sal = 0;
                foreach (var ds in query.OrderByDescending(x => x.DateSalary))
                {
                    if (fecha_prev != null)
                    {
                        if (ds.DateSalary.AddMonths(1) == fecha_prev && ct_sal < 3)
                        {
                            fecha_prev = ds.DateSalary;
                            sum_sal = sum_sal + ds.TotalSalary;
                            ct_sal++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        fecha_prev = ds.DateSalary;
                        sum_sal = sum_sal + ds.TotalSalary;
                        ct_sal++;
                    }
                }

                var item = new SalarioPromedio
                {
                    EmployeeId = emp.EmployeeID,
                    EmployeeCode = emp.EmployeeCode,
                    EmployeeFullName = emp.EmployeeName + ' ' + emp.EmployeeSurName,
                    AverageSalary = ct_sal > 0 ? sum_sal / ct_sal : 0

                };

                lista.Add(item);

            }

            return View(lista);


        }

        [HttpGet]
        public IActionResult Lista(string search_empleado, string search_filtro)
        {
           

            var lista_salarios = _applicationSalarios.GetAllSalarios();
            var lista_empleados = _applicationEmpleados.GetEmpleados();
            var lista_divisiones = _applicationDivision.GetDivisiones();
            var lista_oficinas = _applicationEmpleados.getOficinas();
            var lista_posiciones = _applicationEmpleados.getPosiciones();


            if (!string.IsNullOrEmpty(search_empleado) && search_empleado != "-1" &&
               !string.IsNullOrEmpty(search_filtro) && search_filtro != "-1")
            {

                var reg_emp = lista_empleados.Where(x => x.EmployeeID == int.Parse(search_empleado)).SingleOrDefault();


                switch (search_filtro)
                {
                    case "1": //Misma Oficina y Grado
                        lista_empleados = lista_empleados.Where(x => x.Office == reg_emp.Office && x.Grade == reg_emp.Grade && x.EmployeeID != reg_emp.EmployeeID);
                        break;
                    case "2": //Todas las Oficinas, mismo Grado
                        lista_empleados = lista_empleados.Where(x => x.Grade == reg_emp.Grade && x.EmployeeID != reg_emp.EmployeeID);
                        break;
                    case "3": //Misma Posición y Grado
                        lista_empleados = lista_empleados.Where(x => x.PositionId == reg_emp.PositionId && x.Grade == reg_emp.Grade && x.EmployeeID != reg_emp.EmployeeID);
                        break;
                    case "4": //Todos los Puestos y con el mismo Grado
                        lista_empleados = lista_empleados.Where(x => x.Grade == reg_emp.Grade && x.EmployeeID != reg_emp.EmployeeID);
                        break;

                }

            }


            var query = from ep in lista_empleados
                        join dv in lista_divisiones on ep.DivisionId equals dv.DivisionId
                        join of in lista_oficinas on ep.Office equals of.Key
                        join po in lista_posiciones on ep.PositionId equals po.Key
                        let years = DateTime.Now.Year - ((DateTime)ep.Birthday).Year
                        let birthdayThisYear = ((DateTime)ep.Birthday).AddYears(years)
                        let LastSalary = lista_salarios.Where(x=>x.EmployeeId == ep.EmployeeID)
                                        .OrderByDescending(x=>x.Year)
                                        .ThenByDescending(x=>x.Month).FirstOrDefault()
                        select new SalarioPromedio
                        {
                            EmployeeId = ep.EmployeeID,
                            EmployeeCode = ep.EmployeeCode,
                            EmployeeFullName = ep.EmployeeName + ' ' + ep.EmployeeSurName,
                            NumeroIdentificacion = ep.IdentificationNumber,
                            Division = dv.DivisionName,
                            Posicion = po.Value,
                            Oficina = of.Value,
                            Grado = ep.Grade.ToString(),
                            FechaCumple = ep.Birthday == null? "": ((DateTime)ep.Birthday).ToString("dd/MMMM"),
                            Edad = birthdayThisYear > DateTime.Now ? years - 1 : years,
                            FechaInicio = ((DateTime)ep.BeginDate).ToString("dd/MM/yyyy"),
                            UltimoSalario = LastSalary == null ? 0 :
                                        _applicationSalarios.getTotalSalary(LastSalary.BaseSalary,
                                                                            LastSalary.Commission,
                                                                            LastSalary.ProductionBonus,
                                                                            LastSalary.Contributions),

                        };


            


            var reg = new ConsultaSalarios
            {
                EmployeeId = search_empleado,
                Filtro = search_filtro,
                Lista = query.ToList()
            };

            _empleadosItems = new List<SelectListItem>();
            _empleadosItems.Add(new SelectListItem
            {
                Text = "-Seleccione un empleado-",
                Value = "-1"
            });
            foreach (var item in _applicationEmpleados.GetEmpleados())
            {
                _empleadosItems.Add(new SelectListItem
                {
                    Text = item.EmployeeName + " " + item.EmployeeSurName,
                    Value = item.EmployeeID.ToString()
                });
            }

            ViewBag.empleadosItems = _empleadosItems;


            return View(reg);


        }


        [HttpGet]
        public IActionResult Bonos(string search_codigo)
        {
            List<SalarioExt> lista = new List<SalarioExt>();

            decimal sum_sal = 0;
           

            if (!string.IsNullOrEmpty(search_codigo))
            {
                var lista_salarios = _applicationSalarios.GetAllSalarios();
                var lista_empleados = _applicationEmpleados.GetEmpleados();
                var meses = getMeses();

                var reg_empleado = lista_empleados.Where(x => x.EmployeeCode == search_codigo).SingleOrDefault();
                var nombre_empleado = reg_empleado.EmployeeName + " " + reg_empleado.EmployeeSurName;
                var query = lista_salarios.Where(x => x.EmployeeId == reg_empleado.EmployeeID)
                                        .OrderByDescending(x => x.Year)
                                        .ThenByDescending(x => x.Month).Take(3); // Toma los ultimos tres salarios del empleado


                


                DateTime? fecha_prev = null;
                
                foreach (var dr in query)
                {
                    DateTime DateSalary = new DateTime(dr.Year, dr.Month, 1);
                    var TotalSalary = _applicationSalarios.getTotalSalary(dr.BaseSalary, dr.Commission, dr.ProductionBonus, dr.Contributions);

                    if (fecha_prev != null)
                    {
                        if (DateSalary.AddMonths(1) == fecha_prev)
                        {
                            fecha_prev = DateSalary;
                            sum_sal = sum_sal + TotalSalary;

                            lista.Add(
                                new SalarioExt
                                {
                                    FullNameEmployee = nombre_empleado,
                                    Year = dr.Year,
                                    MonthDescription = meses[dr.Month],
                                    TotalSalary = TotalSalary
                                }
                            );
                           
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        fecha_prev = DateSalary;
                        sum_sal = sum_sal + TotalSalary;
                        
                        lista.Add(
                            new SalarioExt {
                                FullNameEmployee = nombre_empleado, 
                                Year = dr.Year, 
                                MonthDescription = meses[dr.Month], 
                                TotalSalary = TotalSalary 
                            }
                        );
                    }
                }


            }


            var reg = new ConsultaBono
            {
                EmployeeCode = search_codigo,
                Lista = lista,
                Bono = "$" + sum_sal/3
            };


            return View(reg);

        }



        [HttpGet]
        public IActionResult Registro(int? id)
        {
            SalariosRegistroViewModel viewModel = new SalariosRegistroViewModel();

            if (id != null)
            {
                //viewModel = _applicationEmpleados.CargarRegistro((int)id);
            }

            _empleadosItems = new List<SelectListItem>();
            _empleadosItems.Add(new SelectListItem
            {
                Text = "-Seleccione un empleado-",
                Value = "-1"
            });
            foreach (var item in _applicationEmpleados.GetEmpleados())
            {
                _empleadosItems.Add(new SelectListItem
                {
                    Text = item.EmployeeName + " " + item.EmployeeSurName,
                    Value = item.EmployeeID.ToString()
                });
            }

            ViewBag.empleadosItems = _empleadosItems;
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Registro(SalariosRegistroViewModel entidade)
        {
            if (ModelState.IsValid)
            {
                _applicationSalarios.Registrar(entidade);
            }
            else
            {

                _empleadosItems = new List<SelectListItem>();

                _empleadosItems.Add(new SelectListItem
                {
                    Text = "-Seleccione un empleado-",
                    Value = "-1"
                });

                foreach (var item in _applicationEmpleados.GetEmpleados())
                {
                    _empleadosItems.Add(new SelectListItem
                    {
                        Text = item.EmployeeName + " " + item.EmployeeSurName,
                        Value = item.EmployeeID.ToString()
                    });
                }

                ViewBag.empleadosItems = _empleadosItems;

                return View(entidade);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult Validar(Salario sal)
        {


            var res = _applicationSalarios.Validar(sal);

            var ret = new
            {
                mensaje = res,
            };

            return Json(ret);

        }



        public Dictionary<int, string> getMeses()
        {
            var myDict = new Dictionary<int, string>
            {
                { 1, "Enero" },
                { 2, "Febrero" },
                { 3, "Marzo" },
                { 4, "Abril" },
                { 5, "Mayo" },
                { 6, "Junio" },
                { 7, "Julio" },
                { 8, "Agosto" },
                { 9, "Septiembre" },
                { 10, "Octubre" },
                { 11, "Noviembre" },
                { 12, "Diciembre" }
            };

            return myDict;
        }


    }


}


