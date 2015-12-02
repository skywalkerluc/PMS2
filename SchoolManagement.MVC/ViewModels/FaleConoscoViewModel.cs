using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.MVC.ViewModels
{
    public class FaleConoscoViewModel
    {
        [ScaffoldColumn(false)]
        public int ContatoId { get; set; }

        public string Nome { get; set; }
        
        public string Mensagem { get; set; }
        
        [Display(Name = "Tipo de contato")]
        public TipoContato TipoContato { get; set; }
        
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        
        [ScaffoldColumn(false)]
        public DateTime DataHoraEnvio { get; set; }
    }
}