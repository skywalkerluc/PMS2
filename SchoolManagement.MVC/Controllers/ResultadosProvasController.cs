using AutoMapper;
using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Servicos;
using SchoolManagement.MVC.Utilitarios;
using SchoolManagement.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.Controllers
{
    public class ResultadosProvasController : Controller
    {
        private readonly IResultadosProvasServico _resultadosProvasApp;
        private readonly Utilizavel _util;
        private readonly ITurmaServico _turmaServico;

        public ResultadosProvasController(IResultadosProvasServico resultadosProvasApp, Utilizavel util, ITurmaServico turmaServico)
        {
            _resultadosProvasApp = resultadosProvasApp;
            _util = util;
            _turmaServico = turmaServico;
        }

        //
        // GET: /ResultadosProvas/
        public ActionResult RecuperarTodosResultadosProvas()
        {
            var resultados = _resultadosProvasApp.RecuperarTodos();
            var resultadosMapped = Mapper.Map<IEnumerable<ResultadosProvas>, IEnumerable<ResultadosProvasViewModel>>(resultados);

            return View("RecuperarTodosResultadosProvas", resultadosMapped);
        }

        [HttpGet]
        public ActionResult LancarNotasTurma()
        {
            throw new NotImplementedException();   
        }

        [HttpPost]
        public ActionResult LancarNotasTurma(int turmaId)
        {
            throw new NotImplementedException();
        }
	}
}