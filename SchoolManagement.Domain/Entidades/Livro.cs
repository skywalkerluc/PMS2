using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entidades
{
    public class Livro
    {
        public int LivroId { get; set; }
        public string NomeLivro { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
    }
}
