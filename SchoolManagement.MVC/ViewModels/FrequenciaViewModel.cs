using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement.MVC.ViewModels
{
    public class FrequenciaViewModel
    {
        public int FrequenciaId { get; set; }
        public AlunoViewModel Aluno { get; set; }
        public DisciplinaViewModel Disciplina { get; set; }
        public DateTime DataReferencia { get; set; }
        public bool Presente { get; set; }
    }
}