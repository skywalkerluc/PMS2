using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface ILivroServico : IServicoBase<Livro>
    {
        IEnumerable<Livro> FiltrarLivro(string NomeLivro, string NomeEditora, string NomeAutor);

        IEnumerable<Livro> BuscarPorNome(string nome);
    }

}
