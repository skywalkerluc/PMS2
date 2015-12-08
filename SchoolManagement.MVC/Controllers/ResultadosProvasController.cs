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
        private readonly IProvaServico _provaApp;

        public ResultadosProvasController(IResultadosProvasServico resultadosProvasApp, Utilizavel util, ITurmaServico turmaServico, IAlunoServico alunoServico, IProvaServico provaApp)
        {
            _resultadosProvasApp = resultadosProvasApp;
            _util = util;
            _turmaServico = turmaServico;
            _alunoServico = alunoServico;
            _provaApp = provaApp;
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

        [HttpGet]
        public ActionResult FiltroTurmasProvaProfessorLeciona(TurmaViewModel turma)
        {
            int professorId = (int)Session["UsuarioId"];
            int turmaIdEscolhida = turma.TurmaId;

            List<SelectListItem> ListaProvasResultadoNota = new List<SelectListItem>();
            var listaProvas = _provaApp.RecuperarProvasPendentesTurmaProfessor(professorId, turmaIdEscolhida);
            foreach (var item in listaProvas)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = Convert.ToString(item.ProvaId),
                    Text = item.Disciplina.NomeDisciplina
                };
                ListaProvasResultadoNota.Add(select);
            }

            ViewBag.ListaProvasResultadoNota = ListaProvasResultadoNota;

            return View("FiltroProvasProfessorTurma");
        }

        [HttpPost]
        public ActionResult VisualizarAlunosTurmasProfessorLecionaLancarNota(ProvaViewModel provaid2)
        {

            Session["provaIdselecionado"] = provaid2.provaIdSelecionado;

            List<ResultadosProvas> AlunosBackEnd = new List<ResultadosProvas>();

            int professorId = (int)Session["UsuarioId"];
            //VisualizarTurmasProfessor
            var turmas = _turmaServico.RecuperarTurmasQueProfessorLeciona(professorId);

            foreach (var turma in turmas)
            {
                var alunos = _alunoServico.RecuperarAlunosTurma(turma.TurmaId).ToList();
                foreach (var aluno in alunos)
                {
                    ResultadosProvas rp = new ResultadosProvas();
                    rp.Aluno = aluno;
                    AlunosBackEnd.Add(rp);
                }
            } 
            
            Utilizavel util = new Utilizavel();
            ViewBag.ListaNotas = util.PreencherListasNotas();

            var alunosMapped = Mapper.Map<IEnumerable<ResultadosProvas>, IEnumerable<ResultadosProvasViewModel>>(AlunosBackEnd);
            return View("VisualizarAlunosTurmasProfessorLecionaLancarNotas", alunosMapped);
        }
    }
}