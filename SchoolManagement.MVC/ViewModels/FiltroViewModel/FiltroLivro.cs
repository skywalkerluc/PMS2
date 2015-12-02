using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace SchoolManagement.MVC.ViewModels.FiltroViewModel
{
    public class FiltroLivro
    {
        public string NomeLivro { get; set; }
        public string NomeEditora { get; set; }
        public string NomeAutor { get; set; }

        public virtual TurmaViewModel Turma { get; set; }

        public virtual DisciplinaViewModel Disciplina { get; set; }


        public List<SelectListItem> ListaTurmas { get; set; }
        public List<SelectListItem> ListaDisciplinas { get; set; }

        public int turmaEscolhida { get; set; }

        public int disciplinaEscolhida { get; set; }
    }
}