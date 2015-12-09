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
    public class EventoController : Controller
    {
        private readonly IEventoServico _eventoServico;
        private readonly IFuncionarioServico _funcionarioServico;
        private readonly IUsuarioServico _usuarioServico;
        private readonly INotificacaoServico _notificacaoServico;

        public EventoController(IEventoServico eventoServico, IFuncionarioServico funcionarioServico, IUsuarioServico usuarioServico, INotificacaoServico notifServico)
        {
            _eventoServico = eventoServico;
            _funcionarioServico = funcionarioServico;
            _usuarioServico = usuarioServico;
            _notificacaoServico = notifServico;
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

            int idUsuario = Convert.ToInt32(Session["IdentidicadorAcessoUsuário"].ToString());

            if (idUsuario == 4)
            {
                return View("DetalhesEventoProfessor", eventoViewModel);
            }
            else
            {
                return View("DetalhesEvento", eventoViewModel);
            }

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
                evento.DataCriacao = DateTime.Now.Date;

                if (evento.funcionariocomboselected != 0)
                {
                    var funcionario = _funcionarioServico.Recuperar(evento.funcionariocomboselected);
                    var funcionarioViewModel = Mapper.Map<Funcionario, FuncionarioViewModel>(funcionario);

                    evento.FuncionarioResponsavel = funcionarioViewModel;
                }

                

                var eventoDomain = Mapper.Map<EventoViewModel, Evento>(evento);
                var attmpt = _eventoServico.IncluirEvento(eventoDomain);

                if (attmpt != null)
                {
                    var usuarioCriacao = Mapper.Map<Usuario, UsuarioViewModel>(_usuarioServico.Recuperar((int)Session["UsuarioId"]));
                    var notif = new NotificacaoViewModel()
                    {
                        Assunto = "Um novo evento foi adicionado",
                        DataCriacao = DateTime.Now,
                        Descricao = "Um novo evento foi adicionado por: " + usuarioCriacao.Nome + ".", 
                        UsuarioCriacao = Mapper.Map<Usuario, UsuarioViewModel>(_usuarioServico.Recuperar((int)Session["UsuarioId"])),
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
                var eventoDomain = Mapper.Map<EventoViewModel, Evento>(evento);
                _eventoServico.AtualizarDadosEvento(eventoDomain);

                return RedirectToAction("Index", "Home");
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

            SelectListItem itemBranco = new SelectListItem()
            {
                Value = "0",
                Text = string.Empty
            };
            listaFuncionarios.Add(itemBranco);

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
            if (evento.eventoEscolhido == null)
            {
                var evento2 = new EventoViewModel();
                PreencherListaEventos(evento2);
                return View("FiltroParaConsultaDeEventosOutroUsuario");
            }
            else
            {
                var eventoDescricao = _eventoServico.Recuperar(Convert.ToInt32(evento.eventoEscolhido));
                var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(eventoDescricao);

                var descricao = eventoViewModel.Descricao;


                var evento1 = _eventoServico.BuscarEventoPorDescricao(descricao);
                var evento2 = Mapper.Map<IEnumerable<Evento>, IEnumerable<EventoViewModel>>(evento1);

                return View("ResultaConsultaEventosOutroUsuario", evento2.ToList());
            }
            
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


                var funcionario = _funcionarioServico.Recuperar(ProfessorId);
                var funcionarioViewModel = Mapper.Map<Funcionario, FuncionarioViewModel>(funcionario);

                evento.FuncionarioResponsavel = funcionarioViewModel;

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
