using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.MVC.ViewModels.FiltroViewModel
{
    public class FiltroAluno
    {
        public int? TurmaId { get; set; }
        public List<SelectListItem> Turmas { get; set; }

        [Display(Name = "Nome do aluno")]
        public string NomeAluno { get; set; }

        public int turmaSelecionada { get; set; }
    }
}