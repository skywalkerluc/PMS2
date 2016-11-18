using System.Collections.Generic;

namespace SchoolManagement.Domain.Entidades
{
    public class Professor : Funcionario
    {
        public virtual ICollection<Disciplina> Disciplinas { get; set; }
        public string Especialidade { get; set; }
        public ICollection<Turma> Turmas { get; set; }
    }
}
