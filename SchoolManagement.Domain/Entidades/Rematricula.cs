using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entidades
{
    public class Rematricula
    {
        public int OperacaoId { get; set; }
        public Aluno Aluno { get; set; }
        public Turma Turma { get; set; }
    }
}
