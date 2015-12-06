using AutoMapper;
using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Servicos;
using SchoolManagement.MVC.Utilitarios;
using SchoolManagement.MVC.ViewModels;
using SchoolManagement.MVC.ViewModels.FiltroViewModel;
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
        private readonly IAlunoServico _alunoServico;
        private readonly Utilizavel _util;

        public ProfessorController(IProfessorServico professorApp, IUsuarioServico usuarioApp, IDisciplinaServico disciplinaApp, ITurmaServico turmaServico, IAlunoServico alunoServico, Utilizavel util)
        {
            _professorApp = professorApp;
            _usuarioApp = usuarioApp;
            _disciplinaApp = disciplinaApp;
            _turmaServico = turmaServico;
            _alunoServico = alunoServico;
            _util = util;
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
                professor.indicadorAcesso = 4;

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
        public ActionResult VisualizarTurmasProfessorLeciona()
        {
            int ProfessorId = (int)Session["UsuarioId"];
            var turmas = _turmaServico.RecuperarTurmasQueProfessorLeciona(ProfessorId);
            var turmasMapped = Mapper.Map<IEnumerable<Turma>, IEnumerable<TurmaViewModel>>(turmas);

            IEnumerable<TurmaViewModel> TurmasRetorno = turmasMapped;
            return View("VisualizarTurmasProfessorLeciona", TurmasRetorno);
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

            return View("FiltroTurmasProfessorLeciona");

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


        [HttpPost]
        public ActionResult VisualizarAlunosTurmasProfessorLeciona()
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
            return View("VisualizarAlunosTurmasProfessorLeciona", alunosMapped);
        }

        public ActionResult DetalhesTurmaProfessorLeciona(int id)
        {
            var turma = _turmaServico.Recuperar(id);
            var turmaViewModel = Mapper.Map<Turma, TurmaViewModel>(turma);

            return View("DetalhesTurmaProfessorLeciona", turmaViewModel);
        }



        [HttpGet]
        public ActionResult IncluirProfessorEmTurma()
        {
            ViewBag.ListaProfessores = _util.PreencherListaProfessores();
            ViewBag.ListaTurmas = _util.PreencherListaTurmas();

            return View("AssociarProfessorTurmaParte1");
        }

        [HttpPost]
        public ActionResult IncluirProfessorEmTurma(ProfessorViewModel professor)
        {
            var attmpt = _professorApp.IncluirProfessorEmTurma(professor.professorSelecionado, professor.turmaSelecionada);
            if (attmpt)
                return RedirectToAction("Index", "Home");
            else
                throw new NotImplementedException("Erro ao incluir Professor em Turma");
        }


        [HttpGet]
        public ActionResult RemoverProfessorDeTurma()
        {
            ViewBag.ListaProfessores = _util.PreencherListaProfessores();
            ViewBag.ListaTurmas = _util.PreencherListaTurmas();

            return View("AssociarProfessorTurmaParte1");
        }

        public ActionResult RemoverProfessorTurma(ProfessorViewModel professor)
        {
            var attmpt = _professorApp.RemoverProfessorDeTurma(professor.professorSelecionado, professor.turmaSelecionada);
            if (attmpt)
                return RedirectToAction("Index", "Home");
            else
                throw new NotImplementedException("Erro ao remover Professor de Turma");
        }
    }
}
