using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entidades
{
    public class Evento
    {
        public int EventoId { get; set; }
        public string Local { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataEvento { get; set; }
        public ICollection<Funcionario> FuncionarioResponsavel { get; set; }
        public bool NecessidadeAprovacao { get; set; }
        public decimal PrecoEvento { get; set; }
        public List<string> NomeAcompanhante { get; set; }

    }
}
