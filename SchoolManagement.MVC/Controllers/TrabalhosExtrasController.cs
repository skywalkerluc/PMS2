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
    public class TrabalhosExtrasController : Controller
    {
        private readonly ITrabalhosExtrasServico _trabalhosExtrasServico;
        private readonly Utilizavel _util;
        private readonly ITurmaServico _turmaServico;
        private readonly IAlunoServico _alunoApp;


        public TrabalhosExtrasController(ITrabalhosExtrasServico trabalhosExtrasServico, Utilizavel util, ITurmaServico turmaServico, IAlunoServico alunoApp)
        {
            _trabalhosExtrasServico = trabalhosExtrasServico;
            _util = util;
            _turmaServico = turmaServico;
            _alunoApp = alunoApp;
        }

        //
        // GET: /TrabalhosExtras/
        public ActionResult Index()
        {
            var trabalhos = _trabalhosExtrasServico.RecuperarTodos();
            var trabalhosMapped = Mapper.Map<IEnumerable<TrabalhosExtras>, IEnumerable<TrabalhosExtrasViewModel>>(trabalhos);
            return View("VisualizarTodosTrabalhosExtras", trabalhosMapped);
        }

        //
        // GET: /TrabalhosExtras/Details/5
        public ActionResult Details(int id)
        {
            var trabalhoExtra = _trabalhosExtrasServico.Recuperar(id);
            var trabalhoExtraMapped = Mapper.Map<TrabalhosExtras, TrabalhosExtrasViewModel>(trabalhoExtra);
            return View("DetalhesTrabalhoExtra", trabalhoExtraMapped);
        }

        //
        // GET: /TrabalhosExtras/Create
        public ActionResult Create()
        {
            TrabalhosExtrasViewModel trabalho = new TrabalhosExtrasViewModel();
            _util.PreencherListaTurmas();
            return View("AdicionarConteudoExtra", trabalho);
        }

        //
        // POST: /TrabalhosExtras/Create
        [HttpPost]
        public ActionResult Create(TrabalhosExtrasViewModel trabalho)
        {
            try
            {
                var turmaSelecionada = _turmaServico.Recuperar(trabalho.TurmaSelecionada);
                if (turmaSelecionada == null)
                {
                    ViewBag.AlertMessage = "Turma não encontrada. Erro ao adicionar trabalho extra.";
                    throw new NotImplementedException("Turma não encontrada. Erro ao adicionar trabalho extra.");
                }

                var trabalhoMapped = Mapper.Map<TrabalhosExtrasViewModel, TrabalhosExtras>(trabalho);
                trabalhoMapped.TurmaSelecionada = turmaSelecionada;
                var attmpt = _trabalhosExtrasServico.IncluirTrabalhoExtra(trabalhoMapped);

                if (!attmpt)
                {
                    ViewBag.AlertMessage = "Erro ao adicionar trabalho extra.";
                    throw new NotImplementedException("Erro ao adicionar trabalho extra.");
                }
                else
                {
                    return View("Index", "Home");
                }
            }
            catch (Exception)
            {
                ViewBag.AlertMessage = "Erro ao adicionar trabalho extra.";
                throw new NotImplementedException("Erro ao adicionar trabalho extra.");
            }
        }

        //
        // GET: /TrabalhosExtras/Edit/5
        public ActionResult Edit(int id)
        {
            var trabalhoExtra = _trabalhosExtrasServico.Recuperar(id);
            var trabalhoExtraMapped = Mapper.Map<TrabalhosExtras, TrabalhosExtrasViewModel>(trabalhoExtra);
            return View("AtualizarDadosConteudoExtra", trabalhoExtraMapped);
        }

        //
        // POST: /TrabalhosExtras/Edit/5
        [HttpPost]
        public ActionResult Edit(TrabalhosExtrasViewModel trabalhosExtras)
        {
            try
            {
                var trabalhosExtrasMapped = Mapper.Map<TrabalhosExtrasViewModel, TrabalhosExtras>(trabalhosExtras);
                var attempt = _trabalhosExtrasServico.Atualizar(trabalhosExtrasMapped);
                if (attempt)
                {
                    return View("Index", "Home");
                }
                else
                {
                    ViewBag.AlertMessage = "Erro ao editar trabalho extra";
                    throw new NotImplementedException("Erro ao editar trabalho extra");
                }
            }
            catch
            {
                ViewBag.AlertMessage = "Erro ao editar trabalho extra";
                throw new NotImplementedException("Erro ao editar trabalho extra");
            }
        }

        //
        // GET: /TrabalhosExtras/Delete/5
        public ActionResult Delete(int id)
        {
            var trabalhosExtras = _trabalhosExtrasServico.Recuperar(id);
            var trabalhosExtrasMapped = Mapper.Map<TrabalhosExtras, TrabalhosExtrasViewModel>(trabalhosExtras);
            return View("RemoverConteudoExtra", trabalhosExtrasMapped);
        }

        //
        // POST: /TrabalhosExtras/Delete/5
        [HttpPost]
        public ActionResult Delete(TrabalhosExtrasViewModel trabalhosExtras)
        {
            try
            {
                var trabalhosExtrasMapped = Mapper.Map<TrabalhosExtrasViewModel, TrabalhosExtras>(trabalhosExtras);
                var attempt = _trabalhosExtrasServico.Remover(trabalhosExtrasMapped);
                if (attempt)
                {
                    return View("Index", "Home");
                }
                else
                {
                    ViewBag.AlertMessage = "Erro ao remover conteúdo extra.";
                    throw new NotImplementedException("Erro ao remover conteúdo extra.");
                }
            }
            catch
            {
                ViewBag.AlertMessage = "Erro ao remover conteúdo extra.";
                throw new NotImplementedException("Erro ao remover conteúdo extra.");
            }
        }

        public ActionResult VisualizarTrabalhosExtrasMinhaTurma()
        {
            int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());

            var aluno = _alunoApp.Recuperar(idUsuario);
            var trabalhoTurma = _trabalhosExtrasServico.RecuperarTrabalhosTurma(aluno.Turma.TurmaId);

            var trabalhoViewModel = Mapper.Map < IEnumerable<TrabalhosExtras>, IEnumerable<TrabalhosExtrasViewModel>>(trabalhoTurma);

            return View("VisualizarTrabalhosExtrasMinhaTurma", trabalhoViewModel);
        }

        public ActionResult DetalhesTrabalhosExtrasMinhaTurma(int id)
        {
            var trabalho = _trabalhosExtrasServico.Recuperar(id);
            var trabalhoViewModel = Mapper.Map<TrabalhosExtras, TrabalhosExtrasViewModel>(trabalho);

            return View("DetalhesTrabalhosExtrasMinhaTurma", trabalhoViewModel);
        }

    }
}
