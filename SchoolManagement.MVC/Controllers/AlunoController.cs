using AutoMapper;
using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Servicos;
using SchoolManagement.MVC.ViewModels;
using SchoolManagement.MVC.ViewModels.FiltroViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.Controllers
{
    public class AlunoController : Controller
    {

        private readonly IAlunoServico _alunoApp;
        private readonly IUsuarioServico _usuarioApp;
        private readonly ITurmaServico _turmaApp;
        private readonly IDisciplinaServico _disciplinaApp;
        private readonly IResultadosProvasServico _resultadosProvasApp;
        private readonly IResponsavelServico _responsavelApp;
        private readonly IProvaServico _provalApp;

        public AlunoController(IAlunoServico alunoApp, IUsuarioServico usuarioApp, ITurmaServico turmaApp, IDisciplinaServico disciplinaApp, IResultadosProvasServico resultadosProvasApp, IResponsavelServico responsavelApp, IProvaServico provalApp)
        {
            _alunoApp = alunoApp;
            _usuarioApp = usuarioApp;
            _turmaApp = turmaApp;
            _disciplinaApp = disciplinaApp;
            _resultadosProvasApp = resultadosProvasApp;
            _responsavelApp = responsavelApp;
            _provalApp = provalApp;
        }

        //
        // GET: /Aluno/
        public ActionResult Index()
        {
            try
            {
                var enumeradorAlunos = _alunoApp.RecuperarTodos();
                var alunoViewModel = Mapper.Map<IEnumerable<Aluno>, IEnumerable<AlunoViewModel>>(enumeradorAlunos);
                return View("ResultadoConsultaAlunos", alunoViewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Create");
            }
        }

        //
        // GET: /Aluno/Details/5
        public ActionResult Details(int id)
        {
            var aluno = _alunoApp.RecuperarDadosAluno(id);
            var alunoViewModel = Mapper.Map<Aluno, AlunoViewModel>(aluno);

            return View("DetalhesAluno", alunoViewModel);
        }

        //
        // GET: /Aluno/Create
        [HttpGet]
        public ActionResult Create()
        {
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
        public ActionResult Create(AlunoViewModel aluno)
        {
            try
            {
                aluno.DataCadastro = DateTime.Now;
                aluno.indicadorAcesso = 2;
                aluno.Endereco.Pais = "Brasil";


                if (!_usuarioApp.verificarCPFSendoUtilizado(aluno.Cpf))
                {
                    ViewBag.AlertMessage = "Este CPF já está sendo utilizado.";
                    throw new ArgumentNullException(aluno.Cpf, "Este CPF já está sendo utilizado.");
                }
                if (!_usuarioApp.VerificarLoginExistente(aluno.UserLogin))
                {
                    ViewBag.AlertMessage = "Este nome de usuário já está sendo utilizado.";
                    throw new ArgumentNullException(aluno.UserLogin, "Este nome de usuário já está sendo utilizado.");
                }
                if (!_usuarioApp.VerificarRGSendoUtilizado(aluno.Rg))
                {
                    ViewBag.AlertMessage = "Este RG já está sendo utilizado.";
                    throw new ArgumentNullException(aluno.Rg, "Este RG já está sendo utilizado.");
                }

                var dia = aluno.DataNascimento.Day;
                var mes = aluno.DataNascimento.Month;
                var ano = aluno.DataNascimento.Year;

                DateTime date = Convert.ToDateTime(dia + "/" + mes + "/" + ano);
                var data = date.ToString("dd/MM/yyyy");
                aluno.DataNascimento = Convert.ToDateTime(data);
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

                ViewData["AlertMensage"] = "Aluno cadastrado com sucesso!";
                var mensageAlert = ViewData["AlertMensage"];
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                var mensagemErro = ex.Message.ToString();
                ViewBag.AlertMessage = mensagemErro;
                throw new NotImplementedException(mensagemErro);
            }
        }

        //
        // GET: /Aluno/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var aluno = _alunoApp.RecuperarDadosAluno(id);
            var alunoViewModel = Mapper.Map<Aluno, AlunoViewModel>(aluno);
            return View("EditarAluno", alunoViewModel);
        }

        //
        // POST: /Aluno/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AlunoViewModel aluno)
        {
            if (ModelState.IsValid)
            {
                var alunoDomain = Mapper.Map<AlunoViewModel, Aluno>(aluno);
                _alunoApp.AtualizarDadosAluno(alunoDomain);

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home", aluno);
        }

        //
        // GET: /Aluno/Delete/5
        public ActionResult Delete(int id)
        {
            var aluno = _alunoApp.RecuperarDadosAluno(id);
            var alunoViewModel = Mapper.Map<Aluno, AlunoViewModel>(aluno);

            return View("DeleteAluno", alunoViewModel);
        }

        //
        // POST: /Aluno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var sucesso = _alunoApp.RemoverAluno(id);
            if (sucesso)
                return RedirectToAction("Index", "Aluno");
            else
                ViewBag.AlertMessage = "Erro ao tentar excluir um aluno";
                throw new NotImplementedException("Erro ao tentar excluir um aluno");
        }

        [HttpGet]
        public ActionResult ExibirNotificacoes(AlunoViewModel aluno)
        {
            try
            {
                var enumNotificacoes = _usuarioApp.ExibirNotificacoesUsuario(aluno.indicadorAcesso, aluno.Id);
                var notificacoesViewModel = Mapper.Map<IEnumerable<Notificacao>, IEnumerable<NotificacaoViewModel>>(enumNotificacoes);
                return View("ExibirNotificacoes", notificacoesViewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("DashboardSecundaria");
            }
        }

        //GET
        [HttpGet]
        public ActionResult RecuperarAlunosPorNome()
        {
            var aluno = new AlunoViewModel();
            var filtro = new FiltroAluno();
            PreencherListaTurma(aluno, filtro);
            return View("filtroconsultaalunos");
        }

        //POST
        [HttpPost]
        public ActionResult RecuperarAlunosPorNome(FiltroAluno alunoFiltro)
        {
            if (alunoFiltro.NomeAluno == null || alunoFiltro.NomeAluno == string.Empty)
            {
                if (alunoFiltro.turmaSelecionada == 0)
                {
                    var listaAlunos = _alunoApp.RecuperarTodos();
                    var alunosMapeados = Mapper.Map<IEnumerable<Aluno>, IEnumerable<AlunoViewModel>>(listaAlunos);
                    return View("ResultadoConsultaAlunos", alunosMapeados.ToList());
                }
            }

            if (alunoFiltro.turmaSelecionada != 0)
            {
                var turma = _turmaApp.Recuperar(alunoFiltro.turmaSelecionada);
                var turmaViewModel = Mapper.Map<Turma, TurmaViewModel>(turma);

                alunoFiltro.TurmaId = turmaViewModel.TurmaId;
            }

            var aluno2 = _alunoApp.FiltrarAluno(alunoFiltro.NomeAluno, alunoFiltro.TurmaId);
            var aluno3 = Mapper.Map<IEnumerable<Aluno>, IEnumerable<AlunoViewModel>>(aluno2);

            return View("ResultadoConsultaAlunos", aluno3.ToList());
        }

        public ActionResult VerAlunosMinhaTurma(int AlunoId)
        {
            var aluno = _alunoApp.RecuperarDadosAluno(AlunoId);
            var alunosTurma = _alunoApp.RecuperarAlunosTurma(aluno.Turma.TurmaId);

            var alunosMapeados = Mapper.Map<IEnumerable<Aluno>, IEnumerable<AlunoViewModel>>(alunosTurma);

            return View("VerAlunosMinhaTurma", alunosMapeados);
        }


        [HttpGet]
        public ActionResult VisualizarMinhasNotas()
        {
            int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());

            var aluno = _alunoApp.RecuperarDadosAluno(idUsuario);
            var notas = _resultadosProvasApp.RecuperarNotasAluno(aluno.Id);
            var notasMapeadas = Mapper.Map<IEnumerable<ResultadosProvas>, IEnumerable<ResultadosProvasViewModel>>(notas);
            return View("VisualizarMinhasNotas", notasMapeadas);
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

        private string RecuperarValorHorarioTurma(int value)
        {
            string descricaoRetorno = string.Empty;
            switch (value)
            {
                case 1:
                    descricaoRetorno = "Manhã";
                    break;
                case 2:
                    descricaoRetorno = "Tarde";
                    break;
                case 3:
                    descricaoRetorno = "Noite";
                    break;
                default:
                    descricaoRetorno = string.Empty;
                    break;
            }
            return descricaoRetorno;
        }

        public ActionResult VisualizarTodosAlunos()
        {
            var alunos = _alunoApp.RecuperarTodos();
            var alunosMapped = Mapper.Map<IEnumerable<Aluno>, IEnumerable<AlunoViewModel>>(alunos);

            return View("VisualizarTodosAlunos", alunosMapped);
        }

        public ActionResult VisualizarDadosMeuResponsavel()
        {
            List<ResponsavelViewModel> ListaRetorno = new List<ResponsavelViewModel>();
            int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());
            var aluno = _alunoApp.RecuperarDadosAluno(idUsuario);
            var alunoMapped = Mapper.Map<Aluno, AlunoViewModel>(aluno);
            if (alunoMapped != null)
            {
                var responsaveis = alunoMapped.Responsaveis.ToList();
                ListaRetorno = responsaveis;

                if (ListaRetorno.Count == 1)
                {
                    return View("VisualizarDadosMeuResponsavel", ListaRetorno.First());
                }
                else
                {
                    return View("VisualizarMeusResponsaveis", ListaRetorno);
                }
            }
            else
            {
                throw new NotImplementedException("Erro!");
            }
        }

    }
}
