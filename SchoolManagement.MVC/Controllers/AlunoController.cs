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

        public AlunoController(IAlunoServico alunoApp, IUsuarioServico usuarioApp, ITurmaServico turmaApp, IDisciplinaServico disciplinaApp, IResultadosProvasServico resultadosProvasApp, IResponsavelServico responsavelApp)
        {
            _alunoApp = alunoApp;
            _usuarioApp = usuarioApp;
            _turmaApp = turmaApp;
            _disciplinaApp = disciplinaApp;
            _resultadosProvasApp = resultadosProvasApp;
            _responsavelApp = responsavelApp;
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
            var aluno = _alunoApp.Recuperar(id);
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
                
                var matricula = aluno.turmaEscolhida + "-" + DateTime.Now.Year + "" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                aluno.NumeroMatricula = matricula;

                if (!_usuarioApp.verificarCPFSendoUtilizado(aluno.Cpf))
                    throw new ArgumentNullException(aluno.Cpf, "Este CPF já está sendo utilizado.");
                if (!_usuarioApp.VerificarLoginExistente(aluno.UserLogin))
                    throw new ArgumentNullException(aluno.UserLogin, "Este nome de usuário já está sendo utilizado.");
                if (!_usuarioApp.VerificarRGSendoUtilizado(aluno.Rg))
                    throw new ArgumentNullException(aluno.Rg, "Este RG já está sendo utilizado.");

                var dia = aluno.DataNascimento.Day;
                var mes = aluno.DataNascimento.Month;
                var ano = aluno.DataNascimento.Year;

                DateTime date = Convert.ToDateTime(mes + "/" + dia + "/" + ano);
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

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                var mensagemErro = ex.Message.ToString();
                return RedirectToAction("Index", "Home", mensagemErro);
            }
        }

        //
        // GET: /Aluno/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var aluno = _alunoApp.Recuperar(id);
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
                _alunoApp.Atualizar(alunoDomain);

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home", aluno);
        }

        //
        // GET: /Aluno/Delete/5
        public ActionResult Delete(int id)
        {
            var aluno = _alunoApp.Recuperar(id);
            var alunoViewModel = Mapper.Map<Aluno, AlunoViewModel>(aluno);

            return View("DeleteAluno", alunoViewModel);
        }

        //
        // POST: /Aluno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var aluno = _alunoApp.Recuperar(id);

            var sucesso = _alunoApp.Remover(aluno);
            if (sucesso)
                return RedirectToAction("Index", "Aluno");
            else
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
            var aluno = _alunoApp.Recuperar(AlunoId);
            var alunosTurma = _alunoApp.RecuperarAlunosTurma(aluno.Turma.TurmaId);

            var alunosMapeados = Mapper.Map<IEnumerable<Aluno>, IEnumerable<AlunoViewModel>>(alunosTurma);

            return View("VerAlunosMinhaTurma", alunosMapeados);
        }


        public ActionResult VisualizarDisciplinasAluno()
        {
            //int AlunoId = Convert.ToInt32(Session["UsuarioId"].ToString());

            //var aluno = _alunoApp.Recuperar(AlunoId);
            //var disciplinaAluno = _alunoApp.RecuperarDisciplinasAluno(aluno.Turma.TurmaId);

            //var disciplinasMapeadas = Mapper.Map<IEnumerable<Disciplina>, IEnumerable<DisciplinaViewModel>>(disciplinaAluno);

            //return View("VisualizarDisciplinasAluno", disciplinasMapeadas);
            return null;
        }

<<<<<<< HEAD
        public ActionResult VerDisciplinasMinhaTurma()
        {
            int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());

            var aluno = _alunoApp.Recuperar(idUsuario);
            var alunosTurma = _disciplinaApp.RecuperarDisciplinasTurma(aluno.Turma.TurmaId);

            var disciplinasMapeados = Mapper.Map<IEnumerable<Disciplina>, IEnumerable<DisciplinaViewModel>>(alunosTurma);

            return View("VisualizarDisciplinasMinhaTurma", disciplinasMapeados);
        }

        public ActionResult VerMinhaTurma()
        {
            int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());

            var aluno = _alunoApp.Recuperar(idUsuario);
            var alunoTurma = _turmaApp.Recuperar(aluno.Turma.TurmaId);

            var turmaViewModel = Mapper.Map<Turma, TurmaViewModel>(alunoTurma);

            return View("VisualizaMinhaTurma", turmaViewModel);
        }

        [HttpGet]
        public ActionResult VisualizarMinhasNotas()
        {
            int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());

            var aluno = _alunoApp.Recuperar(idUsuario);
            var notas = _resultadosProvasApp.RecuperarNotasAluno(aluno.Id);
            var notasMapeadas = Mapper.Map<IEnumerable<ResultadosProvas>, IEnumerable<ResultadosProvasViewModel>>(notas);
            return View("VisualizarMinhasNotas", notasMapeadas);
        }
=======
>>>>>>> 47221abbbeff2cbed25e535b0cb20e2bfd2188b3

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

        public ActionResult DetalhesDisciplinasMinhaTurma(int id)
        {
            var disciplina = _disciplinaApp.Recuperar(id);
            var disciplinaViewModel = Mapper.Map<Disciplina, DisciplinaViewModel>(disciplina);

            return View("DetalhesDisciplinasMinhaTurma", disciplinaViewModel);
        }

        public ActionResult DetalhesMinhaTurma(int id)
        {
            var turma = _turmaApp.Recuperar(id);
            var turmaViewModel = Mapper.Map<Turma, TurmaViewModel>(turma);

            return View("DetalhesMinhaTurma", turmaViewModel);
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
            var aluno = _alunoApp.Recuperar(idUsuario);
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
