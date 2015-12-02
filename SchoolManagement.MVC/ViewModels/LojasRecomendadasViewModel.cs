using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.MVC.ViewModels
{
    public class LojasRecomendadasViewModel
    {
        [ScaffoldColumn(false)]
        public int LojaId { get; set; }

        [Required(ErrorMessage = "É necessário o nome da loja para prossegir.")]
        [Display(Name = "Nome da loja")]
        public string NomeLoja { get; set; }

        public Endereco Endereco { get; set; }

        public Contato Contato { get; set; }
    }
}