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
    public class EventoController : Controller
    {
        private readonly IEventoServico _eventoServico;
        private readonly IFuncionarioServico _funcionarioServico;

        public EventoController(IEventoServico eventoServico, IFuncionarioServico funcionarioServico)
        {
            _eventoServico = eventoServico;
            _funcionarioServico = funcionarioServico;
        }

        //
        // GET: /Evento/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Evento/Details/5
        public ActionResult Details(int id)
        {
            var evento = _eventoServico.Recuperar(id);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            return View("DetalhesEvento", eventoViewModel);
        }

        public ActionResult DetailsOutroUsuario(int id)
        {
            var evento = _eventoServico.Recuperar(id);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            return View("DetalhesEventoOutroUsuario", eventoViewModel);
        }

        //
        // GET: /Evento/Create
        public ActionResult Create()
        {
            var evento = new EventoViewModel();
            PreencherListaFuncionario(evento);
            return View("CadastrarEventos");
        }

        //
        // POST: /Evento/Create
        [HttpPost]
        public ActionResult Create(EventoViewModel evento)
        {
            try
            {
                evento.DataEvento = DateTime.Now.Date;
                evento.DataCriacao = DateTime.Now.Date;

                List<FuncionarioViewModel> ListaFuncionarios = new List<FuncionarioViewModel>();
                if (evento.funcionariocomboselected != 0)
                {
                    var funcionario = _funcionarioServico.Recuperar(evento.funcionariocomboselected);
                    var funcionarioViewModel = Mapper.Map<Funcionario, FuncionarioViewModel>(funcionario);

                    ListaFuncionarios.Add(funcionarioViewModel);
                }

                evento.FuncionarioResponsavel = ListaFuncionarios;

                var eventoDomain = Mapper.Map<EventoViewModel, Evento>(evento);
                _eventoServico.IncluirEvento(eventoDomain);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                var mensagemErro = ex.Message.ToString();
                return RedirectToAction("Index", "Home", mensagemErro);
            }
        }

        //
        // GET: /Evento/Edit/5
        public ActionResult Edit(int id)
        {
            var evento = _eventoServico.Recuperar(id);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            return View("EditarEvento", eventoViewModel);
        }

        //
        // POST: /Evento/Edit/5
        [HttpPost]
        public ActionResult Edit(EventoViewModel evento)
        {
            if (ModelState.IsValid)
            {
                var eventoDomain = Mapper.Map<EventoViewModel, Evento>(evento);
                _eventoServico.Atualizar(eventoDomain);

                return RedirectToAction("Index", "Home");
            }
            return View("Index", "Home", evento);
        }

        //
        // GET: /Evento/Delete/5
        public ActionResult Delete(int id)
        {
            var evento = _eventoServico.Recuperar(id);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            return View("ExcluirEvento", eventoViewModel);
        }

        //
        // POST: /Evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var evento = _eventoServico.Recuperar(id);

            var eventoSelecionado = Mapper.Map<Evento, Evento>(evento);
            _eventoServico.Remover(eventoSelecionado);

            return RedirectToAction("Index", "Home");
        }

        private void PreencherListaFuncionario(EventoViewModel evento)
        {
            List<SelectListItem> listaFuncionarios = new List<SelectListItem>();
            var enumFuncionarios = _funcionarioServico.RecuperarTodos();

            foreach (var disc in enumFuncionarios)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = disc.Id.ToString(),
                    Text = disc.Nome
                };
                listaFuncionarios.Add(listItem);
            }

            evento.ListaFuncionarios = listaFuncionarios;
            ViewBag.ListaProfessores = evento.ListaFuncionarios;
        }

        //GET
        [HttpGet]
        public ActionResult RecuperarEventoPorDescricao()
        {
            var evento = new EventoViewModel();
            PreencherListaEventos(evento);
            return View("FiltroParaConsultaDeEventos");
        }

        //POST
        [HttpPost]
        public ActionResult RecuperarEventoPorDescricao(EventoViewModel evento)
        {
            var eventoDescricao = _eventoServico.Recuperar(Convert.ToInt32(evento.eventoEscolhido));
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(eventoDescricao);

            var descricao = eventoViewModel.Descricao;


            var evento1 = _eventoServico.BuscarEventoPorDescricao(descricao);
            var evento2 = Mapper.Map<IEnumerable<Evento>, IEnumerable<EventoViewModel>>(evento1);

            return View("ResultaConsultaEventos", evento2.ToList());
        }

        //GET
        [HttpGet]
        public ActionResult RecuperarEventoPorDescricaoOutroUsuario()
        {
            var evento = new EventoViewModel();
            PreencherListaEventos(evento);
            return View("FiltroParaConsultaDeEventosOutroUsuario");
        }

        //POST
        [HttpPost]
        public ActionResult RecuperarEventoPorDescricaoOutroUsuario(EventoViewModel evento)
        {
            var eventoDescricao = _eventoServico.Recuperar(Convert.ToInt32(evento.eventoEscolhido));
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(eventoDescricao);

            var descricao = eventoViewModel.Descricao;


            var evento1 = _eventoServico.BuscarEventoPorDescricao(descricao);
            var evento2 = Mapper.Map<IEnumerable<Evento>, IEnumerable<EventoViewModel>>(evento1);

            return View("ResultaConsultaEventosOutroUsuario", evento2.ToList());
        }

        private void PreencherListaEventos(EventoViewModel evento)
        {
            List<SelectListItem> listaEventos = new List<SelectListItem>();
            var enumEventos = _eventoServico.RecuperarTodos();

            foreach (var disc in enumEventos)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = disc.EventoId.ToString(),
                    Text = disc.Descricao
                };
                listaEventos.Add(listItem);
            }

            evento.ListaEventos = listaEventos;
            ViewBag.ListaEventos = evento.ListaEventos;
        }

        public ActionResult VoltarDetalhesOutroUsuario()
        {
            var evento = new EventoViewModel();
            PreencherListaEventos(evento);
            return View("FiltroParaConsultaDeEventosOutroUsuario");
        }

        public ActionResult VisualizarTodosEventos()
        {
            var eventos = _eventoServico.RecuperarTodos();
            var eventoMapped = Mapper.Map<IEnumerable<Evento>, IEnumerable<EventoViewModel>>(eventos);
            return View("VisualizarTodosEventos", eventoMapped);
        }

        public ActionResult VisualizarEventosFuturos()
        {
            var eventos = _eventoServico.RecuperarEventosFuturos();
            var eventoMapped = Mapper.Map<IEnumerable<Evento>, IEnumerable<EventoViewModel>>(eventos);
            return View("VisualizarEventosFuturos", eventoMapped);
        }

        public ActionResult CreateEventoProfessor()
        {
            var evento = new EventoViewModel();
            PreencherListaFuncionario(evento);
            return View("CadastrarEventosProfessor");
        }

        //
        // POST: /Evento/Create
        [HttpPost]
        public ActionResult CreateEventoProfessor(EventoViewModel evento)
        {
            try
            {
                evento.DataEvento = DateTime.Now.Date;
                evento.DataCriacao = DateTime.Now.Date;

                int ProfessorId = (int)Session["UsuarioId"];

                List<FuncionarioViewModel> ListaFuncionarios = new List<FuncionarioViewModel>();

                var funcionario = _funcionarioServico.Recuperar(ProfessorId);
                var funcionarioViewModel = Mapper.Map<Funcionario, FuncionarioViewModel>(funcionario);

                ListaFuncionarios.Add(funcionarioViewModel);

                evento.FuncionarioResponsavel = ListaFuncionarios;

                var eventoDomain = Mapper.Map<EventoViewModel, Evento>(evento);
                _eventoServico.IncluirEvento(eventoDomain);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                var mensagemErro = ex.Message.ToString();
                return RedirectToAction("Index", "Home", mensagemErro);
            }
        }

    }
}
