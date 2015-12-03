﻿using AutoMapper;
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
    public class FrequenciaController : Controller
    {
        private readonly IFrequenciaServico _frequenciaServico;
        private readonly IAlunoServico _alunoServico;
        private readonly IDisciplinaServico _disciplinaServico;
        private readonly IProfessorServico _professorServico;
        private readonly ITurmaServico _turmaServico;
        private readonly Utilizavel _util;

        public FrequenciaController(IFrequenciaServico frequenciaServico, IAlunoServico alunoServico, IDisciplinaServico disciplinaServico, IProfessorServico professorServico, ITurmaServico turmaServico, Utilizavel util)
        {
            _frequenciaServico = frequenciaServico;
            _alunoServico = alunoServico;
            _disciplinaServico = disciplinaServico;
            _professorServico = professorServico;
            _turmaServico = turmaServico;
            _util = util;
        }

        //
        // GET: /Frequencia/
        public ActionResult MostrarHistoricoFrequenciaTodosAlunos()
        {
            var frequencias = _frequenciaServico.RecuperarTodos();
            var frequenciasMapped = Mapper.Map<IEnumerable<Frequencia>, IEnumerable<FrequenciaViewModel>>(frequencias);
            return View("VisualizarFrequenciasTodosAlunos", frequenciasMapped);
        }

        public ActionResult MostrarHistorioFrequenciaAluno(int AlunoId)
        {
            var frequencias = _frequenciaServico.RecuperarHistoricoFrequenciasAluno(AlunoId);
            var frequenciasMapped = Mapper.Map<IEnumerable<Frequencia>, IEnumerable<FrequenciaViewModel>>(frequencias);
            return View("VisualizarFrequenciasAluno", frequenciasMapped);
        }

        public ActionResult IncluirDadosFrequenciaAluno()
        {
            List<DisciplinaViewModel> ListDisciplina = new List<DisciplinaViewModel>();
            List<TurmaViewModel> ListTurmas = new List<TurmaViewModel>();
            List<AlunoViewModel> ListAlunos = new List<AlunoViewModel>();

            List<SelectListItem> ListaAlunos = new List<SelectListItem>(); 
            List<SelectListItem> ListaDisciplinas = new List<SelectListItem>();
            int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());
            var turmas = _turmaServico.RecuperarTodos();
            var turmasMapped = Mapper.Map<IEnumerable<Turma>, IEnumerable<TurmaViewModel>>(turmas);

            foreach (var turma in turmasMapped)
            {
                foreach (var prof in turma.Professores)
                {
                    if (prof.Id == idUsuario)
                    {
                        ListTurmas.Add(turma);
                    }
                }
            }

            foreach (var item in ListTurmas)
            {
                var disciplinas = _disciplinaServico.RecuperarDisciplinasTurma(item.TurmaId);
                var discMapped = Mapper.Map<IEnumerable<Disciplina>, IEnumerable<DisciplinaViewModel>>(disciplinas);

                var alunos = _alunoServico.RecuperarAlunosTurma(item.TurmaId);
                var alunosMapped = Mapper.Map<IEnumerable<Aluno>, IEnumerable<AlunoViewModel>>(alunos);

                foreach (var aluno in alunosMapped)
                {
                    ListAlunos.Add(aluno);
                }
                
                foreach (var disc in discMapped)
                {
                    ListDisciplina.Add(disc);
                }
            }

            foreach (var alun in ListAlunos)
            {
                var selectListItem = new SelectListItem()
                {
                    Value = alun.Id.ToString(),
                    Text = alun.Nome.ToString()
                };
                ListaAlunos.Add(selectListItem);
            }

            foreach (var item in ListDisciplina)
            {
                var selectListItem = new SelectListItem()
                {
                    Value = item.DisciplinaId.ToString(),
                    Text = item.NomeDisciplina.ToString()
                };
                ListaDisciplinas.Add(selectListItem);
            }

            ViewBag.ListaAlunos = ListaAlunos;
            ViewBag.ListaDisciplinas = ListaDisciplinas;

            var frequencia = new FrequenciaViewModel();
            return View("IncluirDadosFrequenciaAluno", frequencia);
        }

        [HttpPost]
        public ActionResult IncluirDadosFrequenciaAluno(FrequenciaViewModel frequencia, int AlunoId, int DisciplinaId)
        {
            var aluno = _alunoServico.Recuperar(AlunoId);
            var disciplina = _disciplinaServico.Recuperar(DisciplinaId);

            var frequenciaMapped = Mapper.Map<FrequenciaViewModel, Frequencia>(frequencia);
            frequenciaMapped.Aluno = aluno;
            frequenciaMapped.Disciplina = disciplina;

            var attempt = _frequenciaServico.IncluirFrequenciaAluno(frequenciaMapped);

            if (attempt != null)
            {
                return View("Index", "Home");
            }
            else
            {
                throw new NotImplementedException("Erro ao incluir dados de frequência de um aluno.");
            }
            
        }
	}
}