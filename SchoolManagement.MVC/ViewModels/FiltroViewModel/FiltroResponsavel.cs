using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;


namespace SchoolManagement.MVC.ViewModels.FiltroViewModel
{
    public class FiltroResponsavel
    {
        public List<SelectListItem> Alunos { get; set; }

        public int? AlunoId { get; set; }
        public string NomeResponsavel { get; set; }

        public int alunoSelecionada { get; set; }
    }
}