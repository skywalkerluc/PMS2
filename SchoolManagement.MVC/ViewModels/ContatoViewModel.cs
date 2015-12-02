using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.MVC.ViewModels
{
    public class ContatoViewModel
    {
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Preencha um e-mail válido.")]
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
    }
}