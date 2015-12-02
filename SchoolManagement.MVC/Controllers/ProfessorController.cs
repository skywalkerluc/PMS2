<<<<<<< HEAD
﻿using AutoMapper;
using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Servicos;
using SchoolManagement.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly IProfessorServico _professorApp;
        private readonly IUsuarioServico _usuarioApp;
        private readonly IDisciplinaServico _disciplinaApp;
        private readonly ITurmaServico _turmaServico;

        public ProfessorController(IProfessorServico professorApp, IUsuarioServico usuarioApp, IDisciplinaServico disciplinaApp, ITurmaServico turmaServico)
        {
            _professorApp = professorApp;
            _usuarioApp = usuarioApp;
            _disciplinaApp = disciplinaApp;
            _turmaServico = turmaServico;
        }

        // GET: Professor
        public ActionResult Index()
        {

            try
            {
                var enumeradorProfessores = _professorApp.RecuperarTodos();
                var professorViewModel = Mapper.Map<IEnumerable<Professor>, IEnumerable<ProfessorViewModel>>(enumeradorProfessores);
                return View("Index", professorViewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Create");
            }

        }

        // GET: Professor/Details/5
        public ActionResult Details(int id)
        {
            var professor = _professorApp.Recuperar(id);
            var professorViewModel = Mapper.Map<Professor, ProfessorViewModel>(professor);

            return View("ExibirInformacoes", professorViewModel);
        }

        // GET: Professor/Create
        public ActionResult Create()
        {
            var professor = new ProfessorViewModel();
            return View("AdicionarProfessor");
        }

        // POST: Professor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProfessorViewModel professor)
        {
            try
            {
                var matricula = "3" + "-" + DateTime.Now.Year + "" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                professor.Matricula = matricula;

                List<DisciplinaViewModel> ListaDisciplinas = new List<DisciplinaViewModel>();
                if(professor.disciplinasTeste.Count > 0)
                {
                    for (int i = 0; i <= professor.disciplinasTeste.Count - 1; i++)
                    {
                        var disciplina = _disciplinaApp.Recuperar(professor.disciplinasTeste[i]);
                        var disciplinaViewModel = Mapper.Map<Disciplina, DisciplinaViewModel>(disciplina);

                        ListaDisciplinas.Add(disciplinaViewModel);
                        //professor.Disciplinas.Add(disciplinaViewModel);
                    }
                }

                professor.Disciplinas = ListaDisciplinas;
                professor.Funcao = "1";

                var professorDomain = Mapper.Map<ProfessorViewModel, Professor>(professor);
                _professorApp.IncluirProfessor(professorDomain);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                var mensagemErro = ex.Message.ToString();
                return RedirectToAction("Index", "Home", mensagemErro);
            }
        }


        // GET: Professor/Edit/5
        public ActionResult Edit(int id)
        {
            var professor = _professorApp.Recuperar(id);
            var professorViewModel = Mapper.Map<Professor, ProfessorViewModel>(professor);

            return View("EditarProfessor", professorViewModel);
        }

        // POST: Professor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfessorViewModel professor)
        {
            if (ModelState.IsValid)
            {
                var professorDomain = Mapper.Map<ProfessorViewModel, Professor>(professor);
                _professorApp.Atualizar(professorDomain);

                return RedirectToAction("Index", "Home");
            }
            return View("Index", "Home", professor);
        }

        // GET: Professor/Delete/5
        public ActionResult Delete(int id)
        {
            var professor = _professorApp.Recuperar(id);
            var professorViewModel = Mapper.Map<Professor, ProfessorViewModel>(professor);

            return View("ExcluirProfessor", professorViewModel);
        }

        // POST: Professor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var professor = _professorApp.Recuperar(id);
            _professorApp.Remover(professor);

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Ver Turmas que eu leciono
        /// </summary>
        /// <param name="ProfessorId"></param>
        /// <returns></returns>
        public ActionResult VisualizarTurmasProfessor(int ProfessorId)
        {
            var turmas = _turmaServico.RecuperarTodos();
            var turmasMapped = Mapper.Map<IEnumerable<Turma>, IEnumerable<TurmaViewModel>>(turmas);
            List<TurmaViewModel> ListaTurmas = new List<TurmaViewModel>(); 

            foreach (var turma in turmasMapped)
            {
                foreach (var professor in turma.Professores)
                {
                    if (professor.Id == ProfessorId)
                    {
                        ListaTurmas.Add(turma);
                    }
                }
            }

            IEnumerable<TurmaViewModel> TurmasRetorno = ListaTurmas;
            return View("VisualizarTurmasLeciono", TurmasRetorno);
        }


        [HttpGet]
        public ActionResult ExibirNotificacoes(ProfessorViewModel professor)
        {
            try
            {
                var enumNotificacoes = _usuarioApp.ExibirNotificacoesUsuario(professor.indicadorAcesso, professor.Id);
                var notificacoesViewModel = Mapper.Map<IEnumerable<Notificacao>, IEnumerable<NotificacaoViewModel>>(enumNotificacoes);
                return View("ExibirNotificacoes", notificacoesViewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("DashboardSecundaria");
            }
        }

        public ActionResult VisualizarTodosProfessores()
        {
            var prof = _professorApp.RecuperarTodos();
            var profMapped = Mapper.Map<IEnumerable<Professor>, IEnumerable<ProfessorViewModel>>(prof);

            return View("VisualizarTodosProfessores");
        }

    }
}
=======
﻿using AutoMapper;
using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Servicos;
using SchoolManagement.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly IProfessorServico _professorApp;
        private readonly IUsuarioServico _usuarioApp;
        private readonly IDisciplinaServico _disciplinaApp;
        private readonly ITurmaServico _turmaServico;

        public ProfessorController(IProfessorServico professorApp, IUsuarioServico usuarioApp, IDisciplinaServico disciplinaApp, ITurmaServico turmaServico)
        {
            _professorApp = professorApp;
            _usuarioApp = usuarioApp;
            _disciplinaApp = disciplinaApp;
            _turmaServico = turmaServico;
        }

        // GET: Professor
        public ActionResult Index()
        {

            try
            {
                var enumeradorProfessores = _professorApp.RecuperarTodos();
                var professorViewModel = Mapper.Map<IEnumerable<Professor>, IEnumerable<ProfessorViewModel>>(enumeradorProfessores);
                return View("Index", professorViewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Create");
            }

        }

        // GET: Professor/Details/5
        public ActionResult Details(int id)
        {
            var professor = _professorApp.Recuperar(id);
            var professorViewModel = Mapper.Map<Professor, ProfessorViewModel>(professor);

            return View("ExibirInformacoes", professorViewModel);
        }

        // GET: Professor/Create
        public ActionResult Create()
        {
            var professor = new ProfessorViewModel();
            PreencherListaDisciplina(professor);
            return View("AdicionarProfessor");
        }

        // POST: Professor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProfessorViewModel professor)
        {
            //if (ModelState.IsValid)
            //{
            try
            {
                List<DisciplinaViewModel> ListaDisciplinas = new List<DisciplinaViewModel>();
                if(professor.disciplinasTeste.Count > 0)
                {
                    for (int i = 0; i <= professor.disciplinasTeste.Count - 1; i++)
                    {
                        var disciplina = _disciplinaApp.Recuperar(professor.disciplinasTeste[i]);
                        var disciplinaViewModel = Mapper.Map<Disciplina, DisciplinaViewModel>(disciplina);

                        ListaDisciplinas.Add(disciplinaViewModel);
                        //professor.Disciplinas.Add(disciplinaViewModel);
                    }
                }

                professor.Disciplinas = ListaDisciplinas;

                professor.Funcao = "1";

                var professorDomain = Mapper.Map<ProfessorViewModel, Professor>(professor);
                _professorApp.Incluir(professorDomain);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                var mensagemErro = ex.Message.ToString();
                return RedirectToAction("Index", "Home", mensagemErro);
            }

            //}
            //return View("AdicionarFuncionario", "Funcionario", professor.Id);
        }

        // GET: Professor/Edit/5
        public ActionResult Edit(int id)
        {
            var professor = _professorApp.Recuperar(id);
            var professorViewModel = Mapper.Map<Professor, ProfessorViewModel>(professor);

            return View("EditarProfessor", professorViewModel);
        }

        // POST: Professor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfessorViewModel professor)
        {
            if (ModelState.IsValid)
            {
                var professorDomain = Mapper.Map<ProfessorViewModel, Professor>(professor);
                _professorApp.Atualizar(professorDomain);

                return RedirectToAction("Index", "Home");
            }
            return View("Index", "Home", professor);
        }

        // GET: Professor/Delete/5
        public ActionResult Delete(int id)
        {
            var professor = _professorApp.Recuperar(id);
            var professorViewModel = Mapper.Map<Professor, ProfessorViewModel>(professor);

            return View("ExcluirProfessor", professorViewModel);
        }

        // POST: Professor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var professor = _professorApp.Recuperar(id);
            _professorApp.Remover(professor);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult VisualizarTurmasProfessor(int ProfessorId)
        {
            var turmas = _turmaServico.RecuperarTodos();
            var turmasMapped = Mapper.Map<IEnumerable<Turma>, IEnumerable<TurmaViewModel>>(turmas);
            List<TurmaViewModel> ListaTurmas = new List<TurmaViewModel>(); 

            foreach (var turma in turmasMapped)
            {
                foreach (var professor in turma.Professores)
                {
                    if (professor.Id == ProfessorId)
                    {
                        ListaTurmas.Add(turma);
                    }
                }
            }

            IEnumerable<TurmaViewModel> TurmasRetorno = ListaTurmas;
            return View("VisualizarTurmasLeciono", TurmasRetorno);
        }


        #region Métodos Específicos
        [HttpGet]
        public ActionResult ExibirNotificacoes(ProfessorViewModel professor)
        {
            try
            {
                var enumNotificacoes = _usuarioApp.ExibirNotificacoesUsuario(professor.indicadorAcesso, professor.Id);
                var notificacoesViewModel = Mapper.Map<IEnumerable<Notificacao>, IEnumerable<NotificacaoViewModel>>(enumNotificacoes);
                return View("ExibirNotificacoes", notificacoesViewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("DashboardSecundaria");
            }
        }

        private void PreencherListaDisciplina(ProfessorViewModel funcionario)
        {
            List<SelectListItem> listaDisciplinas = new List<SelectListItem>();
            var enumDisciplinas = _disciplinaApp.RecuperarTodos();

            foreach (var disc in enumDisciplinas)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = disc.DisciplinaId.ToString(),
                    Text = disc.NomeDisciplina
                };
                listaDisciplinas.Add(listItem);
            }

            funcionario.ListaDisciplinas = listaDisciplinas;
            ViewBag.ListaDisciplinas = funcionario.ListaDisciplinas;
        }


        #endregion

    }
}
>>>>>>> 47221abbbeff2cbed25e535b0cb20e2bfd2188b3
