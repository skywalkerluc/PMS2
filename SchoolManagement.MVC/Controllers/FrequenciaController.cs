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
    public class FrequenciaController : Controller
    {
        private readonly IFrequenciaServico _frequenciaServico;
        private readonly IAlunoServico _alunoServico;
        private readonly IDisciplinaServico _disciplinaServico;
        private readonly IProfessorServico _professorServico;
        private readonly ITurmaServico _turmaServico;
        private readonly Utilizavel _util;

        public FrequenciaController(IFrequenciaServico frequenciaServico, IAlunoServico alunoServico, IDisciplinaServico disciplinaServico, IProfessorServico professorServico, ITurmaServico turmaServico, Utilizavel util)
        {
            _frequenciaServico = frequenciaServico;
            _alunoServico = alunoServico;
            _disciplinaServico = disciplinaServico;
            _professorServico = professorServico;
            _turmaServico = turmaServico;
            _util = util;
        }

        //
        // GET: /Frequencia/
        public ActionResult MostrarHistoricoFrequenciaTodosAlunos()
        {
            var frequencias = _frequenciaServico.RecuperarTodos();
            var frequenciasMapped = Mapper.Map<IEnumerable<Frequencia>, IEnumerable<FrequenciaViewModel>>(frequencias);
            return View("VisualizarFrequenciasTodosAlunos", frequenciasMapped);
        }

        public ActionResult MostrarHistorioFrequenciaAluno(int AlunoId)
        {
            var frequencias = _frequenciaServico.RecuperarHistoricoFrequenciasAluno(AlunoId);
            var frequenciasMapped = Mapper.Map<IEnumerable<Frequencia>, IEnumerable<FrequenciaViewModel>>(frequencias);
            return View("VisualizarFrequenciasAluno", frequenciasMapped);
        }

        public ActionResult IncluirDadosFrequenciaAluno()
        {
            List<DisciplinaViewModel> ListDisciplina = new List<DisciplinaViewModel>();
            List<TurmaViewModel> ListTurmas = new List<TurmaViewModel>();
            List<AlunoViewModel> ListAlunos = new List<AlunoViewModel>();

            List<SelectListItem> ListaAlunos = new List<SelectListItem>(); 
            List<SelectListItem> ListaDisciplinas = new List<SelectListItem>();
            int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());
            var turmas = _turmaServico.RecuperarTodos();
            var turmasMapped = Mapper.Map<IEnumerable<Turma>, IEnumerable<TurmaViewModel>>(turmas);

            foreach (var turma in turmasMapped)
            {
                foreach (var prof in turma.Professores)
                {
                    if (prof.Id == idUsuario)
                    {
                        ListTurmas.Add(turma);
                    }
                }
            }

            foreach (var item in ListTurmas)
            {
                var disciplinas = _disciplinaServico.RecuperarDisciplinasTurma(item.TurmaId);
                var discMapped = Mapper.Map<IEnumerable<Disciplina>, IEnumerable<DisciplinaViewModel>>(disciplinas);

                var alunos = _alunoServico.RecuperarAlunosTurma(item.TurmaId);
                var alunosMapped = Mapper.Map<IEnumerable<Aluno>, IEnumerable<AlunoViewModel>>(alunos);

                foreach (var aluno in alunosMapped)
                {
                    ListAlunos.Add(aluno);
                }
                
                foreach (var disc in discMapped)
                {
                    ListDisciplina.Add(disc);
                }
            }

            foreach (var alun in ListAlunos)
            {
                var selectListItem = new SelectListItem()
                {
                    Value = alun.Id.ToString(),
                    Text = alun.Nome.ToString()
                };
                ListaAlunos.Add(selectListItem);
            }

            foreach (var item in ListDisciplina)
            {
                var selectListItem = new SelectListItem()
                {
                    Value = item.DisciplinaId.ToString(),
                    Text = item.NomeDisciplina.ToString()
                };
                ListaDisciplinas.Add(selectListItem);
            }

            ViewBag.ListaAlunos = ListaAlunos;
            ViewBag.ListaDisciplinas = ListaDisciplinas;

            var frequencia = new FrequenciaViewModel();
            return View("IncluirDadosFrequenciaAluno", frequencia);
        }

        [HttpPost]
        public ActionResult IncluirDadosFrequenciaAluno(FrequenciaViewModel frequencia, int AlunoId, int DisciplinaId)
        {
            var aluno = _alunoServico.Recuperar(AlunoId);
            var disciplina = _disciplinaServico.Recuperar(DisciplinaId);

            var frequenciaMapped = Mapper.Map<FrequenciaViewModel, Frequencia>(frequencia);
            frequenciaMapped.Aluno = aluno;
            frequenciaMapped.Disciplina = disciplina;

            var attempt = _frequenciaServico.IncluirFrequenciaAluno(frequenciaMapped);

            if (attempt != null)
            {
                return View("Index", "Home");
            }
            else
            {
                ViewBag.AlertMessage = "Erro ao incluir dados de frequência de um aluno.";
                throw new NotImplementedException("Erro ao incluir dados de frequência de um aluno.");
            }
            
        }

        [HttpGet]
        public ActionResult FiltroTurmasFrequenciaProfessorLeciona()
        {
            int professorId = (int)Session["UsuarioId"];
            List<SelectListItem> ListaTurmasFrequencia = new List<SelectListItem>();
            var listaTurmas = _turmaServico.RecuperarTurmasQueProfessorLeciona(professorId);
            foreach (var item in listaTurmas)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = item.TurmaId.ToString(),
                    Text = String.Concat(item.Descricao, " (", this.RecuperarValorHorarioTurma(item.HorariosTurmaId), ")")
                };
                ListaTurmasFrequencia.Add(select);
            }

            ViewBag.ListaTurmasFrequencia = ListaTurmasFrequencia;

            return View("FiltroFrequenciaTurmaProfessor");
        }

        [HttpGet]
        public ActionResult FiltroDisciplinaFrequenciaProfessorLeciona(TurmaViewModel turma)
        {
            int professorId = (int)Session["UsuarioId"];

            List<SelectListItem> ListaDisciplinaFrequencia = new List<SelectListItem>();

            var listaTurmas = _disciplinaServico.RecuperarDisciplinasTurmaProfessor(turma.TurmaId, professorId);

            foreach (var item in listaTurmas)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = Convert.ToString(item.DisciplinaId),
                    Text = item.NomeDisciplina
                };
                ListaDisciplinaFrequencia.Add(select);
            }

            ViewBag.ListaDisciplinaFrequencia = ListaDisciplinaFrequencia;

            return View("FiltroDisciplinaFrequenciaProfessor");
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
        public ActionResult VisualizarAlunosTurmasProfessorLecionaFrequencia(DisciplinaViewModel disciplina)
        {

            Session["disciplinaSelecionada"] = disciplina.DisciplinaId;

            List<FrequenciaViewModel> AlunosBackEnd = new List<FrequenciaViewModel>();

            int professorId = (int)Session["UsuarioId"];
            //VisualizarTurmasProfessor
            var turmas = _turmaServico.RecuperarTurmasQueProfessorLeciona(professorId);

            foreach (var turma in turmas)
            {
                var alunos = _alunoServico.RecuperarAlunosTurma(turma.TurmaId).ToList();
                var alun = Mapper.Map<IEnumerable<Aluno>, IEnumerable<AlunoViewModel>>(alunos);
                foreach (var aluno in alun)
                {
                    FrequenciaViewModel f = new FrequenciaViewModel();
                    f.Aluno = aluno;
                    AlunosBackEnd.Add(f);
                }
            }

            Utilizavel util = new Utilizavel();
            ViewBag.ListaTiposDeFrequencia = util.PreencherListasFrequencia();

            var alunosMapped = Mapper.Map<IEnumerable<FrequenciaViewModel>, IEnumerable<FrequenciaViewModel>>(AlunosBackEnd);
            return View("VisualizarAlunosTurmasProfessorLecionaFrequencia", alunosMapped);

        }


        [HttpPost]
        public ActionResult LancarFrequenciaAluno(FormCollection resultados)
        {
            try
            {
                var AlunosLista = resultados["item.Aluno.Id"];
                var FrequenciaLista = resultados["item.resul"];
                var dataReferencia = resultados["item.DataReferencia"];

                int disciplinaSelecionada = (int)Session["disciplinaSelecionada"];

                var disciplina = _disciplinaServico.Recuperar(disciplinaSelecionada);
                var disciplinaMap = Mapper.Map<Disciplina, DisciplinaViewModel>(disciplina);


                string[] quebAlunos = AlunosLista.Split(',');
                string[] quebFrequencia = FrequenciaLista.Split(',');
                string[] quebData = dataReferencia.Split(',');


                List<FrequenciaViewModel> listResultados = new List<FrequenciaViewModel>();

                for (int i = 0; i < quebAlunos.Length; i++)
                {
                    FrequenciaViewModel rp = new FrequenciaViewModel();

                    var aluno = _alunoServico.RecuperarDadosAluno(Convert.ToInt32(quebAlunos[i]));
                    var alunoMap = Mapper.Map<Aluno, AlunoViewModel>(aluno);

                    rp.Aluno = alunoMap;
                    
                    if(Convert.ToInt32(quebFrequencia[i]) == 1)
                    {
                        rp.Presente = true;
                    }
                    else
                    {
                        rp.Presente = false;
                    }

                    rp.Disciplina = disciplinaMap;

                    rp.DataReferencia = Convert.ToDateTime(quebData[i]);

                    listResultados.Add(rp);
                }


                foreach (var item in listResultados)
                {
                    var resultMapped = Mapper.Map<FrequenciaViewModel, Frequencia>(item);
                    var atmpt = _frequenciaServico.IncluirFrequenciaAluno(resultMapped);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        [HttpGet]
        public ActionResult FiltroTurmasFrequenciaConsultaProfessor()
        {
            int professorId = (int)Session["UsuarioId"];

            List<SelectListItem> ListaTurmasFrequenciaConsulta = new List<SelectListItem>();
            var listaTurmasConsulta = _turmaServico.RecuperarTurmasQueProfessorLeciona(professorId);
            foreach (var item in listaTurmasConsulta)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = item.TurmaId.ToString(),
                    Text = String.Concat(item.Descricao, " (", this.RecuperarValorHorarioTurma(item.HorariosTurmaId), ")")
                };
                ListaTurmasFrequenciaConsulta.Add(select);
            }

            ViewBag.ListaTurmasFrequenciaConsulta = ListaTurmasFrequenciaConsulta;

            return View("FiltroTurmasFrequenciaConsultaProfessor");
        }

        [HttpGet]
        public ActionResult FiltroDisciplinaFrequenciaConsultaProfessor(TurmaViewModel turma)
        {
            int professorId = (int)Session["UsuarioId"];

            List<SelectListItem> ListaDisciplinaFrequenciaConsulta = new List<SelectListItem>();

            var listaTurmasConsulta = _disciplinaServico.RecuperarDisciplinasTurmaProfessor(turma.TurmaId, professorId);

            foreach (var item in listaTurmasConsulta)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = Convert.ToString(item.DisciplinaId),
                    Text = item.NomeDisciplina
                };
                ListaDisciplinaFrequenciaConsulta.Add(select);
            }

            ViewBag.ListaDisciplinaFrequenciaConsulta = ListaDisciplinaFrequenciaConsulta;

            return View("FiltroDisciplinaFrequenciaConsultaProfessor");
        }

        [HttpGet]
        public ActionResult VisualizarAlunosFrequenciaConsulta(DisciplinaViewModel disciplina)
        {

            Session["disciplinaSelecionada"] = disciplina.DisciplinaId;

            List<FrequenciaViewModel> AlunosBackEnd = new List<FrequenciaViewModel>();

            int professorId = (int)Session["UsuarioId"];
            //VisualizarTurmasProfessor
            var turmas = _turmaServico.RecuperarTurmasQueProfessorLeciona(professorId);

            foreach (var turma in turmas)
            {
                var alunos = _alunoServico.RecuperarAlunosTurma(turma.TurmaId).ToList();
                var alun = Mapper.Map<IEnumerable<Aluno>, IEnumerable<AlunoViewModel>>(alunos);
                foreach (var aluno in alun)
                {
                    var frequenciaAluno = _frequenciaServico.RecuperarHistoricoFrequenciasAluno(aluno.Id);
                    var frequenciaMapped = Mapper.Map<IEnumerable<Frequencia>, IEnumerable<FrequenciaViewModel>>(frequenciaAluno);
                    
                    foreach(var frequencia in frequenciaMapped)
                    {
                        FrequenciaViewModel f = new FrequenciaViewModel();
                        f.Aluno = aluno;
                        f.DataReferencia = frequencia.DataReferencia;
                        f.Presente = frequencia.Presente;
                        f.FrequenciaId = frequencia.FrequenciaId;

                        AlunosBackEnd.Add(f);
                    }
                   
                }
            }

            var alunosMapped = Mapper.Map<IEnumerable<FrequenciaViewModel>, IEnumerable<FrequenciaViewModel>>(AlunosBackEnd);
            return View("VisualizarAlunosFrequenciaConsulta", alunosMapped);

        }

        public ActionResult Details(int id)
        {
            var resultado = _frequenciaServico.Recuperar(id);
            var resultadoViewModel = Mapper.Map<Frequencia, FrequenciaViewModel>(resultado);

            return View("DetalhesFrequenciaConsulta", resultadoViewModel);
        }

        public ActionResult voltarFrequencia()
        {
            int professorId = (int)Session["UsuarioId"];

            List<SelectListItem> ListaTurmasFrequenciaConsulta = new List<SelectListItem>();
            var listaTurmasConsulta = _turmaServico.RecuperarTurmasQueProfessorLeciona(professorId);
            foreach (var item in listaTurmasConsulta)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = item.TurmaId.ToString(),
                    Text = String.Concat(item.Descricao, " (", this.RecuperarValorHorarioTurma(item.HorariosTurmaId), ")")
                };
                ListaTurmasFrequenciaConsulta.Add(select);
            }

            ViewBag.ListaTurmasFrequenciaConsulta = ListaTurmasFrequenciaConsulta;

            return View("FiltroTurmasFrequenciaConsultaProfessor");
        }

        public ActionResult Delete(int id)
        {
            var frequencia = _frequenciaServico.Recuperar(id);
            var frequenciaViewModel = Mapper.Map<Frequencia, FrequenciaViewModel>(frequencia);

            return View("ExcluirFrequencia", frequenciaViewModel);
        }

        //
        // POST: /Aluno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Frequencia f = new Frequencia();
            f.FrequenciaId = id;

            var sucesso = _frequenciaServico.Remover(f);

            if (sucesso)
                return RedirectToAction("Index", "Professor");
            else
                ViewBag.AlertMessage = "Erro ao tentar excluir uma frequencia";
            throw new NotImplementedException("Erro ao tentar excluir uma frequencia");
        }

	}
}