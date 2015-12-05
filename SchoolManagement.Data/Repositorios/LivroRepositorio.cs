using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositorios
{
    public class LivroRepositorio : RepositorioBase<Livro>, ILivroRepositorio
    {
        public IEnumerable<Livro> FiltrarLivro(string NomeLivro, string NomeEditora, string NomeAutor)
        {
            //List<Livro> listaLivros = new List<Livro>();
            //var result = from d in Db.Livros
            //             where d.NomeLivro == nomeLivro || nomeLivro == string.Empty || nomeLivro == null
            //             select d;

            //if (DisciplinaId > 0)
            //{
            //    DisciplinaRepositorio disc = new DisciplinaRepositorio();
            //    var resultDisc = disc.Recuperar(DisciplinaId);

            //    if (resultDisc != null)
            //    {
            //        foreach (var itemDisc in resultDisc.Livros)
            //        {
            //            foreach (var itemLivr in result)
            //            {
            //                if (itemDisc.LivroId == itemLivr.LivroId)
            //                {
            //                    listaLivros.Add(itemLivr);
            //                }
            //            }
            //        }
            //    }
            //}
            //IEnumerable<Livro> RetornoLivro = listaLivros;
            //return RetornoLivro;

            var result = from d in Db.Livros
                         where d.Autor == NomeAutor || NomeAutor == null
                         where d.Editora == NomeEditora || NomeEditora == null
                         where d.NomeLivro == NomeLivro || NomeLivro == null
                         select d;

            IEnumerable<Livro> RetornoLivro = result;
            return RetornoLivro;
        }

        public IEnumerable<Livro> BuscarPorNome(string nome)
        {
            return Db.Livros.Where(p => p.NomeLivro.Contains(nome));
        }

        public IEnumerable<Livro> RecuperarLivrosTurma(int TurmaId)
        {
            try
            {
                var turmaParameter = new SqlParameter("@TurmaId", TurmaId);
                var query = this.Db.Livros.SqlQuery("SELECT * FROM Livro AS L WHERE L.Turma_TurmaId = @TurmaId", turmaParameter).ToList();
                return query;
            }
            catch (Exception)
            {
                throw new NotImplementedException("Erro ao recuperar materiais da turma.");
            }
        }
    }
}
