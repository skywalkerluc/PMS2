using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositorios
{
    public class ResponsavelRepositorio : RepositorioBase<Responsavel>, IResponsavelRepositorio
    {
        public IEnumerable<Responsavel> PesquisarResponsavelPorNome(string nomeResponsavel)
        {
            return Db.Responsaveis.Where(resp => resp.Nome.Contains(nomeResponsavel));
        }

        public IEnumerable<Aluno> ExibirDadosAlunoRelacionado(int responsavelId)
        {
            var alunos = (new AlunoRepositorio().RecuperarTodos());
            List<Aluno> listaAlunosRelacionados = new List<Aluno>();

            foreach (var aluno in alunos)
            {
                foreach (var responsavelAluno in aluno.Responsaveis)
                {
                    if (responsavelAluno.Id == responsavelId)
                    {
                        listaAlunosRelacionados.Add(aluno);
                    }
                }
            }

            IEnumerable<Aluno> RetornoAlunoRelacionado = listaAlunosRelacionados;
            return RetornoAlunoRelacionado;
        }

        public IEnumerable<Responsavel> FiltrarResponsavel(string nomeResponsavel, int idAluno)
        {
            var enumResponsavel = from d in Db.Responsaveis
                                  where d.Alunos.All(a => a.Id == idAluno || idAluno == null)
                                  where d.Nome == nomeResponsavel || nomeResponsavel == null
                                  select d;

            IEnumerable<Responsavel> RetornoResponsaveis = enumResponsavel;
            return RetornoResponsaveis;
        }
    }
}
