namespace SchoolManagement.Domain.Entidades
{
    public class Rematricula
    {
        public int OperacaoId { get; set; }
        public Aluno Aluno { get; set; }
        public Turma Turma { get; set; }
    }
}
