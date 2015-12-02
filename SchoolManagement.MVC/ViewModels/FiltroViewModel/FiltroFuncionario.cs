using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.WebPages.Html;

namespace SchoolManagement.MVC.ViewModels.FiltroViewModel
{
    public class FiltroFuncionario
    {
        [Display(Name = "Nome do funcionário")]
        public string NomeFuncionario { get; set; }

        [Display(Name = "Função")]
        public int? Funcao { get; set; }

        public int FuncaoSelecionada { get; set; }

        public List<SelectListItem> ListaFuncoes { get; set; }
    }

    //0 string.empty
    //1 prof
    //2 outros
}