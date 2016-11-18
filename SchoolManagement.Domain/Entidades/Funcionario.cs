using System.Collections.Generic;

namespace SchoolManagement.Domain.Entidades
{
    public class Funcionario : Usuario
    {
        public ICollection<Experiencia> Experiencias { get; set; }
        
        public string Funcao { get; set; }

        public bool PoderAdministrativo { get; set; }
    }
}
