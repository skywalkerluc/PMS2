using System.Collections.Generic;

namespace SchoolManagement.Domain.Entidades
{
    public class Responsavel : Usuario
    {
        public ICollection<Aluno> Alunos { get; set; }
        public string FuncaoTrabalhista { get; set; }
        public decimal Renda { get; set; }
    }
}
