using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.MVC.ViewModels
{
    public class TrabalhosExtrasViewModel
    {
        [ScaffoldColumn(false)]
        public int TrabalhoId { get; set; }

        public TurmaViewModel TurmaPublicoAlvo { get; set; }

        public ProfessorViewModel Professor { get; set; }
        
        public int Nota { get; set; }
        
        [Display(Name = "Data proposta")]
        public DateTime DataProposta { get; set; }
        
        [Display(Name = "Data de conclusão")]
        public DateTime DataConclusao { get; set; }
    }
}