using System.Collections.Generic;

namespace SchoolManagement.Domain.Entidades
{
    public class Aluno : Usuario
    {
        public string Observacoes { get; set; }
        public ICollection<Responsavel> Responsaveis { get; set; }
        public StatusCadastro StatusCadastro { get; set; }
        public virtual Turma Turma { get; set; }
    }
}
