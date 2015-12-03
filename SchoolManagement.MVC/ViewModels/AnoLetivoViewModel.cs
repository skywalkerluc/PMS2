using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SchoolManagement.MVC.ViewModels
{
    public class AnoLetivoViewModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int AnoLetivoId { get; set; }

        [Display(Name = "Quantidade de unidades")]
        public int QntUnidades { get; set; }

        public ICollection<TurmaViewModel> Turmas { get; set; }
        
        public int Ano { get; set; }

        
    }
}