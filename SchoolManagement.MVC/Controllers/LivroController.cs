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
    public class LivroController : Controller
    {
        private readonly ILivroServico _livroServicoApp;

        private readonly ITurmaServico _turmaServico;
        private readonly IDisciplinaServico _disciplinaApp;

        private Utilizavel utilizavel;

        public LivroController(ILivroServico livroServicoApp, IDisciplinaServico disciplinaApp, ITurmaServico turmaServico)
        {
            _livroServicoApp = livroServicoApp;
            _turmaServico = turmaServico;
            _disciplinaApp = disciplinaApp;
            utilizavel = new Utilizavel(disciplinaApp, turmaServico, null, livroServicoApp, null);
        }
        // GET: Livro
        public ActionResult Index()
        {
            return View();
        }

        // GET: Livro/Details/5
        public ActionResult Details(int id)
        {
            var livro = _livroServicoApp.Recuperar(id);
            var livroViewModel = Mapper.Map<Livro, LivroViewModel>(livro);

            return View("DetalhesLivro", livroViewModel);
        }

        // GET: Livro/Create
        public ActionResult Create()
        {
            var livro = new LivroViewModel();
            return View("AdicionarLivros");
        }

        // POST: Livro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LivroViewModel livro)
        {
            try
            {
                var livroDomain = Mapper.Map<LivroViewModel, Livro>(livro);
                _livroServicoApp.Incluir(livroDomain);

                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                var mensagemErro = ex.Message.ToString();
                return RedirectToAction("Index", "Home", mensagemErro);
            }
        }

        // GET: Livro/Edit/5
        public ActionResult Edit(int id)
        {
            var livro = _livroServicoApp.Recuperar(id);
            var livroViewModel = Mapper.Map<Livro, LivroViewModel>(livro);
            return View("EditarLivro", livroViewModel);
        }

        // POST: Livro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LivroViewModel livro)
        {
            if (ModelState.IsValid)
            {
                var livroDomain = Mapper.Map<LivroViewModel, Livro>(livro);
                _livroServicoApp.Atualizar(livroDomain);

                return RedirectToAction("Index", "Home");
            }
            return View("Index", "Home", livro);
        }

        // GET: Livro/Delete/5
        public ActionResult Delete(int id)
        {
            var livro = _livroServicoApp.Recuperar(id);
            var livroViewModel = Mapper.Map<Livro, LivroViewModel>(livro);

            return View("ExcluirLivro", livroViewModel);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                var livro = _livroServicoApp.Recuperar(id);

                var livroSelecionado = Mapper.Map<Livro, Livro>(livro);
                _livroServicoApp.Remover(livroSelecionado);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //GET
        [HttpGet]
        public ActionResult FiltrarLivros()
        {
            FiltroLivro filtro = new FiltroLivro();
            filtro.ListaTurmas = utilizavel.PreencherListaTurmas();
            filtro.ListaDisciplinas = utilizavel.PreencherListaDisciplina();

            ViewBag.ListaDisciplinas = filtro.ListaDisciplinas;
            ViewBag.ListaTurmas = filtro.ListaTurmas;

            return View("filtroparaconsultadelivros", filtro);
        }

        //POST
        [HttpPost]
        public ActionResult FiltrarLivros(FiltroLivro livro)
        {

            if (livro.disciplinaEscolhida != 0)
            {
                var disciplina = _disciplinaApp.Recuperar(livro.disciplinaEscolhida);
                var disciplinaViewModel = Mapper.Map<Disciplina, DisciplinaViewModel>(disciplina);

                livro.Disciplina = disciplinaViewModel;
            }
            if (livro.turmaEscolhida != 0)
            {
                var turma = _turmaServico.Recuperar(livro.turmaEscolhida);
                var turmaViewModel = Mapper.Map<Turma, TurmaViewModel>(turma);

                livro.Turma = turmaViewModel;
            }
            
            var filtro = _livroServicoApp.FiltrarLivro(livro.NomeLivro, livro.NomeEditora, livro.NomeAutor);
            var filtroMapeado = Mapper.Map<IEnumerable<Livro>, IEnumerable<LivroViewModel>>(filtro);

            return View("ResultadoConsultaLivros", filtroMapeado.ToList());
        }

        public ActionResult VisualizarTodosMateriais()
        {
            var mat = _livroServicoApp.RecuperarTodos();
            var matMapped = Mapper.Map<IEnumerable<Livro>, IEnumerable<LivroViewModel>>(mat);
            return View("VisualizarTodosMateriais", matMapped);
        }

        
    }
}
