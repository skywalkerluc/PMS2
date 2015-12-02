using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entidades
{
    public class Funcionario : Usuario
    {
        public ICollection<Experiencia> Experiencias { get; set; }
        
        public string Funcao { get; set; }

        public bool PoderAdministrativo { get; set; }
    }
}
