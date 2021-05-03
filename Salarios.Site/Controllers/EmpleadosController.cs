using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Salarios.Aplicacion.Models;
using Salarios.Applicacion.Interfaces;
using Salarios.Site.DTOs;
using Salarios.Site.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Salarios.Site.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly IApplicationEmpleados _applicationEmpleados;
        private readonly IApplicationDivision _applicationDivision;

        private List<SelectListItem> _divisionItems;
        private List<SelectListItem> _oficinaItems;
        private List<SelectListItem> _posicionItems;
        private List<SelectListItem> _generoItems;

        public EmpleadosController(IApplicationEmpleados applicationEmpleados,
                                   IApplicationDivision applicationDivision)
        {
            _applicationEmpleados = applicationEmpleados;
            _applicationDivision = applicationDivision;
        }

        public IActionResult Index()
        {
            var lista_empleados = _applicationEmpleados.GetEmpleados();
            var lista_divisiones = _applicationDivision.GetDivisiones();


            var query = from dr in lista_empleados
                        join dv in lista_divisiones on dr.DivisionId equals dv.DivisionId
                        select new EmpleadoExt
                        {
                            EmployeeID = dr.EmployeeID,
                            EmployeeName = dr.EmployeeName,
                            Birthday = dr.Birthday,
                            BeginDate = dr.BeginDate,
                            EmployeeSurName = dr.EmployeeSurName,
                            FullName = dr.EmployeeName + " " + dr.EmployeeSurName,
                            DivisionId = dr.DivisionId,
                            DivisionName = dv.DivisionName,
                            EmployeeCode = dr.EmployeeCode,
                            Gender = dr.Gender
                        };


            return View(query);
        }

        [HttpGet]
        public IActionResult Registro(int? id)
        {
            EmpleadoViewModel viewModel = new EmpleadoViewModel();

            if (id != null)
            {
                viewModel = _applicationEmpleados.CargarRegistro((int)id);
            }

            _divisionItems = new List<SelectListItem>();
            foreach (var item in _applicationDivision.GetDivisiones())
            {
                _divisionItems.Add(new SelectListItem
                {
                    Text = item.DivisionName,
                    Value = item.DivisionId.ToString()
                });
            }

            _posicionItems = new List<SelectListItem>();
            foreach (var item in _applicationEmpleados.getPosiciones())
            {
                _posicionItems.Add(new SelectListItem
                {
                    Text = item.Value,
                    Value = item.Key.ToString()
                });
            }


            _oficinaItems = new List<SelectListItem>();
            foreach (var item in _applicationEmpleados.getOficinas())
            {
                _oficinaItems.Add(new SelectListItem
                {
                    Text = item.Value,
                    Value = item.Key.ToString()
                });
            }

            

            _generoItems = new List<SelectListItem>();
            foreach (var item in _applicationEmpleados.getGeneros())
            {
                _generoItems.Add(new SelectListItem
                {
                    Text = item.Value,
                    Value = item.Key.ToString()
                });
            }

            ViewBag.divisionItems = _divisionItems;
            ViewBag.posicionItems = _posicionItems;
            ViewBag.oficinasItems = _oficinaItems;
            ViewBag.generoItems = _generoItems;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Registro(EmpleadoViewModel entidade)
        {
            if (ModelState.IsValid)
            {
                _applicationEmpleados.Registrar(entidade);
            }
            else
            {

                _divisionItems = new List<SelectListItem>();
                foreach (var item in _applicationDivision.GetDivisiones())
                {
                    _divisionItems.Add(new SelectListItem
                    {
                        Text = item.DivisionName,
                        Value = item.DivisionId.ToString()
                    });
                }

                _posicionItems = new List<SelectListItem>();
                foreach (var item in _applicationEmpleados.getPosiciones())
                {
                    _posicionItems.Add(new SelectListItem
                    {
                        Text = item.Value,
                        Value = item.Key.ToString()
                    });
                }

                _oficinaItems = new List<SelectListItem>();
                foreach (var item in _applicationEmpleados.getOficinas())
                {
                    _oficinaItems.Add(new SelectListItem
                    {
                        Text = item.Value,
                        Value = item.Key.ToString()
                    });
                }

               

                _generoItems = new List<SelectListItem>();
                foreach (var item in _applicationEmpleados.getGeneros())
                {
                    _generoItems.Add(new SelectListItem
                    {
                        Text = item.Value,
                        Value = item.Key.ToString()
                    });
                }

                ViewBag.divisionItems = _divisionItems;
                ViewBag.posicionItems = _posicionItems;
                ViewBag.oficinasItems = _oficinaItems;
                ViewBag.generoItems = _generoItems;

                return View(entidade);
            }

            return RedirectToAction("Index");
        }


    }
}
