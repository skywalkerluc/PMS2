using System;

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

    }
}
