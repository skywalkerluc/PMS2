using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entidades
{
    public class ResultadosProvas
    {
        public int ResultadoId { get; set; }
        public string Observacao { get; set; }
        public Prova Prova { get; set; }
        public Aluno Aluno { get; set; }
        public int Nota { get; set; }
        public string Gabarito { get; set; }

    }
}
