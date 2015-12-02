using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entidades
{
    public class Notificacao
    {
        public int NotificacaoId { get; set; }

        public string Assunto { get; set; }
        
        public string Descricao { get; set; }
        
        public DateTime DataCriacao { get; set; }

        public Usuario UsuarioCriacao { get; set; }

        public Turma TurmaPublicoAlvo { get; set; }

        public ICollection<Professor> ProfessoresPublicoAlvo { get; set; }
    }
}
