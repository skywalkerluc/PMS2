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
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioServico _funcionarioApp;
        private readonly IDisciplinaServico _disciplinaServico;
        private readonly IProfessorServico _professorServico;

        public FuncionarioController(IFuncionarioServico funcionarioApp, IDisciplinaServico disciplinaServico, IProfessorServico professorServico)
        {
            _funcionarioApp = funcionarioApp;
            _disciplinaServico = disciplinaServico;
            _professorServico = professorServico;
        }

        //
        // GET: /Funcionario/
        public ActionResult Index()
        {
            try
            {
                var enumeradorFuncionarios = _funcionarioApp.RecuperarTodos();
                var funcionarioViewModel = Mapper.Map<IEnumerable<Funcionario>, IEnumerable<FuncionarioViewModel>>(enumeradorFuncionarios);
                return View("Index", funcionarioViewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Create");
            }
        }

        //
        // GET: /Funcionario/Details/5
        public ActionResult Details(int id)
        {
            var funcionario = _funcionarioApp.Recuperar(id);
            var funcionarioViewModel = Mapper.Map<Funcionario, FuncionarioViewModel>(funcionario);

            return View("DetalhesFuncionario", funcionarioViewModel);
        }

        //
        // GET: /Funcionario/Create
        public ActionResult Create()
        {
            var funcionario = new FuncionarioViewModel();
            return View("AdicionarFuncionarioNormal");
        }

        //
        // POST: /Funcionario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FuncionarioViewModel funcionario)
        {
            //    if (ModelState.IsValid)
            //{
            try
            {
                funcionario.Funcao = "2";

                var funcionarioDomain = Mapper.Map<FuncionarioViewModel, Funcionario>(funcionario);
                _funcionarioApp.Incluir(funcionarioDomain);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                var mensagemErro = ex.Message.ToString();
                return RedirectToAction("Index", "Home", mensagemErro);
            }
            //}
            //return View("ExibirInformacoes");
        }

        //
        // GET: /Funcionario/Edit/5
        public ActionResult Edit(int id)
        {
            var funcionario = _funcionarioApp.Recuperar(id);
            var funcionarioViewModel = Mapper.Map<Funcionario, FuncionarioViewModel>(funcionario);
            return View("EditarFuncionario", funcionarioViewModel);
        }

        //
        // POST: /Funcionario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FuncionarioViewModel funcionario)
        {
            if (ModelState.IsValid)
            {
                var funcionarioDomain = Mapper.Map<FuncionarioViewModel, Funcionario>(funcionario);
                _funcionarioApp.Atualizar(funcionarioDomain);

                return RedirectToAction("Index", "Home");
            }
            return View("Index", "Home", funcionario);
        }

        //
        // GET: /Funcionario/Delete/5
        public ActionResult Delete(int id)
        {
            var funcionario = _funcionarioApp.Recuperar(id);
            var funcionarioViewModel = Mapper.Map<Funcionario, FuncionarioViewModel>(funcionario);

            return View("DeleteFuncionario", funcionarioViewModel);
        }

        //
        // POST: /Funcionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var funcionario = _funcionarioApp.Recuperar(id);

            _funcionarioApp.Remover(funcionario);

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult EscolhaFuncaoCadastro()
        {
            Utilizavel util = new Utilizavel();
            var filtro = new FiltroFuncionario();
            filtro.ListaFuncoes = util.PreencherListaFuncoes();
            ViewBag.ListaFuncoes = filtro.ListaFuncoes;
            return View("FiltroFuncaoCadastrar");
        }

        [HttpPost]
        public ActionResult EscolhaFuncaoCadastro(FiltroFuncionario funcionario)
        {
            if (funcionario.FuncaoSelecionada == 1)
            {
                var professor = new ProfessorViewModel();
                PreencherListaDisciplina(professor);
                return View("AdicionarFuncionario");
            }
            else if (funcionario.FuncaoSelecionada == 2)
            {
                return View("AdicionarFuncionarioNormal");
            }
            else
            {
                Utilizavel util = new Utilizavel();
                var filtro = new FiltroFuncionario();
                filtro.ListaFuncoes = util.PreencherListaFuncoes();
                ViewBag.ListaFuncoes = filtro.ListaFuncoes;
                return View("FiltroFuncaoCadastrar");
            }

        }

        //GET
        [HttpGet]
        public ActionResult RecuperarFuncionariosPorNome()
        {
            Utilizavel util = new Utilizavel();
            var filtro = new FiltroFuncionario();
            filtro.ListaFuncoes = util.PreencherListaFuncoes();
            ViewBag.ListaFuncoes = filtro.ListaFuncoes;
            return View("filtroconsultafuncionario");
        }

        //POST
        [HttpPost]
        public ActionResult RecuperarFuncionariosPorNome(FiltroFuncionario funcionario)
        {
            if (funcionario.NomeFuncionario == string.Empty || funcionario.NomeFuncionario == null)
            {
                if (funcionario.FuncaoSelecionada == 1)
                {
                    var professores = _professorServico.RecuperarTodos();
                    var professoresMapped = Mapper.Map<IEnumerable<Funcionario>, IEnumerable<FuncionarioViewModel>>(professores);
                    return View("ResultadoConsultaFuncionario", professoresMapped.ToList());
                }
                else
                {
                    var func = _funcionarioApp.RecuperarTodos();
                    var funcMapped = Mapper.Map<IEnumerable<Funcionario>, IEnumerable<FuncionarioViewModel>>(func);
                    return View("ResultadoConsultaFuncionario", funcMapped.ToList());
                }
            }
            else
            {
                if (funcionario.FuncaoSelecionada == 1)
                {
                    var prof = _professorServico.BuscarPorNome(funcionario.NomeFuncionario);
                    var profMapped = Mapper.Map<IEnumerable<Funcionario>, IEnumerable<FuncionarioViewModel>>(prof);
                    return View("ResultadoConsultaFuncionario", profMapped.ToList());
                }
                else
                {
                    var func = _funcionarioApp.FiltrarFuncionario(null, funcionario.NomeFuncionario);
                    var funcMapped = Mapper.Map<IEnumerable<Funcionario>, IEnumerable<FuncionarioViewModel>>(func);
                    return View("ResultadoConsultaFuncionario", funcMapped.ToList());
                }
            }
        }

        private void PreencherListaDisciplina(ProfessorViewModel funcionario)
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

            funcionario.ListaDisciplinas = listaDisciplinas;
            ViewBag.ListaDisciplinas = funcionario.ListaDisciplinas;
        }
        
        public ActionResult VisualizarTodosFuncionarios()
        {
            var func = _funcionarioApp.RecuperarTodos();
            var funcMapped = Mapper.Map<IEnumerable<Funcionario>, IEnumerable<FuncionarioViewModel>>(func);

            return View("VisualizarTodosFuncionarios", funcMapped);
        }
    }
}
