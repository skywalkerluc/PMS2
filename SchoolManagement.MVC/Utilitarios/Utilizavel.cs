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

        public List<SelectListItem> PreencherListasNotas()
        {
            List<SelectListItem> ListaRetorno = new List<SelectListItem>();
            CriarItemBrancoEmLista(ListaRetorno);

            SelectListItem um = new SelectListItem()
            {
                Value = "1",
                Text = "1"
            };
            ListaRetorno.Add(um);

            SelectListItem dois = new SelectListItem()
            {
                Value = "2",
                Text = "2"
            };
            ListaRetorno.Add(dois);

            SelectListItem tres = new SelectListItem()
            {
                Value = "3",
                Text = "3"
            };
            ListaRetorno.Add(tres);

            SelectListItem quatro = new SelectListItem()
            {
                Value = "4",
                Text = "4"
            };
            ListaRetorno.Add(quatro);

            SelectListItem cinco = new SelectListItem()
            {
                Value = "5",
                Text = "5"
            };
            ListaRetorno.Add(cinco);

            SelectListItem seis = new SelectListItem()
            {
                Value = "6",
                Text = "6"
            };
            ListaRetorno.Add(seis);

            SelectListItem sete = new SelectListItem()
            {
                Value = "7",
                Text = "7"
            };
            ListaRetorno.Add(sete);

            SelectListItem oito = new SelectListItem()
            {
                Value = "8",
                Text = "8"
            };
            ListaRetorno.Add(oito);

            SelectListItem nove = new SelectListItem()
            {
                Value = "9",
                Text = "9"
            };
            ListaRetorno.Add(nove);

            SelectListItem dez = new SelectListItem()
            {
                Value = "10",
                Text = "10"
            };
            ListaRetorno.Add(dez);

            return ListaRetorno;
        }


        public List<SelectListItem> PreencherListasFrequencia()
        {
            List<SelectListItem> ListaRetorno = new List<SelectListItem>();
            CriarItemBrancoEmLista(ListaRetorno);

            SelectListItem presente = new SelectListItem()
            {
                Value = "1",
                Text = "Presente"
            };
            ListaRetorno.Add(presente);

            SelectListItem falta = new SelectListItem()
            {
                Value = "2",
                Text = "Falta"
            };
            ListaRetorno.Add(falta);

            return ListaRetorno;
        }
    }
}