using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entidades
{
    public class AnoLetivo
    {
        public int AnoLetivoId { get; set; }
        public int QntUnidades { get; set; }
        public ICollection<Turma> Turmas { get; set; }
        public int Ano { get; set; }

        
    }
}
