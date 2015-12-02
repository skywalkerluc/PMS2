using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.MVC.ViewModels
{
    public class BoletoViewModel
    {
        [ScaffoldColumn(false)]
        public int BoletoId { get; set; }

        public AlunoViewModel Aluno { get; set; }

        [Display(Name = "Data de Vencimento")]
        public DateTime DataVencimento { get; set; }

        [Display(Name = "Data e hora de criação")]
        public DateTime DataHoraCriacao { get; set; }

        public string Anexo { get; set; }

        [Display(Name = "Data de pagamento")]
        public DateTime DataPagamento { get; set; }

        [Display(Name = "Status de pagamento")]
        public StatusPagamento StatusPagamento { get; set; }
    }
}