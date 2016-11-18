using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface ILivroRepositorio : IRepositorioBase<Livro>
    {
        IEnumerable<Livro> FiltrarLivro(string NomeLivro, string NomeEditora, string NomeAutor);

        IEnumerable<Livro> BuscarPorNome(string nome);
    }
}
