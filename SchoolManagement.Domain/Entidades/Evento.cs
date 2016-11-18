using System;

namespace SchoolManagement.Domain.Entidades
{
    public class Evento
    {
        public int EventoId { get; set; }
        public string Local { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataEvento { get; set; }
        public Funcionario FuncionarioResponsavel { get; set; }
        public bool NecessidadeAprovacao { get; set; }
        public decimal PrecoEvento { get; set; }
        public string NomeAcompanhante { get; set; }

    }
}
