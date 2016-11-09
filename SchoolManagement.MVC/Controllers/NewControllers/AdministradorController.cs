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

namespace SchoolManagement.MVC.Controllers.NewControllers
{
    public class AdministradorController : Controller
    {
        private IUsuarioServico _usuarioApp;
        private ITurmaServico _turmaApp;
        private Util util;

        public AdministradorController(IUsuarioServico usuarioApp, ITurmaServico turmaApp)
        {
            _usuarioApp = usuarioApp;
            _turmaApp = turmaApp;
        }

        // GET: Administrador
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Aluno/Create
        [HttpGet]
        public ActionResult CadastrarAluno(string mensageAlert)
        {
            ViewBag.AlertMessage = mensageAlert;
            var aluno = new AlunoViewModel();
            var filtro = new FiltroAluno();
            PreencherListaTurma(aluno, filtro);
            return View("AdicionarAluno");
        }

        //
        // POST: /Aluno/Create
        /// <summary>
        /// Quando eu desenvolvi isso, só eu e Deus sabíamos como funcionava. Agora, só ele sabe.
        /// </summary>
        /// <param name="aluno"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarAluno(AlunoViewModel aluno)
        {
            try
            {
                ConfigurarAlunoDefault(ref aluno);
                string errorMessage;
                if (!ValidarCadastro(aluno, out errorMessage))
                {
                    return RedirectToAction("Create", "Aluno", new { errorMessage });
                }
                aluno.DataNascimento = util.TratarData(aluno.DataNascimento.Day, aluno.DataNascimento.Month, aluno.DataNascimento.Year);
                var alunoDomain = Mapper.Map<AlunoViewModel, Aluno>(aluno);


                TurmaViewModel turmaT = new TurmaViewModel();
                turmaT.TurmaId = aluno.turmaEscolhida;
                var turmaRecuperada = _turmaApp.Recuperar(turmaT.TurmaId);
                List<Aluno> ListaAlunos = new List<Aluno>();
                ListaAlunos.Add(alunoDomain);
                turmaRecuperada.Alunos = ListaAlunos;
                _turmaApp.Atualizar(turmaRecuperada);

                //ViewBag.TituloMensagem = "Sucesso";
                //ViewBag.MensagemErro = "Aluno cadastrado com sucesso!";

                ViewBag.AlertMessage = "Aluno cadastrado com sucesso!";
                var mensageAlertSucesso = ViewBag.AlertMessage;
                return RedirectToAction("Index", "Home", new { mensageAlertSucesso });
            }
            catch (Exception ex)
            {
                var mensagemErro = ex.Message.ToString();
                ViewBag.AlertMessage = mensagemErro;
                var mensageAlert = ViewBag.AlertMessage;
                return RedirectToAction("Create", "Aluno", new { mensageAlert });
            }
        }

        private bool ValidarCadastro(UsuarioViewModel usuario, out string messageAlert)
        {
            if (!_usuarioApp.verificarCPFSendoUtilizado(usuario.Cpf))
            {
                messageAlert = "Este CPF já está sendo utilizado.";
                return false;
            }
            if (!_usuarioApp.VerificarLoginExistente(usuario.UserLogin))
            {
                messageAlert = "Este nome de usuário já está sendo utilizado.";
                return false;
            }
            if (!_usuarioApp.VerificarRGSendoUtilizado(usuario.Rg))
            {
                messageAlert = "Este RG já está sendo utilizado.";
                return false;
            }
            messageAlert = string.Empty;
            return true;
        }

        private void ConfigurarAlunoDefault(ref AlunoViewModel aluno)
        {
            aluno.DataCadastro = DateTime.Now;
            aluno.indicadorAcesso = 2;
            aluno.Endereco.Pais = "Brasil";
        }

        private void PreencherListaTurma(AlunoViewModel aluno, FiltroAluno filtroAluno)
        {
            List<SelectListItem> listaTurmas = new List<SelectListItem>();
            var enumTurmas = _turmaApp.RecuperarTodos();

            SelectListItem itemBranco = new SelectListItem()
            {
                Text = string.Empty,
                Value = "0",
                Selected = true
            };
            listaTurmas.Add(itemBranco);

            foreach (var disc in enumTurmas)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = disc.TurmaId.ToString(),
                    Text = String.Concat(disc.Descricao, " (", this.RecuperarValorHorarioTurma(disc.HorariosTurmaId), ")")
                };
                listaTurmas.Add(listItem);
            }
            aluno.ListaTurmas = listaTurmas;
            filtroAluno.Turmas = listaTurmas;
            ViewBag.ListaTurmas = listaTurmas;

        }

    }
}