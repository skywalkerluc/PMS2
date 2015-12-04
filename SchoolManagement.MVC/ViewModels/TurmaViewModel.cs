using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SchoolManagement.MVC.ViewModels
{
    public class TurmaViewModel
    {
        [ScaffoldColumn(false)]
        public int TurmaId { get; set; }

        [Display(Name = "Professor")]
        public ICollection<ProfessorViewModel> Professores { get; set; }

        public ICollection<DisciplinaViewModel> Disciplinas { get; set; }

        public ICollection<AlunoViewModel> Alunos { get; set; }

        [Display(Name = "Descricao da Turma")]
        public string Descricao { get; set; }

        [Display(Name = "Horários da turma")]
        public int HorariosTurmaId { get; set; }

        [Display(Name = "Ano letivo")]
        public virtual AnoLetivoViewModel AnoLetivo { get; set; }

        public int Vagas { get; set; }

        public virtual ICollection<LivroViewModel> Materiais { get; set; }

        public List<SelectListItem> ListaDisciplinas { get; set; }
        public List<SelectListItem> ListaAnoLetivo { get; set; }

        public List<int> disciplinasSelecionadas { get; set; }

        public int anoletivoSelecionado { get; set; }
    }
}