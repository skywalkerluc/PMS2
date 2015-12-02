using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface ILivroServico : IServicoBase<Livro>
    {
        IEnumerable<Livro> FiltrarLivro(string NomeLivro, string NomeEditora, string NomeAutor);

        IEnumerable<Livro> BuscarPorNome(string nome);
    }

}
