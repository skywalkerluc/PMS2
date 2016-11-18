using System;

namespace SchoolManagement.Domain.Entidades
{
    public class Boleto
    {
        public int BoletoId { get; set; }
        public Aluno Aluno { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataHoraCriacao { get; set; }
        public string Anexo { get; set; }
        public DateTime DataPagamento { get; set; }
        public StatusPagamento StatusPagamento { get; set; }
    }
}
