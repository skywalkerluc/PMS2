using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.ViewModels.FiltroViewModel
{
    public class FiltroTurma
    {
        [Display(Name = "Descrição")]
        public string DescricaoTurma { get; set; }

        [Display(Name = "Professor")]
        public ProfessorViewModel Professor { get; set; }

        [Display(Name = "Ano Letivo")]
        public AnoLetivoViewModel AnoLetivo { get; set; }

        [Display(Name = "Horário")]
        public int HorarioId { get; set; }


        List<SelectListItem> ListaProfessores { get; set; }
        List<SelectListItem> AnosLetivos { get; set; }
        List<SelectListItem> Horarios { get; set; }
        List<SelectListItem> ListaTurmas { get; set; }

        public int professorSelecionado { get; set; }
        public int anoLetivoSelecionado { get; set; }
    }
}