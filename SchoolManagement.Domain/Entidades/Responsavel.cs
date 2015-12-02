using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entidades
{
    public class Responsavel : Usuario
    {
        public ICollection<Aluno> Alunos { get; set; }
        public string FuncaoTrabalhista { get; set; }
        public decimal Renda { get; set; }
    }
}
