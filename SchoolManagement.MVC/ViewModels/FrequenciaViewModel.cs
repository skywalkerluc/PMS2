using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SchoolManagement.MVC.ViewModels
{
    public class FrequenciaViewModel
    {
        public int FrequenciaId { get; set; }
        public AlunoViewModel Aluno { get; set; }
        public DisciplinaViewModel Disciplina { get; set; }

        [DisplayName("Data")]
        public DateTime DataReferencia { get; set; }
        [DisplayName("Frequencia")]
        public bool Presente { get; set; }

        public int resul { get; set; }
    }
}