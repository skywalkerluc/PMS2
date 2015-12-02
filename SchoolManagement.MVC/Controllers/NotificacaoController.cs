using AutoMapper;
using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Servicos;
using SchoolManagement.MVC.ViewModels;
using SchoolManagement.MVC.ViewModels.FiltroViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.Controllers
{
    public class NotificacaoController : Controller
    {
        private readonly INotificacaoServico _notificacaoServico;
        private readonly IProfessorServico _professorServico;
        private readonly ITurmaServico _turmaServico;
        private readonly IUsuarioServico _usuarioServico;
        private readonly IAlunoServico _alunoServico;

        public NotificacaoController(INotificacaoServico notificacaoServico, IProfessorServico professorServico, ITurmaServico turmaServico, IUsuarioServico usuarioServico, IAlunoServico alunoServico)
        {
            _notificacaoServico = notificacaoServico;
            _professorServico = professorServico;
            _turmaServico = turmaServico;
            _usuarioServico = usuarioServico;
            _alunoServico = alunoServico;
        }

        //
        // GET: /Notificacao/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Notificacao/Details/5
        public ActionResult Details(int id)
        {
            var notf = _notificacaoServico.Recuperar(id);
            var notfViewModel = Mapper.Map<Notificacao, NotificacaoViewModel>(notf);

            return View("DetalhesNotificacao", notfViewModel);
        }

        //
        // GET: /Notificacao/Create
        public ActionResult Create()
        {
            var notf = new NotificacaoViewModel();
            PreencherListaProfessores(notf);
            PreencherListaTurmas(notf);
            return View("CadastrarNotificacao");
        }

        //
        // POST: /Notificacao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NotificacaoViewModel notificacao)
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());

                if (idUsuario != 0)
                {
                    var usuario = _usuarioServico.Recuperar(idUsuario);
                    var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);
                    notificacao.UsuarioCriacao = usuarioViewModel;
                }

                int idTurma = notificacao.turmaEscolhida;
                if (idTurma != 0)
                {
                    var turma = _turmaServico.Recuperar(idTurma);
                    var turmaViewModel = Mapper.Map<Turma, TurmaViewModel>(turma);

                    notificacao.TurmaPublicoAlvo = turmaViewModel;
                }

                notificacao.DataCriacao = DateTime.Now;
                var notifDomain = Mapper.Map<NotificacaoViewModel, Notificacao>(notificacao);
                
                _notificacaoServico.CriarNotificacao(notifDomain);

                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                var mensagemErro = ex.Message.ToString();
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Notificacao/Edit/5
        public ActionResult Edit(int id)
        {
            var notif = _notificacaoServico.Recuperar(id);
            var notifVM = Mapper.Map<Notificacao, NotificacaoViewModel>(notif);
            Session["NotificacaoId"] = id;

            return View("EditarNotificacao", notifVM);
        }

        //
        // POST: /Notificacao/Edit/5
        [HttpPost]
        public ActionResult Edit(NotificacaoViewModel notificacao)
        {
            try
            {
                notificacao.NotificacaoId = (int)Session["NotificacaoId"];
                var notfiVM = Mapper.Map<NotificacaoViewModel, Notificacao>(notificacao);
                _notificacaoServico.EditarNotificacao(notfiVM);

                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                var mensagemErro = ex.Message.ToString();
                return RedirectToAction("Index", "Home", mensagemErro);
            }
        }

        //
        // GET: /Notificacao/Delete/5
        public ActionResult Delete(int id)
        {
            var notif = _notificacaoServico.Recuperar(id);
            var notifVM = Mapper.Map<Notificacao, NotificacaoViewModel>(notif);

            return View("ExcluirNotificacao", notifVM);
        }

        //
        // POST: /Notificacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var notf = _notificacaoServico.Recuperar(id);
            _notificacaoServico.Remover(notf);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public void PreencherListaProfessores(NotificacaoViewModel notif)
        {
            List<SelectListItem> listaProfessor = new List<SelectListItem>();
            var enumProfessor = _professorServico.RecuperarTodos();

            foreach (var disc in enumProfessor)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = disc.Id.ToString(),
                    Text = disc.Nome
                };
                listaProfessor.Add(listItem);
            }

            notif.ListaProfessores = listaProfessor;
            ViewBag.ListaProfessores = notif.ListaProfessores;
        }

        [HttpGet]
        public void PreencherListaTurmas(NotificacaoViewModel notif)
        {
            List<SelectListItem> listaTurmas = new List<SelectListItem>();
            var enumTurma = _turmaServico.RecuperarTodos();

            foreach (var disc in enumTurma)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = disc.TurmaId.ToString(),
                    Text = disc.Descricao
                };
                listaTurmas.Add(listItem);
            }

            notif.ListaTurmas = listaTurmas;
            ViewBag.ListaTurmas = notif.ListaTurmas;
        }

        //GET
        [HttpGet]
        public ActionResult RecuperarNotificacao()
        {
            var notf = new NotificacaoViewModel();
            PreencherListaTurmas(notf);
            return View("FiltroParaConsultaDeNotificacao");
        }

        //POST
        [HttpPost]
        public ActionResult RecuperarNotificacao(NotificacaoViewModel notificacaoFiltro)
        {
            var assunto = notificacaoFiltro.Assunto;
            var turma = notificacaoFiltro.turmaEscolhida;

            var notificacao1 = _notificacaoServico.BuscarNotificacaoPorAssunto(assunto, turma);
            var notificacao2 = Mapper.Map<IEnumerable<Notificacao>, IEnumerable<NotificacaoViewModel>>(notificacao1);

            return View("ResultadoConsultaNotificacao", notificacao2.ToList());
        }

        public ActionResult VisualizarTodasNotificacoes()
        {
            var notf = _notificacaoServico.RecuperarTodos();
            var notfMapped = Mapper.Map<IEnumerable<Notificacao>, IEnumerable<NotificacaoViewModel>>(notf);
            return View("VisualizarTodasNotificacoes", notfMapped);
        }

        public ActionResult VisualizarNotificacoesMinhaTurma()
        {
            int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());

            var aluno = _alunoServico.Recuperar(idUsuario);
            var notif = _notificacaoServico.VisualizarNotificacoesMinhaTurma(aluno.Turma.TurmaId);
            var notifMapped = Mapper.Map<IEnumerable<Notificacao>, IEnumerable<NotificacaoViewModel>>(notif);
            return View("VisualizarNotificacoesMinhaTurma", notifMapped);
        }
    }
}
