using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface ILivroRepositorio : IRepositorioBase<Livro>
    {
        IEnumerable<Livro> FiltrarLivro(string NomeLivro, string NomeEditora, string NomeAutor);

        IEnumerable<Livro> BuscarPorNome(string nome);
    }
}
