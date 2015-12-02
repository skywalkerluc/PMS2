using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.ViewModels
{
    public class EventoViewModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int EventoId { get; set; }

        public string Local { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        
        [Display (Name = "Data de criação do evento")]
        public DateTime DataCriacao { get; set; }
        
        [Display(Name = "Data de realização do evento")]
        public DateTime DataEvento { get; set; }
        
        [Display(Name = "Funcionário(s) responsável(is)")]
        public ICollection<FuncionarioViewModel> FuncionarioResponsavel { get; set; }
        
        [Display(Name = "Necessidade de aprovação")]
        public bool NecessidadeAprovacao { get; set; }
        
        [Display(Name = "Custo do evento")]
        public decimal PrecoEvento { get; set; }
        
        [Display(Name = "Nome de acompanhante")]
        public List<string> NomeAcompanhante { get; set; }

        public List<SelectListItem> ListaFuncionarios { get; set; }

        public int funcionariocomboselected { get; set; }

        public List<SelectListItem> ListaEventos { get; set; }

        public string eventoEscolhido { get; set; }

    }
}