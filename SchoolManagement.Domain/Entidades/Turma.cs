using System.Collections.Generic;

namespace SchoolManagement.Domain.Entidades
{
    public class Turma
    {
        public int TurmaId { get; set; }
        public ICollection<Professor> Professores { get; set; }
        public ICollection<Disciplina> Disciplinas { get; set; }
        public ICollection<Aluno> Alunos { get; set; }
        public string Descricao { get; set; }
        public int HorariosTurmaId { get; set; }
        public virtual AnoLetivo AnoLetivo { get; set; }
        public int Vagas { get; set; }
        public virtual ICollection<Livro> Materiais { get; set; }
        //public Caderneta Caderneta { get; set; }

    }
}
