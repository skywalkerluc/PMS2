using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //public Caderneta Caderneta { get; set; }

    }
}
