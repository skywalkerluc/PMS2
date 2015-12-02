using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace SchoolManagement.MVC.ViewModels.FiltroViewModel
{
    public class FiltroDisciplina
    {
        public string NomeDisciplina { get; set; }
        public int LivroId { get; set; }

        public List<SelectListItem> ListaLivros { get; set; }
    }
}