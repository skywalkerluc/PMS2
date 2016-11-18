using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Domain.Entidades
{
    public class TrabalhosExtras
    {
        [Key]
        public int TrabalhoId { get; set; }
        public Aluno Aluno { get; set; }
        public Professor Professor { get; set; }
        public int Nota { get; set; }
        public DateTime DataProposta { get; set; }
        public DateTime DataConclusao { get; set; }
        public Turma TurmaSelecionada { get; set; }
    }
}
