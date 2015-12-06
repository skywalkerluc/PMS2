using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.MVC.ViewModels
{
    public class ResponsavelViewModel : UsuarioViewModel
    {
        public ICollection<AlunoViewModel> Alunos { get; set; }

        [Display(Name = "Função trabalhista")]
        public string FuncaoTrabalhista { get; set; }

        public decimal Renda { get; set; }

        public List<SelectListItem> ListaAlunos { get; set; }

        public List<int> alunosselecionados { get; set; }

        public int alunoSelecionado { get; set; }
    }
}