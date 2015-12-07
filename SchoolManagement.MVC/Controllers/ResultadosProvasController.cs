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
        private readonly IAlunoServico _alunoServico;
        public ResultadosProvasController(IResultadosProvasServico resultadosProvasApp, Utilizavel util, ITurmaServico turmaServico,  IAlunoServico alunoServico)
        {
            _resultadosProvasApp = resultadosProvasApp;
            _util = util;
            _turmaServico = turmaServico;
            _alunoServico = alunoServico;
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

        [HttpGet]
        public ActionResult FiltroTurmasProfessorLeciona()
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

            ViewBag.ListaTurmas = ListaTurmas;

            return View("FiltroTurmasProfessorLecionaLancarNotas");
        }

        [HttpPost]
        public ActionResult VisualizarAlunosTurmasProfessorLecionaLancarNota()
        {
            List<Aluno> AlunosBackEnd = new List<Aluno>();

            int professorId = (int)Session["UsuarioId"];
            //VisualizarTurmasProfessor
            var turmas = _turmaServico.RecuperarTurmasQueProfessorLeciona(professorId);

            foreach (var turma in turmas)
            {
                var alunos = _alunoServico.RecuperarAlunosTurma(turma.TurmaId).ToList();
                foreach (var aluno in alunos)
                {
                    AlunosBackEnd.Add(aluno);
                }
            }

            var alunosMapped = Mapper.Map<IEnumerable<Aluno>, IEnumerable<AlunoViewModel>>(AlunosBackEnd);
            return View("VisualizarAlunosTurmasProfessorLecionaLancarNotas", alunosMapped);
        }
	}
}