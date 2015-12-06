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
        public virtual List<DisciplinaViewModel> Disciplinas { get; set; }

        public List<int> disciplinasTeste { get; set; }

        public string Especialidade { get; set; }

        public ICollection<TurmaViewModel> Turmas { get; set; }

        public int professorSelecionado { get; set; }
        public int turmaSelecionada { get; set; }

    }
}