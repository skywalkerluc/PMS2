using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.WebPages.Html;

namespace SchoolManagement.MVC.ViewModels
{
    public class EnderecoViewModel
    {
        [Display(Name = "Endereco")]
        public string NomeRua { get; set; }

        [Display(Name = "Nº")]
        public int Numero { get; set; }

        [Display(Name = "CEP")]
        public string Cep { get; set; }
       
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }
        
        [Display(Name = "Complemento")]
        public string Complemento { get; set; }
        
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }
        
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Display(Name = "País")]
        public string Pais { get; set; }
    }
}