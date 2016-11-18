using System;

namespace SchoolManagement.Domain.Entidades
{
    public class Frequencia
    {
        public int FrequenciaId { get; set; }
        public Aluno Aluno { get; set; }
        public Disciplina Disciplina { get; set; }
        public DateTime DataReferencia { get; set; }
        public bool Presente { get; set; }

    }
}
