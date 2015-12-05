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
    public class DisciplinaController : Controller
    {
        private readonly IDisciplinaServico _disciplinaApp;
        private readonly ITurmaServico _turmaServico;
        private readonly IProfessorServico _profServico;
        private readonly ILivroServico _livroServicoApp;
        private readonly IAlunoServico _alunoApp;

        private Utilizavel utilizavel;

        public DisciplinaController(IDisciplinaServico disciplinaApp, ITurmaServico turmaServico, IProfessorServico professorServico, ILivroServico livroServico, IAlunoServico alunoApp)
        {
            _disciplinaApp = disciplinaApp;
            utilizavel = new Utilizavel(null, turmaServico, professorServico, livroServico, null);
            _turmaServico = turmaServico;
            _profServico = professorServico;
            _livroServicoApp = livroServico;
            _alunoApp = alunoApp;
        }

        // GET: Disciplina
        public ActionResult Index()
        {
            return View();
        }

        // GET: Disciplina/Details/5
        public ActionResult Details(int id)
        {
            var disciplina = _disciplinaApp.Recuperar(id);
            var disciplinaViewModel = Mapper.Map<Disciplina, DisciplinaViewModel>(disciplina);

            return View("DetalhesDisciplina", disciplinaViewModel);
        }

        // GET: Disciplina/Create
        public ActionResult Create()
        {
            var disciplina = new DisciplinaViewModel();

            FiltroDisciplina filtro = new FiltroDisciplina();
            filtro.ListaLivros = utilizavel.PreencherListaLivros();

            ViewBag.ListaLivro = filtro.ListaLivros;

            return View("AdicionarDisciplinas");
        }


        // POST: Disciplina/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DisciplinaViewModel disciplina)
        {
            try
            {
                List<Livro> ListaLivros = new List<Livro>();
                if (disciplina.livrosSelecionados.Count > 0)
                {
                    for (int i = 0; i <= disciplina.livrosSelecionados.Count - 1; i++)
                    {
                        var livro = _livroServicoApp.Recuperar(disciplina.livrosSelecionados[i]);
                        if (livro != null)
                        {
                            ListaLivros.Add(livro);
                        }
                    }
                }

                var disciplinaDomain = Mapper.Map<DisciplinaViewModel, Disciplina>(disciplina);
                disciplinaDomain.Livros = ListaLivros;
                _disciplinaApp.IncluirDisciplina(disciplinaDomain);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                var mensagemErro = ex.Message.ToString();
                return RedirectToAction("Index", "Home", mensagemErro);
            }
        }

        // GET: Disciplina/Edit/5
        public ActionResult Edit(int id)
        {
            var disicplina = _disciplinaApp.Recuperar(id);
            var disicplinaViewModel = Mapper.Map<Disciplina, DisciplinaViewModel>(disicplina);

            return View("EditarDisciplinas", disicplinaViewModel);
        }

        // POST: Disciplina/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DisciplinaViewModel disciplina)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var disciplinaDomain = Mapper.Map<DisciplinaViewModel, Disciplina>(disciplina);
                    _disciplinaApp.Atualizar(disciplinaDomain);

                    return RedirectToAction("Index", "Home");
                }
                return View("Index", "Home", disciplina);
            }
            catch
            {
                return View();
            }
        }

        // GET: Disciplina/Delete/5
        public ActionResult Delete(int id)
        {
            var disciplina = _disciplinaApp.Recuperar(id);
            var disciplinaViewModel = Mapper.Map<Disciplina, DisciplinaViewModel>(disciplina);

            return View("ExcluirDisciplinas", disciplinaViewModel);
        }

        // POST: Disciplina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                var disciplina = _disciplinaApp.Recuperar(id);

                var disciplinaSelecionado = Mapper.Map<Disciplina, Disciplina>(disciplina);
                _disciplinaApp
                    .Remover(disciplinaSelecionado);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        //GET
        [HttpGet]
        public ActionResult FiltrarDisciplina()
        {
            FiltroDisciplina filter = new FiltroDisciplina();
            filter.ListaLivros = utilizavel.PreencherListaLivros();

            ViewBag.ListaLivros = filter.ListaLivros;

            return View("FiltroParaConsultaDeDisciplinas", filter);
        }

        //POST
        [HttpPost]
        public ActionResult FiltrarDisciplina(FiltroDisciplina disciplina)
        {
            //var nome = disciplina.NomeDisciplina.Trim();
            //var disciplina2 = _disciplinaApp.BuscarPorNome(nome);
            //var disciplina3 = Mapper.Map<IEnumerable<Disciplina>, IEnumerable<DisciplinaViewModel>>(disciplina2);

            var filtro = _disciplinaApp.FiltroDisciplina(disciplina.NomeDisciplina, disciplina.LivroId);
            var filtroMapeado = Mapper.Map<IEnumerable<Disciplina>, IEnumerable<DisciplinaViewModel>>(filtro);

            return View("ResultadoConsultaDisciplinas", filtroMapeado.ToList());
        }

        public ActionResult VisualizarTodasDisciplinas()
        {
            var disc = _disciplinaApp.RecuperarTodos();
            var discMapped = Mapper.Map<IEnumerable<Disciplina>, IEnumerable<DisciplinaViewModel>>(disc);
            return View("VisualizarTodasDisciplinas", discMapped);
        }

        public ActionResult VerDisciplinasMinhaTurma()
        {
            int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());

            var aluno = _alunoApp.Recuperar(idUsuario);
            var alunosTurma = _disciplinaApp.RecuperarDisciplinasTurma(aluno.Turma.TurmaId);

            var disciplinasMapeados = Mapper.Map<IEnumerable<Disciplina>, IEnumerable<DisciplinaViewModel>>(alunosTurma);

            return View("VisualizarDisciplinasMinhaTurma", disciplinasMapeados);
        }

        public ActionResult DetalhesDisciplinasMinhaTurma(int id)
        {
            var disciplina = _disciplinaApp.Recuperar(id);
            var disciplinaViewModel = Mapper.Map<Disciplina, DisciplinaViewModel>(disciplina);

            return View("DetalhesDisciplinasMinhaTurma", disciplinaViewModel);
        }


    }
}
