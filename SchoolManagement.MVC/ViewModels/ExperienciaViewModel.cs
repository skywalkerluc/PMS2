using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.MVC.ViewModels
{
    public class ExperienciaViewModel
    {
        [ScaffoldColumn(false)]
        public int ExperienciaId { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Função")]
        public string Funcao { get; set; }

        [Display(Name = "Ano de Entrada")]
        public DateTime AnoEntrada { get; set; }

        [Display(Name = "Ano de Saída")]
        public DateTime AnoSaida { get; set; }

        public virtual FuncionarioViewModel Funcionario { get; set; }
    }
}