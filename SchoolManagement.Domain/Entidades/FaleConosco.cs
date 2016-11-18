using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Domain.Entidades
{
    public class FaleConosco
    {
        [Key]
        public int ContatoId { get; set; }
        public string Nome { get; set; }
        public string Mensagem { get; set; }
        public TipoContato TipoContato { get; set; }
        public string Email { get; set; }
        public DateTime DataHoraEnvio { get; set; }
    }
}
