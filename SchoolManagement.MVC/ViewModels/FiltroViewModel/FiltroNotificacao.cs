using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.ViewModels.FiltroViewModel
{
    public class FiltroNotificacao
    {
        public List<SelectListItem> ListaTurmas { get; set; }
        public int TurmaSelecionada { get; set; }
        public string Assunto { get; set; }
    }
}