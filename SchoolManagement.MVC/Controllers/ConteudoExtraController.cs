﻿using AutoMapper;
using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Servicos;
using SchoolManagement.Domain.Servicos;
using SchoolManagement.MVC.Utilitarios;
using SchoolManagement.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.Controllers
{
    public class ConteudoExtraController : Controller
    {
        private readonly IConteudoExtraServico _conteudoExtraServico;
        private readonly Utilizavel _util;
        private readonly IProfessorServico _professorApp;
        private readonly ITurmaServico _turmaApp;
        private readonly IAlunoServico _alunoApp;

        public ConteudoExtraController(IConteudoExtraServico conteudoExtraServico, Utilizavel util, IProfessorServico professorApp, ITurmaServico turmaApp, IAlunoServico alunoApp)
        {
            _conteudoExtraServico = conteudoExtraServico;
            _util = util;
            _professorApp = professorApp;
            _turmaApp = turmaApp;
            _alunoApp = alunoApp;
        }

        //
        // GET: /ConteudoExtra/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /ConteudoExtra/Details/5
        public ActionResult Details(int id)
        {
            var conteudoExtra = _conteudoExtraServico.Recuperar(id);
            var conteudoMapped = Mapper.Map<ConteudosExtras, ConteudosExtrasViewModel>(conteudoExtra);
            return View("DetalhesConteudoExtra", conteudoMapped);
        }

        //
        // GET: /ConteudoExtra/Create
        public ActionResult Create()
        {
            ConteudosExtrasViewModel conteudo = new ConteudosExtrasViewModel();
            conteudo.ListaTurmas = _util.PreencherListaTurmas();
            return View("AdicionarConteudoExtra", conteudo);
        }

        //
        // POST: /ConteudoExtra/Create
        [HttpPost]
        public ActionResult Create(ConteudosExtrasViewModel conteudosExtras)
        {
            try
            {
                var usuarioLogado = Convert.ToInt32(Session["UsuarioId"].ToString());
                var professor = _professorApp.Recuperar(usuarioLogado);
                var turma = _turmaApp.Recuperar(conteudosExtras.TurmaSelecionada);

                conteudosExtras.DataHoraCriacao = DateTime.Now;

                var conteudoMapped = Mapper.Map<ConteudosExtrasViewModel, ConteudosExtras>(conteudosExtras);
                conteudoMapped.Professor = professor;
                conteudoMapped.TurmaPublicoAlvo = turma;

                var attempt = _conteudoExtraServico.IncluirConteudosExtras(conteudoMapped);
                if(attempt != null)
                {
                    return View("Index", "Home");
                }
                else
                {
                    ViewBag.AlertMessage = "Erro ao incluir conteúdo extra";
                    throw new NotImplementedException("Erro ao incluir conteúdo extra");
                }
            }
            catch
            {
                ViewBag.AlertMessage = "Erro ao incluir conteúdo extra";
                throw new NotImplementedException("Erro ao incluir conteúdo extra");
            }
        }

        //
        // GET: /ConteudoExtra/Edit/5
        public ActionResult Edit(int id)
        {
            var conteudoExtra = _conteudoExtraServico.Recuperar(id);
            var conteudoMapped = Mapper.Map<ConteudosExtras, ConteudosExtrasViewModel>(conteudoExtra);
            return View("AtualizarDadosConteudoExtra", conteudoMapped);
        }

        //
        // POST: /ConteudoExtra/Edit/5
        [HttpPost]
        public ActionResult Edit(ConteudosExtrasViewModel conteudosExtras)
        {
            try
            {
                var conteudoMapped = Mapper.Map<ConteudosExtrasViewModel, ConteudosExtras>(conteudosExtras);
                var attempt = _conteudoExtraServico.EditarDadosConteudosExtras(conteudoMapped);
                if (attempt)
                {
                    return View("Index", "Home");
                }
                else
                {
                    ViewBag.AlertMessage = "Erro ao editar conteúdo extra";
                    throw new NotImplementedException("Erro ao editar conteúdo extra");
                }
            }
            catch
            {
                ViewBag.AlertMessage = "Erro ao editar conteúdo extra";
                throw new NotImplementedException("Erro ao editar conteúdo extra");
            }
        }

        //
        // GET: /ConteudoExtra/Delete/5
        public ActionResult Delete(int id)
        {
            var conteudoExtra = _conteudoExtraServico.Recuperar(id);
            var conteudoMapped = Mapper.Map<ConteudosExtras, ConteudosExtrasViewModel>(conteudoExtra);
            return View("RemoverConteudoExtra", conteudoMapped);
        }

        //
        // POST: /ConteudoExtra/Delete/5
        [HttpPost]
        public ActionResult Delete(ConteudosExtrasViewModel conteudosExtras)
        {
            try
            {
                var conteudoMapped = Mapper.Map<ConteudosExtrasViewModel, ConteudosExtras>(conteudosExtras);
                var attempt = _conteudoExtraServico.Remover(conteudoMapped);
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

        public ActionResult RecuperarConteudosExtrasProfessor()
        {
            var professorId = Convert.ToInt32(Session["UsuarioId"]);
            var conteudos = _conteudoExtraServico.RecuperarConteudosExtrasProfessor(professorId);
            var conteudosMapped = Mapper.Map<IEnumerable<ConteudosExtras>, IEnumerable<ConteudosExtrasViewModel>>(conteudos);
            return View("RecuperarConteudosExtrasProfessor", conteudosMapped);
        }

        public ActionResult VisualizarConteudosExtrasMinhaTurma()
        {
            int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());

            var aluno = _alunoApp.Recuperar(idUsuario);
            var conteudoTurma = _conteudoExtraServico.RecuperarConteudosExtrasTurma(aluno.Turma.TurmaId);

            var conteudoViewModel = Mapper.Map<IEnumerable<ConteudosExtras>, IEnumerable<ConteudosExtrasViewModel>>(conteudoTurma);

            return View("VisualizarConteudosExtrasMinhaTurma", conteudoViewModel);
        }

        public ActionResult DetalhesConteudosExtrasMinhaTurma(int id)
        {
            var conteudo = _conteudoExtraServico.Recuperar(id);
            var conteudoViewModel = Mapper.Map<ConteudosExtras, ConteudosExtrasViewModel>(conteudo);

            return View("DetalhesConteudosExtrasMinhaTurma", conteudoViewModel);
        }

        [HttpGet]
        public ActionResult EscolhaTipoConteudoCadastro()
        {
            Utilizavel util = new Utilizavel();
            ViewBag.ListaConteudos = util.PreencherListsConteudosExtras();
            return View("FiltroTipoConteudo");
        }

        [HttpPost]
        public ActionResult EscolhaTipoConteudoCadastro(ConteudosExtrasViewModel conteudo)
        {
            if (conteudo.tipoConteudoEscolhido == 1)
            {
                return View("CadastroConteudoExtra");
            }
            else if (conteudo.tipoConteudoEscolhido == 2)
            {
                return View("AdicionarTrabalhosExtras");
            }
            else
            {
                Utilizavel util = new Utilizavel();
                ViewBag.ListaConteudos = util.PreencherListsConteudosExtras();
                return View("FiltroTipoConteudo");
            }

        }

        [HttpGet]
        public ActionResult EscolhaTipoConteudoCadastroConsulta()
        {
            Utilizavel util = new Utilizavel();
            ViewBag.ListaConteudos = util.PreencherListsConteudosExtras();
            return View("FiltroTipoConteudoConsulta");
        }

        [HttpPost]
        public ActionResult EscolhaTipoConteudoCadastroConsulta(ConteudosExtrasViewModel conteudo)
        {
            if (conteudo.tipoConteudoEscolhido == 1)
            {
                return View("CadastroConteudoExtra");
            }
            else if (conteudo.tipoConteudoEscolhido == 2)
            {
                return View("AdicionarTrabalhosExtras");
            }
            else
            {
                Utilizavel util = new Utilizavel();
                ViewBag.ListaConteudos = util.PreencherListsConteudosExtras();
                return View("FiltroTipoConteudoConsulta");
            }

        }
    }
}
