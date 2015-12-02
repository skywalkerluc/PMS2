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
    public class CustomActionsController : Controller
    {
        private readonly INotificacaoServico _notificacaoServico;
        private readonly IEventoServico _eventoServico;
        private readonly IDisciplinaServico _disciplinaServico;
        private readonly ITrabalhosExtrasServico _trabalhosExtrasServico;
        private readonly IProvaServico _provaServico;

        public CustomActionsController(INotificacaoServico notificacaoServico,
            IEventoServico eventoServico)
        {
            _notificacaoServico = notificacaoServico;
            _eventoServico = eventoServico;
        }

        #region Provas
        [HttpGet]
        public ActionResult AdicionarProva()
        {
            var prova = new ProvaViewModel();
            return View("AdicionarProva", prova);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarProva(ProvaViewModel prova, int UsuarioId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var provaDomain = Mapper.Map<ProvaViewModel, Prova>(prova);
                    //_provaServico.Incluir(provaDomain);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    var mensagemErro = ex.Message.ToString();
                    return RedirectToAction("Index", mensagemErro);
                }
            }
            return View("CriarEvento", prova);
        }

        #endregion

        #region Eventos
        [HttpGet]
        public ActionResult CriarEvento()
        {
            var evento = new EventoViewModel();
            return View("CriarEvento", evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarEvento(EventoViewModel evento, int UsuarioId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var eventoDomain = Mapper.Map<EventoViewModel, Evento>(evento);
                    _eventoServico.Incluir(eventoDomain);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    var mensagemErro = ex.Message.ToString();
                    return RedirectToAction("Index", mensagemErro);
                }
            }
            return View("CriarEvento", evento);
        }

        #endregion

        #region Notificações
        [HttpGet]
        public ActionResult CriarNotificacao()
        {
            var notificacao = new NotificacaoViewModel();
            return View("CriarNotificacao");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarNotificacao(NotificacaoViewModel notificacao, int idUsuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var notificacaoDomain = Mapper.Map<NotificacaoViewModel, Notificacao>(notificacao);
                    _notificacaoServico.Incluir(notificacaoDomain);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    var mensagemErro = ex.Message.ToString();
                    return RedirectToAction("Index", mensagemErro);
                }
            }
            return View("CriarNotificacao", notificacao);
        }
        #endregion

        #region Disciplina
        [HttpGet]
        public ActionResult AdicionarDisciplina()
        {
            var disciplina = new DisciplinaViewModel();
            return View("AdicionarDisciplina");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarDisciplina(DisciplinaViewModel disciplina, int idUsuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var disciplinaDomain = Mapper.Map<DisciplinaViewModel, Disciplina>(disciplina);
                    _disciplinaServico.Incluir(disciplinaDomain);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    var mensagemErro = ex.Message.ToString();
                    return RedirectToAction("Index", mensagemErro);
                }
            }
            return View("AdicionarDisciplina", disciplina);
        }
        #endregion


    }
}
