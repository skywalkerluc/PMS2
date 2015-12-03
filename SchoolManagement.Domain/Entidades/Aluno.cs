using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entidades
{
    public class Aluno : Usuario
    {
        [Key]
        [Column(Order = 20)]
        public string NumeroMatricula { get; set; }
        public Etnia Etnia { get; set; }
        public string Observacoes { get; set; }
        public ICollection<Responsavel> Responsaveis { get; set; }
        public StatusCadastro StatusCadastro { get; set; }
        public virtual Turma Turma { get; set; }
        public Frequencia Frequencia { get; set; }
    }
}
