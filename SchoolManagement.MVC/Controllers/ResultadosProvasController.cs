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
    public class ResultadosProvasController : Controller
    {
        private readonly IResultadosProvasServico _resultadosProvasApp;
        private readonly Utilizavel _util;
        private readonly ITurmaServico _turmaServico;
        private readonly IAlunoServico _alunoServico;
        private readonly IProvaServico _provaApp;

        public ResultadosProvasController(IResultadosProvasServico resultadosProvasApp, Utilizavel util, ITurmaServico turmaServico, IAlunoServico alunoServico, IProvaServico provaApp)
        {
            _resultadosProvasApp = resultadosProvasApp;
            _util = util;
            _turmaServico = turmaServico;
            _alunoServico = alunoServico;
            _provaApp = provaApp;
        }

        //
        // GET: /ResultadosProvas/
        public ActionResult RecuperarTodosResultadosProvas()
        {
            var resultados = _resultadosProvasApp.RecuperarTodos();
            var resultadosMapped = Mapper.Map<IEnumerable<ResultadosProvas>, IEnumerable<ResultadosProvasViewModel>>(resultados);

            return View("RecuperarTodosResultadosProvas", resultadosMapped);
        }

        //[HttpGet]
        //public ActionResult LancarNotasTurma(FormCollection resultados)
        //{
        //    //int professorId = (int)Session["UsuarioId"];
        //    //List<SelectListItem> ListaTurmas = new List<SelectListItem>();
        //    //var listaTurmas = _turmaServico.RecuperarTurmasQueProfessorLeciona(professorId);
        //    //foreach (var item in listaTurmas)
        //    //{
        //    //    SelectListItem select = new SelectListItem()
        //    //    {
        //    //        Value = item.TurmaId.ToString(),
        //    //        Text = String.Concat(item.Descricao, " (", this.RecuperarValorHorarioTurma(item.HorariosTurmaId), ")")
        //    //    };
        //    //    ListaTurmas.Add(select);
        //    //}

        //    //return View("LancarNotasSelecaoTurma", ListaTurmas);

        //    try
        //    {

        //    }
        //    catch(Exce)
        //    {

        //    }
        //}

        public ActionResult LancarNotasTurma(FormCollection resultados)
        {
            try
            {
                var AlunosLista = resultados["item.Aluno.Id"];
                var NotasLista = resultados["item.resul"];

                int provaIdEscolhida = (int)Session["provaIdselecionado"];

                var prova = _provaApp.RecuperarProva(provaIdEscolhida);
                var provaMap = Mapper.Map<Prova, ProvaViewModel>(prova);


                string[] quebAlunos = AlunosLista.Split(',');
                string[] quebNotas = NotasLista.Split(',');


                List<ResultadosProvasViewModel> listResultados = new List<ResultadosProvasViewModel>();

                for (int i = 0; i < quebAlunos.Length; i++)
                {
                    ResultadosProvasViewModel rp = new ResultadosProvasViewModel();

                    var aluno = _alunoServico.RecuperarDadosAluno(Convert.ToInt32(quebAlunos[i]));
                    var alunoMap = Mapper.Map<Aluno, AlunoViewModel>(aluno);

                    rp.Aluno = alunoMap;
                    rp.Nota = Convert.ToInt32(quebNotas[i]);
                    rp.Prova = provaMap;
                    rp.Observacao = "";
                    rp.Gabarito = "";

                    listResultados.Add(rp);
                }


                foreach (var item in listResultados)
                {
                    var resultMapped = Mapper.Map<ResultadosProvasViewModel, ResultadosProvas>(item);
                    var atmpt = _resultadosProvasApp.IncluirNotaAluno(resultMapped);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }



        public ActionResult RecuperarNotasAluno(int alunoId)
        {
            var attmpt = _resultadosProvasApp.RecuperarNotasAluno(alunoId);
            var attmptMapped = Mapper.Map<IEnumerable<ResultadosProvas>, IEnumerable<ResultadosProvasViewModel>>(attmpt);
            return View("VisualizarNotasAluno", attmptMapped);
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

        [HttpGet]
        public ActionResult FiltroTurmasProfessorLeciona()
        {
            int professorId = (int)Session["UsuarioId"];
            List<SelectListItem> ListaTurmas = new List<SelectListItem>();
            var listaTurmas = _turmaServico.RecuperarTurmasQueProfessorLeciona(professorId);
            foreach (var item in listaTurmas)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = item.TurmaId.ToString(),
                    Text = String.Concat(item.Descricao, " (", this.RecuperarValorHorarioTurma(item.HorariosTurmaId), ")")
                };
                ListaTurmas.Add(select);
            }

            ViewBag.ListaTurmas = ListaTurmas;

            return View("FiltroTurmasProfessorLecionaLancarNotas");
        }

        [HttpGet]
        public ActionResult FiltroTurmasProvaProfessorLeciona(TurmaViewModel turma)
        {
            int professorId = (int)Session["UsuarioId"];
            int turmaIdEscolhida = turma.TurmaId;

            List<SelectListItem> ListaProvasResultadoNota = new List<SelectListItem>();
            var listaProvas = _provaApp.RecuperarProvasPendentesTurmaProfessor(professorId, turmaIdEscolhida);
            foreach (var item in listaProvas)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = Convert.ToString(item.ProvaId),
                    Text = item.Disciplina.NomeDisciplina
                };
                ListaProvasResultadoNota.Add(select);
            }

            ViewBag.ListaProvasResultadoNota = ListaProvasResultadoNota;

            return View("FiltroProvasProfessorTurma");
        }

        [HttpPost]
        public ActionResult VisualizarAlunosTurmasProfessorLecionaLancarNota(ProvaViewModel provaid2)
        {

            Session["provaIdselecionado"] = provaid2.ProvaId;

            List<ResultadosProvas> AlunosBackEnd = new List<ResultadosProvas>();

            int professorId = (int)Session["UsuarioId"];
            //VisualizarTurmasProfessor
            var turmas = _turmaServico.RecuperarTurmasQueProfessorLeciona(professorId);

            foreach (var turma in turmas)
            {
                var alunos = _alunoServico.RecuperarAlunosTurma(turma.TurmaId).ToList();
                foreach (var aluno in alunos)
                {
                    ResultadosProvas rp = new ResultadosProvas();
                    rp.Aluno = aluno;
                    AlunosBackEnd.Add(rp);
                }
            }

            Utilizavel util = new Utilizavel();
            ViewBag.ListaNotas = util.PreencherListasNotas();

            var alunosMapped = Mapper.Map<IEnumerable<ResultadosProvas>, IEnumerable<ResultadosProvasViewModel>>(AlunosBackEnd);
            return View("VisualizarAlunosTurmasProfessorLecionaLancarNotas", alunosMapped);
        }


        [HttpGet]
        public ActionResult FiltroTurmasConsultaLancarNotas()
        {
            int professorId = (int)Session["UsuarioId"];
            List<SelectListItem> ListaTurmasConsulta = new List<SelectListItem>();
            var listaTurmasConsulta = _turmaServico.RecuperarTurmasQueProfessorLeciona(professorId);
            foreach (var item in listaTurmasConsulta)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = item.TurmaId.ToString(),
                    Text = String.Concat(item.Descricao, " (", this.RecuperarValorHorarioTurma(item.HorariosTurmaId), ")")
                };
                ListaTurmasConsulta.Add(select);
            }

            ViewBag.ListaTurmasConsulta = ListaTurmasConsulta;

            return View("FiltroTurmasConsultaLancarNotas");
        }

        [HttpGet]
        public ActionResult FiltroProvasConsultaLancarNotas(TurmaViewModel turma)
        {
            int professorId = (int)Session["UsuarioId"];
            int turmaIdEscolhida = turma.TurmaId;

            List<SelectListItem> ListaProvasResultadoNotaConcluidas = new List<SelectListItem>();
            var listaProvasConcluidas = _provaApp.RecuperarProvasConcluidasTurmaProfessor(professorId, turmaIdEscolhida);
            foreach (var item in listaProvasConcluidas)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = Convert.ToString(item.ProvaId),
                    Text = item.Disciplina.NomeDisciplina
                };
                ListaProvasResultadoNotaConcluidas.Add(select);
            }

            ViewBag.ListaProvasResultadoNotaConcluidas = ListaProvasResultadoNotaConcluidas;

            return View("FiltroProvasConsultaLancarNotas");
        }

        [HttpPost]
        public ActionResult VisualizarAlunosProvasConcluidas(ProvaViewModel provaid3)
        {

            Session["provaIdselecionado"] = provaid3.ProvaId;

            List<ResultadosProvas> AlunosBackEnd = new List<ResultadosProvas>();

            int professorId = (int)Session["UsuarioId"];
            //VisualizarTurmasProfessor
            var turmas = _turmaServico.RecuperarTurmasQueProfessorLeciona(professorId);

            foreach (var turma in turmas)
            {
                var alunos = _alunoServico.RecuperarAlunosTurma(turma.TurmaId).ToList();
                foreach (var aluno in alunos)
                {
                    ResultadosProvas rp = new ResultadosProvas();
                    rp.Aluno = aluno;
                    AlunosBackEnd.Add(rp);
                }
            }

            var alunosMapped = Mapper.Map<IEnumerable<ResultadosProvas>, IEnumerable<ResultadosProvasViewModel>>(AlunosBackEnd);
            return View("VisualizarAlunosProvasConcluidas", alunosMapped);
        }
    }
}