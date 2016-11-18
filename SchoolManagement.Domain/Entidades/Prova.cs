using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Entidades
{
    public class Prova
    {
        public int ProvaId { get; set; }

        public Disciplina Disciplina { get; set; }
        public Professor Professores { get; set; }
        public DateTime DataProva { get; set; }
        public Turma Turma { get; set; }
        public int Unidade { get; set; }
        public int StatusProva { get; set; }
        public int TipoProva { get; set; }
        public List<byte> Anexos { get; set; }

    }
}
