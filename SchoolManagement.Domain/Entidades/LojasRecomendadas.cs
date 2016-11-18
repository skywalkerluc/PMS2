namespace SchoolManagement.Domain.Entidades
{
    public class LojasRecomendadas
    {
        public int LojaId { get; set; }

        public string NomeLoja { get; set; }
        
        public Endereco Endereco { get; set; }
        
        public Contato Contato { get; set; }
    }
}
