using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.ViewModels
{
    public class ProfessorViewModel : FuncionarioViewModel
    {
        [Display(Name = "Matrícula")]
        public string Matricula { get; set; }

        public virtual List<DisciplinaViewModel> Disciplinas { get; set; }

        public List<int> disciplinasTeste { get; set; }

        public string Especialidade { get; set; }

        public ICollection<TurmaViewModel> Turmas { get; set; }

    }
}