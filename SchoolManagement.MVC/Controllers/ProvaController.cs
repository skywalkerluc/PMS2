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

        public ProvaController(IProvaServico provaApp, IDisciplinaServico disciplinaServico, IProfessorServico professorApp, IResultadosProvasServico resultadoProvaApp, IAlunoServico alunoApp)
        {
            _provaApp = provaApp;
            _disciplinaServico = disciplinaServico;
            _professorApp = professorApp;
            _resultadoProvaApp = resultadoProvaApp;
            _alunoApp = alunoApp;
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
            PreencherListaDisciplina(prova);
            PreencherListaProfessor(prova);
            return View("CadastrarProva");
        }

        // POST: Prova/Create
        [HttpPost]
        public ActionResult Create(ProvaViewModel prova)
        {
            try
            {
                if (prova.professoresLista != 0)
                {
                   var professor = _professorApp.Recuperar(prova.professoresLista);
                   var professorViewModel = Mapper.Map<Professor, ProfessorViewModel>(professor);

                   prova.Professores = professorViewModel;
                }
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

                prova.DataProva = DateTime.Now.Date;

                var provaDomain = Mapper.Map<ProvaViewModel, Prova>(prova);
                
                _provaApp.IncluirProva(provaDomain);
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
            var prova = _provaApp.Recuperar(id);
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
            var prova = _provaApp.Recuperar(id);
            var provaViewModel = Mapper.Map<Prova, ProvaViewModel>(prova);

            return View("ExcluirProva", provaViewModel);
        }

        // POST: Prova/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var prova = _provaApp.Recuperar(id);

            var provaSelecionado = Mapper.Map<Prova, Prova>(prova);
            _provaApp.Remover(provaSelecionado);

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
            PreencherListaDisciplina(prova);
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
        
    }
}
