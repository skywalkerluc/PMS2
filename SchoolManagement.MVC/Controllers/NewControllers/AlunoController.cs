using AutoMapper;
using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Servicos;
using SchoolManagement.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.Controllers.NewControllers
{
    public class AlunoController : Controller
    {
        IAlunoServico _alunoApp;
        IDisciplinaServico _disciplinaApp;
        ITurmaServico _turmaApp;
        IProvaServico _provaApp;

        public AlunoController(IAlunoServico alunoApp, IDisciplinaServico disciplinaApp, ITurmaServico turmaApp, IProvaServico provaApp)
        {
            _alunoApp = alunoApp;
            _disciplinaApp = disciplinaApp;
            _turmaApp = turmaApp;
            _provaApp = provaApp;
        }

        
        public ActionResult VerDisciplinasMinhaTurma()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());
                var aluno = _alunoApp.Recuperar(idUsuario);
                var alunosTurma = _disciplinaApp.RecuperarDisciplinasTurma(aluno.Turma.TurmaId);
                var disciplinasMapeados = Mapper.Map<IEnumerable<Disciplina>, IEnumerable<DisciplinaViewModel>>(alunosTurma);
                return View("VisualizarDisciplinasMinhaTurma", disciplinasMapeados);
            }
            catch (Exception ex)
            {
                return View("VisualizarDisciplinasMinhaTurma", ex.Message.ToString());
            }
            
        }

        public ActionResult VerMinhaTurma()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());
                var aluno = _alunoApp.Recuperar(idUsuario);
                var alunoTurma = _turmaApp.Recuperar(aluno.Turma.TurmaId);
                var turmaViewModel = Mapper.Map<Turma, TurmaViewModel>(alunoTurma);
                return View("VisualizaMinhaTurma", turmaViewModel);
            }
            catch (Exception ex)
            {
                return View("VisualizaMinhaTurma", ex.Message.ToString());
            }
        }

        public ActionResult VisualizarProvasTurma()
        {
            int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());
            var aluno = _alunoApp.Recuperar(idUsuario);
            var prova = _provaApp.RecuperarProvasTurma(aluno.Turma.TurmaId);
            var provaViewModel = Mapper.Map<IEnumerable<Prova>, IEnumerable<ProvaViewModel>>(prova);
            return View("VisualizarProvasTurma", provaViewModel);
        }

        public ActionResult VisualizarNotasDoAlunoResponsavelSelecionado(ResponsavelViewModel responsavel)
        {
            var aluno1 = responsavel.alunoSelecionado;
            var aluno2 = _alunoApp.Recuperar(aluno1);
            var provaAluno = _alunoApp.RecuperarResultadosAluno(aluno2);
            var provaMapeados = Mapper.Map<IEnumerable<ResultadosProvas>, IEnumerable<ResultadosProvasViewModel>>(provaAluno);
            return View("ResultadoDeNotasDeProvaResponsavelAluno", provaMapeados);
        }
    }
}