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
            int professorId = (int)Session["UsuarioId"];
            List<SelectListItem> ListaTurmas = new List<SelectListItem>();
            var listaTurmas = _turmaServico.RecuperarTurmasQueProfessorLeciona(professorId);
            foreach (var item in listaTurmas)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = item.TurmaId.ToString(),
                    Text = String.Concat(item.Descricao, " (", this.RecuperarValorHorarioTurma(item.HorariosTurmaId), ")")
                };
                ListaTurmas.Add(select);
            }

            return View("LancarNotasSelecaoTurma", ListaTurmas);
        }

        [HttpPost]
        public ActionResult LancarNotasTurma(List<ResultadosProvasViewModel> resultados)
        {
            try
            {
                foreach (var item in resultados)
                {
                    var resultMapped = Mapper.Map<ResultadosProvasViewModel, ResultadosProvas>(item);
                    var atmpt = _resultadosProvasApp.IncluirNotaAluno(resultMapped);
                }
                return View("Home", "Index");
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public ActionResult RecuperarNotasAluno(int alunoId)
        {
            var attmpt = _resultadosProvasApp.RecuperarNotasAluno(alunoId);
            var attmptMapped = Mapper.Map<IEnumerable<ResultadosProvas>, IEnumerable<ResultadosProvasViewModel>>(attmpt);
            return View("VisualizarNotasAluno", attmptMapped);
        }

        private string RecuperarValorHorarioTurma(int value)
        {
            string descricaoRetorno = string.Empty;
            switch (value)
            {
                case 1:
                    descricaoRetorno = "Manhã";
                    break;
                case 2:
                    descricaoRetorno = "Tarde";
                    break;
                case 3:
                    descricaoRetorno = "Noite";
                    break;
                default:
                    descricaoRetorno = string.Empty;
                    break;
            }
            return descricaoRetorno;
        }
	}
}