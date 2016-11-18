using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Domain.Entidades
{
    public class ConteudosExtras
    {
        [Key]
        public int ConteudoId { get; set; }

        public DateTime DataHoraCriacao { get; set; }
        public Professor Professor { get; set; }
        public string Anexo { get; set; }
        public Turma TurmaPublicoAlvo { get; set; }
        public string Descricao { get; set; }
    }
}
