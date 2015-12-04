using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entidades
{
    public class Professor : Funcionario
    {
        public string Matricula { get; set; }
        public virtual ICollection<Disciplina> Disciplinas { get; set; }
        public string Especialidade { get; set; }
        public ICollection<Turma> Turmas { get; set; }
    }
}
