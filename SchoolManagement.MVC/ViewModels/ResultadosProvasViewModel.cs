using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.MVC.ViewModels
{
    public class ResultadosProvasViewModel
    {
        [ScaffoldColumn(false)]
        public int ResultadoId { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public ProvaViewModel Prova { get; set; }

        public AlunoViewModel Aluno { get; set; }

        public int Nota { get; set; }

        public string Gabarito { get; set; }

        public List<int> notas { get; set; }

        public List<int> resultados { get; set; }

        public int resul { get; set; }
        public int resulAluno { get; set; }
    }
}