using AutoMapper;
using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Servicos;
using SchoolManagement.MVC.ViewModels;
using SchoolManagement.MVC.ViewModels.FiltroViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SchoolManagement.MVC.Controllers
{
    public class TurmaController : Controller
    {
        private readonly ITurmaServico _turmaServico;
        private readonly IDisciplinaServico _disciplinaServico;
        private readonly IAnoLetivoServico _anoLetivoServico;
        private readonly IProfessorServico _profServico;
        private readonly IAlunoServico _alunoApp;

        public TurmaController(ITurmaServico turmaServico, IDisciplinaServico disciplinaServico, IAnoLetivoServico anoLetivoServico, IProfessorServico profServico, IAlunoServico alunoApp)
        {
            _turmaServico = turmaServico;
            _disciplinaServico = disciplinaServico;
            _anoLetivoServico = anoLetivoServico;
            _profServico = profServico;
            _alunoApp = alunoApp;
        }

        //
        // GET: /Turma/
        public ActionResult Index()
        {
            try
            {
                var enumeradorTurmas = _turmaServico.RecuperarTodos();
                var turmaViewModel = Mapper.Map<IEnumerable<Turma>, IEnumerable<TurmaViewModel>>(enumeradorTurmas);
                return View("Index", turmaViewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Create");
            }

        }

        //
        // GET: /Turma/Details/5
        public ActionResult Details(int id)
        {
            var turma = _turmaServico.Recuperar(id);
            var turmaViewModel = Mapper.Map<Turma, TurmaViewModel>(turma);

            return View("ExibirInformacoes", turmaViewModel);
        }

        //
        // GET: /Turma/Create
        [HttpGet]
        public ActionResult Create()
        {
            var turma = new TurmaViewModel();
            var filtro = new FiltroTurma();
            PreencherListaDisciplina(turma);
            PreencherListaAnoLetivo(turma);
            PreencherListaProfessor(turma, filtro);
            return View("AdicionarTurma");
        }

        //
        // POST: /Turma/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TurmaViewModel turma)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    List<DisciplinaViewModel> ListaDisciplinas = new List<DisciplinaViewModel>();
                    if (turma.disciplinasSelecionadas.Count > 0)
                    {
                        for (int i = 0; i <= turma.disciplinasSelecionadas.Count - 1; i++)
                        {
                            var disciplina = _disciplinaServico.Recuperar(turma.disciplinasSelecionadas[i]);
                            var disciplinaViewModel = Mapper.Map<Disciplina, DisciplinaViewModel>(disciplina);

                            ListaDisciplinas.Add(disciplinaViewModel);
                            //professor.Disciplinas.Add(disciplinaViewModel);
                        }
                    }

                    turma.Disciplinas = ListaDisciplinas;

                    if (turma.anoletivoSelecionado != 0)
                    {
                        var anoLetivo = _anoLetivoServico.Recuperar(turma.anoletivoSelecionado);
                        var anoLetivoViewModel = Mapper.Map<AnoLetivo, AnoLetivoViewModel>(anoLetivo);

                        turma.AnoLetivo = anoLetivoViewModel;
                    }


                    var turmaDomain = Mapper.Map<TurmaViewModel, Turma>(turma);
                    _turmaServico.IncluirTurma(turmaDomain);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    var mensagemErro = ex.Message.ToString();
                    return RedirectToAction("Index", "Home", mensagemErro);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Turma/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var turma = _turmaServico.Recuperar(id);
            var turmaViewModel = Mapper.Map<Turma, TurmaViewModel>(turma);
            return View("EditarTurma", turmaViewModel);
        }

        //
        // POST: /Turma/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TurmaViewModel turma)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var turmaDomain = Mapper.Map<TurmaViewModel, Turma>(turma);
                    _turmaServico.Atualizar(turmaDomain);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    var mensagemErro = ex.Message.ToString();
                    return RedirectToAction("Index", "Home", mensagemErro);
                }
            }
            return RedirectToAction("Index", "Home", turma);
        }

        //
        // GET: /Turma/Delete/5
        public ActionResult Delete(int id)
        {
            var turma = _turmaServico.Recuperar(id);
            var turmaViewModel = Mapper.Map<Turma, TurmaViewModel>(turma);

            return View("DeleteTurma", turmaViewModel);
        }

        //
        // POST: /Turma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var turma = _turmaServico.Recuperar(id);
                _turmaServico.Remover(turma);
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                var mensagemErro = ex.Message.ToString();
                return RedirectToAction("Index", "Home", mensagemErro);
            }
        }

        private void PreencherListaDisciplina(TurmaViewModel turma)
        {
            List<SelectListItem> listaDisciplinas = new List<SelectListItem>();
            var enumDisciplinas = _disciplinaServico.RecuperarTodos();

            foreach (var disc in enumDisciplinas)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = disc.DisciplinaId.ToString(),
                    Text = disc.NomeDisciplina
                };
                listaDisciplinas.Add(listItem);
            }

            turma.ListaDisciplinas = listaDisciplinas;
            ViewBag.ListaDisciplinas = turma.ListaDisciplinas;
        }

        private void PreencherListaAnoLetivo(TurmaViewModel turma)
        {
            List<SelectListItem> listAnoLetivo = new List<SelectListItem>();
            var enumAnoLet = _anoLetivoServico.RecuperarTodos();

            foreach (var anoLet in enumAnoLet)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = anoLet.AnoLetivoId.ToString(),
                    Text = anoLet.Ano.ToString()
                };
                listAnoLetivo.Add(listItem);
            }

            turma.ListaAnoLetivo = listAnoLetivo;
            ViewBag.ListaAnoLetivo = turma.ListaAnoLetivo;
        }

        #region Métodos de Responsável

        //GET
        [HttpGet]
        public ActionResult FiltrarTurma()
        {
            TurmaViewModel turma = new TurmaViewModel();
            FiltroTurma filtroTurma = new FiltroTurma();
            PreencherListaProfessor(turma, filtroTurma);
            PreencherListaAnoLetivo(turma, filtroTurma);
            return View("FiltroConsultaTruma", filtroTurma);
        }

        //POST
        [HttpPost]
        public ActionResult FiltrarTurma(FiltroTurma turma)
        {

            if (turma.anoLetivoSelecionado != 0)
            {
                var anoLetivo = _anoLetivoServico.Recuperar(turma.anoLetivoSelecionado);
                var anoLetivoViewModel = Mapper.Map<AnoLetivo, AnoLetivoViewModel>(anoLetivo);

                turma.AnoLetivo = anoLetivoViewModel;
            }

            if (turma.professorSelecionado != 0)
            {
                var professor = _profServico.Recuperar(turma.professorSelecionado);
                var professorViewModel = Mapper.Map<Professor, ProfessorViewModel>(professor);

                turma.Professor = professorViewModel;
            }

            var prof = Mapper.Map<ProfessorViewModel, Professor>(turma.Professor);
            var anoLetivo2 = Mapper.Map<AnoLetivoViewModel, AnoLetivo>(turma.AnoLetivo);

                var filtroTurma = _turmaServico.FiltrarTurma(turma.DescricaoTurma, prof, anoLetivo2, turma.HorarioId);
            var responsavelViewModel = Mapper.Map<IEnumerable<Turma>, IEnumerable<TurmaViewModel>>(filtroTurma);

            return View("ResultadoConsultaTurma", responsavelViewModel);
        }

        private void PreencherListaProfessor(TurmaViewModel turma, FiltroTurma filtro)
        {
            List<SelectListItem> ListaProfessores = new List<SelectListItem>();
            var prof = _profServico.RecuperarTodos();
            var profMapped = Mapper.Map<IEnumerable<Professor>, IEnumerable<ProfessorViewModel>>(prof);

            foreach (var professor in profMapped)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = professor.Id.ToString(),
                    Text = professor.Nome
                };
                ListaProfessores.Add(select);
            }
            ViewBag.ListaProfessores = ListaProfessores;
        }

        private void PreencherListaAnoLetivo(TurmaViewModel turma, FiltroTurma filtro)
        {
            List<SelectListItem> ListaAnoLetivo = new List<SelectListItem>();
            var anosLetivos = _anoLetivoServico.RecuperarTodos();
            var anoLetivoMapped = Mapper.Map<IEnumerable<AnoLetivo>, IEnumerable<AnoLetivoViewModel>>(anosLetivos);

            foreach (var anoLetivo in anoLetivoMapped)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = anoLetivo.AnoLetivoId.ToString(),
                    Text = anoLetivo.Ano.ToString()
                };
                ListaAnoLetivo.Add(select);
            }
            ViewBag.ListaAnoLetivo = ListaAnoLetivo;
        }

        #endregion

        public ActionResult InserirDisciplinasTurma(int turmaId, List<int> disciplinas)
        {
            var turmaRecuperada = _turmaServico.Recuperar(turmaId);
            List<Disciplina> ListaDisciplinas = new List<Disciplina>();

            foreach (var disc in disciplinas)
            {
                var disciplina = _disciplinaServico.Recuperar(disc);
                ListaDisciplinas.Add(disciplina);
            }
            turmaRecuperada.Disciplinas = ListaDisciplinas;
            var atualizarTurma = _turmaServico.Atualizar(turmaRecuperada);
            if (atualizarTurma)
            {
                return View("Index", "Home");
            }
            else
            { 
                throw new NotImplementedException("Não foi possível adicionar disciplinas na turma selecionada");
            }
        }

        public ActionResult VisualizarTodasTurmas()
        {
            var turmas = _turmaServico.RecuperarTodos();
            var turmasMapped = Mapper.Map<IEnumerable<Turma>, IEnumerable<TurmaViewModel>>(turmas);
            return View("VisualizarTodasTurmas", turmasMapped);
        }

        public ActionResult VerMinhaTurma()
        {
            int idUsuario = Convert.ToInt32(Session["UsuarioId"].ToString());

            var aluno = _alunoApp.Recuperar(idUsuario);
            var alunoTurma = _turmaServico.Recuperar(aluno.Turma.TurmaId);

            var turmaViewModel = Mapper.Map<Turma, TurmaViewModel>(alunoTurma);

            return View("VisualizaMinhaTurma", turmaViewModel);
        }

        public ActionResult DetalhesMinhaTurma(int id)
        {
            var turma = _turmaServico.Recuperar(id);
            var turmaViewModel = Mapper.Map<Turma, TurmaViewModel>(turma);

            return View("DetalhesMinhaTurma", turmaViewModel);
        }
        
    }
}
