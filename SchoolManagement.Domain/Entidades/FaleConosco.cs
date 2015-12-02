using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
