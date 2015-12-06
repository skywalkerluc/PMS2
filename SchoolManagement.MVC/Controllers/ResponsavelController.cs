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
    public class ResponsavelController : Controller
    {
        private readonly IResponsavelServico _responsavelApp;
        private readonly IUsuarioServico _usuarioApp;
        private readonly IAlunoServico _alunoApp;

        private Utilizavel utilizavel;

        public ResponsavelController(IAlunoServico alunoApp, IResponsavelServico responsavelApp, IUsuarioServico usuarioApp)
        {
            _responsavelApp = responsavelApp;
            _usuarioApp = usuarioApp;
            _alunoApp = alunoApp;

            utilizavel = new Utilizavel(null, null, null, null, alunoApp);
        }

        //
        // GET: /Responsavel/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Responsavel/Details/5
        public ActionResult Details(int id)
        {
            var responsavel = _responsavelApp.Recuperar(id);
            var responsavelViewModel = Mapper.Map<Responsavel, ResponsavelViewModel>(responsavel);

            return View("DetalhesResponsavel", responsavelViewModel);
        }

        //
        // GET: /Responsavel/Create
        public ActionResult Create()
        {
            var responsavel = new ResponsavelViewModel();

            FiltroResponsavel filtro = new FiltroResponsavel();
            filtro.Alunos = utilizavel.PreencherListaAlunos();

            ViewBag.ListaAlunos = filtro.Alunos;

            return View("CadastrarResponsavel");
        }

        //
        // POST: /Responsavel/Create
        [HttpPost]
        public ActionResult Create(ResponsavelViewModel responsavel)
        {
            try
            {
                //montando data
                //var dia = responsavel.DataNascimento.Day;
                //var mes = responsavel.DataNascimento.Month;
                //var ano = responsavel.DataNascimento.Year;

                //DateTime date = Convert.ToDateTime(mes + "/" + dia + "/" + ano);
                //var data = date.ToString("dd/MM/yyyy");
                ////fim montar data

                //responsavel.DataNascimento = Convert.ToDateTime(data);

                responsavel.indicadorAcesso = 5;

                if (!_usuarioApp.verificarCPFSendoUtilizado(responsavel.Cpf))
                    throw new ArgumentNullException("Este CPF já está sendo utilizado.");
                if (!_usuarioApp.VerificarLoginExistente(responsavel.UserLogin))
                    throw new ArgumentNullException("Este nome de usuário já está sendo utilizado.");
                if (!_usuarioApp.VerificarRGSendoUtilizado(responsavel.Rg))
                    throw new ArgumentNullException("Este RG já está sendo utilizado.");

                List<Aluno> ListaAlunoDomain = new List<Aluno>();
                List<Responsavel> ListaResponsavel = new List<Responsavel>();

                var responsavelDomain = Mapper.Map<ResponsavelViewModel, Responsavel>(responsavel);
                ListaResponsavel.Add(responsavelDomain);

                if (responsavel.alunosselecionados.Count > 0)
                {
                    foreach (var aluno in responsavel.alunosselecionados)
                    {

                        var alunoRecuperado = _alunoApp.Recuperar(aluno);
                        ListaAlunoDomain.Add(alunoRecuperado);
                    }

                    foreach (var aluno in ListaAlunoDomain)
                    {
                        aluno.Responsaveis = ListaResponsavel;
                        _alunoApp.Atualizar(aluno);
                    }
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
        // GET: /Responsavel/Edit/5
        public ActionResult Edit(int id)
        {
            var responsavel = _responsavelApp.Recuperar(id);
            var responsavelViewModel = Mapper.Map<Responsavel, ResponsavelViewModel>(responsavel);
            return View("EditarResponsavel", responsavelViewModel);
        }

        //
        // POST: /Responsavel/Edit/5
        [HttpPost]
        public ActionResult Edit(ResponsavelViewModel responsavel)
        {
            if (ModelState.IsValid)
            {
                var responsavelDomain = Mapper.Map<ResponsavelViewModel, Responsavel>(responsavel);
                _responsavelApp.Atualizar(responsavelDomain);

                return RedirectToAction("Index", "Home");
            }
            return View("Index", "Home", responsavel);
        }

        //
        // GET: /Responsavel/Delete/5
        public ActionResult Delete(int id)
        {
            var responsavel = _responsavelApp.Recuperar(id);
            var responsavelViewModel = Mapper.Map<Responsavel, ResponsavelViewModel>(responsavel);

            return View("DeleteResponsavel", responsavelViewModel);
        }

        //
        // POST: /Responsavel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var responsavel = _responsavelApp.Recuperar(id);

                _responsavelApp.Remover(responsavel);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        //GET
        [HttpGet]
        public ActionResult RecuperarResponsavelPorNome()
        {
            FiltroResponsavel filtro = new FiltroResponsavel();
            filtro.Alunos = utilizavel.PreencherListaAlunos();

            ViewBag.ListaAlunos = filtro.Alunos;

            return View("FiltroConsultaResponsavelPorNome");
        }

        //POST
        [HttpPost]
        public ActionResult RecuperarResponsavelPorNome(FiltroResponsavel responsavel)
        {
            if (responsavel.alunoSelecionada != 0)
            {
                var aluno = _alunoApp.Recuperar(responsavel.alunoSelecionada);
                var alunoViewModel = Mapper.Map<Aluno, AlunoViewModel>(aluno);

                responsavel.AlunoId = alunoViewModel.Id;
            }

            var nome = responsavel.NomeResponsavel.Trim();
            var responsavelBuscarNome = _responsavelApp.PesquisarResponsavelPorNome(nome);
            var responsavelViewModel = Mapper.Map<IEnumerable<Responsavel>, IEnumerable<ResponsavelViewModel>>(responsavelBuscarNome);

            return View("RecuperarResponsavelPorNome", responsavelViewModel);
        }

        public ActionResult VisualizarTodosResponsaveis()
        {
            var resp = _responsavelApp.RecuperarTodos();
            var respMapped = Mapper.Map<IEnumerable<Responsavel>, IEnumerable<ResponsavelViewModel>>(resp);
            return View("VisualizarTodosResponsaveis", respMapped);
        }

        //private void PreencherListaAlunos(ResponsavelViewModel responsavel)
        //{
        //    List<SelectListItem> listaAluno = new List<SelectListItem>();
        //    var enumAluno = _alunoApp.RecuperarTodos();

        //    foreach (var disc in enumAluno)
        //    {

        //        SelectListItem listItem = new SelectListItem()
        //        {
        //            Value = Convert.ToString(disc.Id),
        //            Text = disc.Nome
        //        };
        //        listaAluno.Add(listItem);
        //    }

        //    responsavel.ListaAlunos = listaAluno;
        //    ViewBag.ListaAlunos = responsavel.ListaAlunos;
        //}

        public ActionResult RecuperarMeusAlunosResponsavel()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());
                //var attmpt = _responsavelApp.ExibirDadosAlunoRelacionado(idUsuario);
                var attmpt = _responsavelApp.ExibirDadosAlunoRelacionado2(idUsuario);
                //var alunoMapped = Mapper.Map<IEnumerable<Aluno>, IEnumerable<AlunoViewModel>>(attmpt);
                return View("RecuperarMeusAlunosResponsavel");
            }
            catch (Exception)
            {
                throw new NotImplementedException("Erro ao recuperar alunos.");
            }
        }

        public void PreencherListaAlunosResponsavel(ResponsavelViewModel responsavel)
        {
            List<SelectListItem> listaAlunosResponsavel = new List<SelectListItem>();
            int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());
            var attmpt = _responsavelApp.ExibirDadosAlunoRelacionado2(idUsuario);

            foreach (var aluno in attmpt)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = aluno.Keys.First().ToString(),
                    Text = aluno.Values.First().ToString()
                };
                listaAlunosResponsavel.Add(select);
            }
            responsavel.ListaAlunos = listaAlunosResponsavel;
            ViewBag.ListaAlunosResponsavel = responsavel.ListaAlunos;
        }

        [HttpGet]
        public ActionResult RecuperarAluno()
        {
            var responsavel = new ResponsavelViewModel();
            PreencherListaAlunosResponsavel(responsavel);
            return View("FiltroAlunosResponsavel");
        }

        [HttpPost]
        public ActionResult RecuperarAluno(ResponsavelViewModel responsavel)
        {

            var aluno = responsavel.alunoSelecionado;
            var aluno2 = _alunoApp.Recuperar(aluno);
            var alunolViewModel = Mapper.Map<Aluno, AlunoViewModel>(aluno2);

            return View("DetalhesAlunoSelecionado", alunolViewModel);
        }


        [HttpGet]
        public ActionResult RecuperarAlunoProva()
        {
            var responsavel = new ResponsavelViewModel();
            PreencherListaAlunosResponsavel(responsavel);
            return View("FiltroAlunosResponsavelProva");
        }



        [HttpGet]
        public ActionResult RecuperarAlunoNotas()
        {
            var responsavel = new ResponsavelViewModel();
            PreencherListaAlunosResponsavel(responsavel);
            return View("FiltroAlunosResponsavelNotas");
        }

        [HttpGet]
        public ActionResult RecuperarAlunosNotificacao()
        {
            var responsavel = new ResponsavelViewModel();
            PreencherListaAlunosResponsavel(responsavel);
            return View("FiltroAlunosResponsavelNotificacoes");
        }

    }
}
