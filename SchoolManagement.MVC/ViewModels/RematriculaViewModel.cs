using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.MVC.ViewModels
{
    public class RematriculaViewModel
    {
        [ScaffoldColumn(false)]
        public int OperacaoId { get; set; }

        public AlunoViewModel Aluno { get; set; }
        public TurmaViewModel Turma { get; set; }
    }
}