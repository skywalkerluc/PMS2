using AutoMapper;
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
    public class ProvaController : Controller
    {
        private readonly IDisciplinaServico _disciplinaServico;
        private readonly IProvaServico _provaApp;
        private readonly IProfessorServico _professorApp;
        private readonly IResultadosProvasServico _resultadoProvaApp;
        private readonly IAlunoServico _alunoApp;
        private readonly INotificacaoServico _notificacaoServico;
        private readonly ITurmaServico _turmaServico;

        public ProvaController(IProvaServico provaApp, IDisciplinaServico disciplinaServico, IProfessorServico professorApp, IResultadosProvasServico resultadoProvaApp, IAlunoServico alunoApp, INotificacaoServico notificacaoServico, ITurmaServico turmaServico)
        {
            _provaApp = provaApp;
            _disciplinaServico = disciplinaServico;
            _professorApp = professorApp;
            _resultadoProvaApp = resultadoProvaApp;
            _alunoApp = alunoApp;
            _notificacaoServico = notificacaoServico;
            _turmaServico = turmaServico;
        }

        // GET: Prova
        public ActionResult Index()
        {
            return View();
        }

        // GET: Prova/Details/5
        public ActionResult Details(int id)
        {
            var prova = _provaApp.Recuperar(id);
            var provaViewModel = Mapper.Map<Prova, ProvaViewModel>(prova);

            return View("DetalhesProva", provaViewModel);
        }

        // GET: Prova/Create
        public ActionResult Create()
        {

            var prova = new ProvaViewModel();
            PreencherListaDisciplinaProfessor(prova);
            PreencherListaTurmasProfessor(prova);
            return View("CadastrarProva");
        }

        // POST: Prova/Create
        [HttpPost]
        public ActionResult Create(ProvaViewModel prova)
        {
            try
            {
                if(prova.disciplinasTeste != 0)
                {
                    var disciplina = _disciplinaServico.Recuperar(prova.disciplinasTeste);
                    var disciplinaViewModel = Mapper.Map<Disciplina, DisciplinaViewModel>(disciplina);

                    prova.Disciplina = disciplinaViewModel;
                }

                //adiquirindo valor dos enumeradores
                SchoolManagement.MVC.ViewModels.StatusProva status = prova.StatusProva;
                int valorEnumStatus = (int)Enum.Parse(typeof(SchoolManagement.MVC.ViewModels.StatusProva), Enum.GetName(typeof(SchoolManagement.MVC.ViewModels.StatusProva), status));

                SchoolManagement.MVC.ViewModels.TipoProva tipo = prova.TipoProva;
                int valorEnumTipo = (int)Enum.Parse(typeof(SchoolManagement.MVC.ViewModels.TipoProva), Enum.GetName(typeof(SchoolManagement.MVC.ViewModels.TipoProva), tipo));
                //fim adiquirindo valor dos enumeradores

                prova.status = valorEnumStatus;
                prova.tipo = valorEnumTipo;

                var turma = Mapper.Map<Turma,TurmaViewModel>(_turmaServico.Recuperar(prova.turmaLista));
                prova.Turma = turma;

                var professor = Mapper.Map<Professor, ProfessorViewModel>(_professorApp.Recuperar((int)Session["UsuarioId"]));
                prova.Professores = professor;

                var provaDomain = Mapper.Map<ProvaViewModel, Prova>(prova);
                
                var attmpt = _provaApp.IncluirProva(provaDomain);

                if (attmpt != null)
                {
                    
                    var notif = new NotificacaoViewModel()
                    {
                        Assunto = "Uma nova prova foi adicionada.",
                        DataCriacao = DateTime.Now,
                        Descricao = "Uma nova prova foi adicionada pelo professor: " + attmpt.Professores.Nome + ", para a turma: " + attmpt.Turma.Descricao + "(" + RecuperarValorHorarioTurma(attmpt.Turma.HorariosTurmaId) + ")"
                        + "para o dia: " + attmpt.DataProva.ToString() + ". É uma prova do tipo '" + RecuperarTipoProva(attmpt.TipoProva) + "'.",
                        UsuarioCriacao = Mapper.Map<Professor, ProfessorViewModel>(_professorApp.Recuperar((int)Session["UsuarioId"])),
                        TurmaPublicoAlvo = Mapper.Map<Turma, TurmaViewModel>(attmpt.Turma)
                    };
                    var notifMapped = Mapper.Map<NotificacaoViewModel, Notificacao>(notif);
                    var attmptNotf = _notificacaoServico.CriarNotificacao(notifMapped);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                var mensagemErro = ex.Message.ToString();
                return RedirectToAction("Index", "Home", mensagemErro);
            }
        }

        // GET: Prova/Edit/5
        public ActionResult Edit(int id)
        {
            var prova = _provaApp.RecuperarProva(id);
            var provaViewModel = Mapper.Map<Prova, ProvaViewModel>(prova);

            return View("EditarProva", provaViewModel);
        }

        // POST: Prova/Edit/5
        [HttpPost]
        public ActionResult Edit(ProvaViewModel prova)
        {
            if (ModelState.IsValid)
            {
                var provaDomain = Mapper.Map<ProvaViewModel, Prova>(prova);
                _provaApp.AtualizarDadosProva(provaDomain);

                return RedirectToAction("Index", "Home");
            }
            return View("Index", "Home", prova);
        }

        // GET: Prova/Delete/5
        public ActionResult Delete(int id)
        {
            var prova = _provaApp.RecuperarProva(id);
            var provaViewModel = Mapper.Map<Prova, ProvaViewModel>(prova);

            return View("ExcluirProva", provaViewModel);
        }

        // POST: Prova/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var prova = _provaApp.RecuperarProva(id);

            var provaSelecionado = Mapper.Map<Prova, Prova>(prova);
            _provaApp.ExcluirProva(provaSelecionado.ProvaId);
            
            return RedirectToAction("Index", "Home");
        }

        private void PreencherListaDisciplina(ProvaViewModel prova)
        {
            List<SelectListItem> listaDisciplinas = new List<SelectListItem>();
            var enumDisciplinas = _disciplinaServico.RecuperarTodos();

            foreach (var disc in enumDisciplinas)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = disc.DisciplinaId.ToString(),
                    Text = disc.NomeDisciplina
                };
                listaDisciplinas.Add(listItem);
            }

            prova.ListaDisciplinas = listaDisciplinas;
            ViewBag.ListaDisciplinas = prova.ListaDisciplinas;
        }

        private void PreencherListaTurmasProfessor(ProvaViewModel prova)
        {
            var turmas = _turmaServico.RecuperarTurmasQueProfessorLeciona((int)Session["UsuarioId"]);
            foreach (var turma in turmas)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = turma.TurmaId.ToString(),
                    Text = String.Concat(turma.Descricao, " (", this.RecuperarValorHorarioTurma(turma.HorariosTurmaId), ")")
                };
                prova.ListaTurmas = new List<SelectListItem>();
                prova.ListaTurmas.Add(select);
            }
            ViewBag.ListaTurmas = prova.ListaTurmas;
        }

        private void PreencherListaDisciplinaProfessor(ProvaViewModel prova)
        {
            List<SelectListItem> listaDisciplinas = new List<SelectListItem>();
            var disciplinas = _disciplinaServico.RecuperarDisciplinasProfessorLeciona((int)Session["UsuarioId"]);

            foreach (var disciplina in disciplinas)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = disciplina.DisciplinaId.ToString(),
                    Text = disciplina.NomeDisciplina
                };
                listaDisciplinas.Add(select);
            }
            prova.ListaDisciplinas = listaDisciplinas;
            ViewBag.ListaDisciplinas = prova.ListaDisciplinas;
        }

        private void PreencherListaProfessor(ProvaViewModel prova)
        {
            List<SelectListItem> listaProfessor = new List<SelectListItem>();
            var enumProfessor = _professorApp.RecuperarTodos();

            foreach (var disc in enumProfessor)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = disc.Id.ToString(),
                    Text = disc.Nome
                };
                listaProfessor.Add(listItem);
            }

            prova.ListaProfessores = listaProfessor;
            ViewBag.ListaProfessores = prova.ListaProfessores;
        }

        public ActionResult CalendarioDeProva()
        {
            return View("CalendarioDeProva");
        }

        //GET
        [HttpGet]
        public ActionResult RecuperarProvaPorDisciplina()
        {
            var prova = new ProvaViewModel();
            PreencherListaDisciplinaProfessor(prova);
            return View("FiltroConsultaProva");
        }

        //POST
        [HttpPost]
        public ActionResult RecuperarProvaPorDisciplina(ProvaViewModel prova)
        {
            var disciplina = prova.disciplinasTeste;
            var prova2 = _provaApp.BuscarPorDisciplina(disciplina);
            var prova3 = Mapper.Map<IEnumerable<Prova>, IEnumerable<ProvaViewModel>>(prova2);

            return View("ResultadoPesquisaProva", prova3.ToList()); 
        }




        public ActionResult VisualizarNotasDoAluno()
        {
            int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());

            var aluno = _alunoApp.Recuperar(idUsuario);
            var provaAluno = _alunoApp.RecuperarResultadosAluno(aluno);

            var provaMapeados = Mapper.Map<IEnumerable<ResultadosProvas>, IEnumerable<ResultadosProvasViewModel>>(provaAluno);

            return View("ResultadoDeNotasDeProva", provaMapeados);
        }

        public ActionResult DetalhesResultadoSelecionado(int id)
        {
            var prova = _resultadoProvaApp.Recuperar(id);
            var provaViewModel = Mapper.Map<ResultadosProvas, ResultadosProvasViewModel>(prova);

            return View("DetalhesResultadoNotaSelecionada", provaViewModel);
        }

        
        public ActionResult RecuperarProvasProfessor(int ProfessorId)
        {
            var attmpt = _provaApp.RecuperarProvasProfessor(ProfessorId);
            var provasMapped = Mapper.Map<IEnumerable<Prova>, IEnumerable<ProvaViewModel>>(attmpt);
            return View("RecuperarProvasProfessor", provasMapped);
        }

        public ActionResult VisualizarProvasTurma()
        {
            int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());

            var aluno = _alunoApp.Recuperar(idUsuario);

            var prova = _provaApp.RecuperarProvasTurma(aluno.Turma.TurmaId);
            var provaViewModel = Mapper.Map<IEnumerable<Prova>, IEnumerable<ProvaViewModel>>(prova);

            return View("VisualizarProvasTurma", provaViewModel);
        }

        public ActionResult DetalhesProvaSelecionada(int id)
        {
            var prova = _provaApp.Recuperar(id);
            var provaViewModel = Mapper.Map<Prova, ProvaViewModel>(prova);

            return View("DetalhesProvaSelecionada", provaViewModel);
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

        private string RecuperarTipoProva(int value)
        {
            string descricaoRetorno = string.Empty;
            switch (value)
            {
                case 1:
                    descricaoRetorno = "Prova normal";
                    break;
                case 2:
                    descricaoRetorno = "Prova de recuperação";
                    break;
                case 3:
                    descricaoRetorno = "Prova de recuperação";
                    break;
                case 4:
                    descricaoRetorno = "Prova de segunda chamada";
                    break;
                case 5:
                    descricaoRetorno = "Prova final";
                    break;
                default:
                    break;
            }
            return descricaoRetorno;
        }

        public ActionResult VisualizarProvasTurmaAlunoResponsavel(ResponsavelViewModel responsavel)
        {
            var aluno1 = responsavel.alunoSelecionado;
            var aluno2 = _alunoApp.Recuperar(aluno1);

            var prova = _provaApp.RecuperarProvasTurma(aluno2.Turma.TurmaId);
            var provaViewModel = Mapper.Map<IEnumerable<Prova>, IEnumerable<ProvaViewModel>>(prova);

            return View("VisualizarProvasTurmaAlunoResponsavel", provaViewModel);
        }

        public ActionResult DetalhesProvaSelecionadaResponsavel(int id)
        {
            var prova = _provaApp.Recuperar(id);
            var provaViewModel = Mapper.Map<Prova, ProvaViewModel>(prova);

            return View("DetalhesProvaSelecionadaResponsavel", provaViewModel);
        }

        public ActionResult VisualizarNotasDoAlunoResponsavelSelecionado(ResponsavelViewModel responsavel)
        {
            var aluno1 = responsavel.alunoSelecionado;
            var aluno2 = _alunoApp.Recuperar(aluno1);

            var provaAluno = _alunoApp.RecuperarResultadosAluno(aluno2);

            var provaMapeados = Mapper.Map<IEnumerable<ResultadosProvas>, IEnumerable<ResultadosProvasViewModel>>(provaAluno);

            return View("ResultadoDeNotasDeProvaResponsavelAluno", provaMapeados);
        }

        public ActionResult DetalhesResultadoSelecionadoResponsavel(int id)
        {
            var prova = _resultadoProvaApp.Recuperar(id);
            var provaViewModel = Mapper.Map<ResultadosProvas, ResultadosProvasViewModel>(prova);

            return View("DetalhesResultadoNotaSelecionadaResponsavel", provaViewModel);
        }
    }
}
