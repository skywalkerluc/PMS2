using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.ViewModels
{
    public class FuncionarioViewModel : UsuarioViewModel
    {
        [Display(Name = "Experiência")]
        public ICollection<ExperienciaViewModel> Experiencias { get; set; }

        [Display(Name = "Função")]
        [Required(ErrorMessage = "Uma função é necessária para prosseguir.")]
        public string Funcao { get; set; }

        [Display(Name = "Poder Administrativo")]
        public bool PoderAdministrativo { get; set; }

        public List<SelectListItem> ListaDisciplinas { get; set; }
    }
}