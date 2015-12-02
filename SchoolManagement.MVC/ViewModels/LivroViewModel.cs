using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.MVC.ViewModels
{
    public class LivroViewModel
    {
        [ScaffoldColumn(false)]
        public int LivroId { get; set; }

        [Required(ErrorMessage = "Um nome de um livro é necessário para prosseguir.")]
        [Display(Name = "Nome do livro")]
        public string NomeLivro { get; set; }

        public string Autor { get; set; }
        
        public string Editora { get; set; }
    }
}