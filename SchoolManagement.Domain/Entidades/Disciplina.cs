using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entidades
{
    public class Disciplina
    {
        [Key]
        public int DisciplinaId { get; set; }
        public string NomeDisciplina { get; set; }
        public virtual ICollection<Livro> Livros { get; set; }
        public virtual ICollection<Professor> Professores { get; set; }
        public ICollection<Turma> Turmas { get; set; }
    

        //public virtual Caderneta Caderneta { get; set; }
    }
}
