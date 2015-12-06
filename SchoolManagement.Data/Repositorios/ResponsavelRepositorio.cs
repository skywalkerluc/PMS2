using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositorios
{
    public class ResponsavelRepositorio : RepositorioBase<Responsavel>, IResponsavelRepositorio
    {
        public Responsavel IncluirResponsavel(Responsavel responsavel)
        {
            try
            {
                if (responsavel.Alunos != null)
                {
                    foreach (var aluno in responsavel.Alunos)
                    {
                        Db.Entry(aluno).State = EntityState.Unchanged;
                    }
                }
                Db.Responsaveis.Add(responsavel);
                Db.SaveChanges();
                return responsavel;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public bool CriarRelacaoResponsavelAluno(int ResponsavelId, int AlunoId)
        {
            try
            {
                var ResponsavelIdParameter = new SqlParameter("@ResponsavelId", ResponsavelId);
                var AlunoIdParameter = new SqlParameter("@AlunoId", AlunoId);
                var query = this.Db.Database.ExecuteSqlCommand("UPDATE ResponsavelAluno SET Aluno_Id = @AlunoId, Responsavel_Id = @ResponsavelId", ResponsavelIdParameter, AlunoIdParameter);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Responsavel> PesquisarResponsavelPorNome(string nomeResponsavel)
        {
            return Db.Responsaveis.Where(resp => resp.Nome.Contains(nomeResponsavel));
        }

        public IEnumerable<Aluno> ExibirDadosAlunoRelacionado(int responsavelId)
        {
            //var alunos = (new AlunoRepositorio().RecuperarTodos());
            //List<Aluno> listaAlunosRelacionados = new List<Aluno>();

            //foreach (var aluno in alunos)
            //{
            //    foreach (var responsavelAluno in aluno.Responsaveis)
            //    {
            //        if (responsavelAluno.Id == responsavelId)
            //        {
            //            listaAlunosRelacionados.Add(aluno);
            //        }
            //    }
            //}

            //IEnumerable<Aluno> RetornoAlunoRelacionado = listaAlunosRelacionados;
            //return RetornoAlunoRelacionado;

            try
            {
                var ResponsavelIdParameter = new SqlParameter("@ResponsavelId", responsavelId);
                var query = this.Db.Set<Aluno>().SqlQuery("SELECT U.Id, U.Nome FROM Usuario AS U INNER JOIN Aluno AS A ON U.Id = A.Id INNER JOIN ResponsavelAluno AS RA ON A.Id = RA.Aluno_Id WHERE RA.Responsavel_Id = @ResponsavelId", ResponsavelIdParameter).ToList();
                return query;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
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

        public List<Dictionary<int, string>> ExibirDadosAlunoRelacionado2(int responsavelId)
        {
            try
            {
                List<Dictionary<int, string>> ListaRetorno = new List<Dictionary<int, string>>();
                //var ResponsavelIdParameter = new SqlParameter("@ResponsavelId", responsavelId);
                //var query = this.Db.Database.ExecuteSqlCommand("SELECT U.Id, U.Nome FROM Usuario AS U INNER JOIN Aluno AS A ON U.Id = A.Id INNER JOIN ResponsavelAluno AS RA ON A.Id = RA.Aluno_Id WHERE RA.Responsavel_Id = @ResponsavelId", ResponsavelIdParameter);

                var alunos = from a in Db.Alunos
                             where a.Responsaveis.All(w => w.Id.Equals(responsavelId))
                             select a;

                if (alunos.ToList().Count > 0)
                {
                    foreach (var aluno in alunos)
                    {
                        Dictionary<int, string> dict = new Dictionary<int, string>();
                        dict.Add(aluno.Id, aluno.Nome);
                        ListaRetorno.Add(dict);
                    }
                    return ListaRetorno;
                }
                else
                {
                    throw new NotImplementedException("Erro ao recuperar dados de alunos");
                }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }
    }
}
