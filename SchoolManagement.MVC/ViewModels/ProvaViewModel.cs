using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.ViewModels
{
    public class ProvaViewModel
    {
        [ScaffoldColumn(false)]
        public int ProvaId { get; set; }

        public DisciplinaViewModel Disciplina { get; set; }
        public ProfessorViewModel Professores { get; set; }
        public TurmaViewModel Turma { get; set; }

        [Display(Name = "Data da prova")]
        public DateTime DataProva { get; set; }
        public int Unidade { get; set; }

        [Display(Name = "Status da prova")]
        public StatusProva StatusProva { get; set; }

        [Display(Name = "Tipo de prova")]
        public TipoProva TipoProva { get; set; }

        public List<byte> Anexos { get; set; }

        public List<SelectListItem> ListaDisciplinas { get; set; }
        public List<SelectListItem> ListaProfessores { get; set; }
        public List<SelectListItem> ListaTurmas { get; set; }

        public int disciplinasTeste { get; set; }
        public int professoresLista { get; set; }
        public int turmaLista { get; set; }

        public int status { get; set; }
        public int tipo { get; set; }
    }
}