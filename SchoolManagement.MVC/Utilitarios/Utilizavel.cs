using SchoolManagement.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace SchoolManagement.MVC.Utilitarios
{
    public class Utilizavel
    {
        private readonly ITurmaServico _turmaServico;
        private readonly IProfessorServico _profServico;
        private readonly ILivroServico _livroServicoApp;
        private readonly IDisciplinaServico _disciplinaApp;
        private readonly IAlunoServico _alunoApp;

        public Utilizavel(IDisciplinaServico disciplinaApp, ITurmaServico turmaServico, IProfessorServico professorServico, ILivroServico livroServico, IAlunoServico alunoApp)
        {
            _turmaServico = turmaServico;
            _profServico = professorServico;
            _livroServicoApp = livroServico;
            _disciplinaApp = disciplinaApp;
            _alunoApp = alunoApp;
        }

        public Utilizavel()
        {
                
        }

        public List<SelectListItem> PreencherListaFuncoes()
        {
            List<SelectListItem> ListaRetorno = new List<SelectListItem>();
            CriarItemBrancoEmLista(ListaRetorno);

            SelectListItem Professor = new SelectListItem()
            {
                Value = "1",
                Text = "Professor"
            };
            ListaRetorno.Add(Professor);

            SelectListItem Outros = new SelectListItem()
            {
                Value = "2",
                Text = "Outros"
            };
            ListaRetorno.Add(Outros);

            return ListaRetorno;
        }

        public List<SelectListItem> PreencherListsConteudosExtras()
        {
            List<SelectListItem> ListaRetorno = new List<SelectListItem>();
            CriarItemBrancoEmLista(ListaRetorno);

            SelectListItem Professor = new SelectListItem()
            {
                Value = "1",
                Text = "Conteudo Extra"
            };
            ListaRetorno.Add(Professor);

            SelectListItem Outros = new SelectListItem()
            {
                Value = "2",
                Text = "Trabalhos Extras"
            };
            ListaRetorno.Add(Outros);

            return ListaRetorno;
        }

        public List<SelectListItem> PreencherListaTurmas()
        {
            var turmas = _turmaServico.RecuperarTodos();
            List<SelectListItem> ListaRetorno = new List<SelectListItem>();
            CriarItemBrancoEmLista(ListaRetorno);

            foreach (var item in turmas)
            {
                SelectListItem select = new SelectListItem()
                {
                     Value = item.TurmaId.ToString(),
                     Text = String.Concat(item.Descricao, " (", this.RecuperarValorHorarioTurma(item.HorariosTurmaId), ")")
                };
                ListaRetorno.Add(select);
            }
            return ListaRetorno;
        }

        public List<SelectListItem> PreencherListaProfessores()
        {
            var professores = _profServico.RecuperarTodos();
            List<SelectListItem> ListaRetorno = new List<SelectListItem>();
            CriarItemBrancoEmLista(ListaRetorno);

            foreach (var item in professores)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Nome
                };
                ListaRetorno.Add(select);
            }
            return ListaRetorno;
        }

        public List<SelectListItem> PreencherListaLivros()
        {
            var livros = _livroServicoApp.RecuperarTodos();
            List<SelectListItem> ListaRetorno = new List<SelectListItem>();
            CriarItemBrancoEmLista(ListaRetorno);

            foreach (var item in livros)
            {
                SelectListItem select = new SelectListItem()
                {
                    Value = item.LivroId.ToString(),
                    Text = String.Concat(item.NomeLivro, " - ", item.Autor)
                };
                ListaRetorno.Add(select);
            }
            return ListaRetorno;
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

        public void CriarItemBrancoEmLista(List<SelectListItem> ListaSelecionaveis)
        {
            SelectListItem selectList = new SelectListItem()
            {
                Text = string.Empty,
                Value = "0",
                Selected = true
            };
            ListaSelecionaveis.Add(selectList);
        }

        public List<SelectListItem> PreencherListaDisciplina()
        {
            List<SelectListItem> listaDisciplinas = new List<SelectListItem>();
            var enumDisciplinas = _disciplinaApp.RecuperarTodos();
            CriarItemBrancoEmLista(listaDisciplinas);

            foreach (var disc in enumDisciplinas)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = disc.DisciplinaId.ToString(),
                    Text = disc.NomeDisciplina
                };
                listaDisciplinas.Add(listItem);
            }

            return listaDisciplinas;
        }

        public List<SelectListItem> PreencherListaAlunos()
        {
            List<SelectListItem> listaAluno = new List<SelectListItem>();
            var enumAluno = _alunoApp.RecuperarTodos();
            CriarItemBrancoEmLista(listaAluno);

            foreach (var disc in enumAluno)
            {

                SelectListItem listItem = new SelectListItem()
                {
                    Value = Convert.ToString(disc.Id),
                    Text = disc.Nome
                };
                listaAluno.Add(listItem);
            }

            return listaAluno;
        }
    }
}