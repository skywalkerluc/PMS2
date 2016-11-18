using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using SchoolManagement.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Servicos
{
    public class LivroServico : ServicoBase<Livro>, ILivroServico
    {
        private readonly ILivroRepositorio _livroRep;

        public LivroServico(ILivroRepositorio livroRep)
            :base(livroRep)
        {
            _livroRep = livroRep;
        }

        public IEnumerable<Livro> FiltrarLivro(string NomeLivro, string NomeEditora, string NomeAutor)
        {
            return _livroRep.FiltrarLivro(NomeLivro, NomeEditora, NomeAutor);
        }

        public IEnumerable<Livro> BuscarPorNome(string nome)
        {
            return this._livroRep.BuscarPorNome(nome);
        }

        
    }
}
