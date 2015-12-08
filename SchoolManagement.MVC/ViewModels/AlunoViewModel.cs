using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.ViewModels
{
    public class AlunoViewModel : UsuarioViewModel
    {
        public Etnia Etnia { get; set; }

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        [Display(Name = "Responsáveis")]
        public ICollection<ResponsavelViewModel> Responsaveis { get; set; }

        [Display(Name = "Status de cadastro")]
        public StatusCadastro StatusCadastro { get; set; }

        public virtual TurmaViewModel Turma { get; set; }

        public List<SelectListItem> ListaTurmas { get; set; }

        public int turmaEscolhida { get; set; }

        [Display(Name = "Alunos")]
        public List<SelectListItem> AlunosResponsavel { get; set; }

        public int alunoSelecionadoResponsavel { get; set; }

        public int notas { get; set; }
    }
}