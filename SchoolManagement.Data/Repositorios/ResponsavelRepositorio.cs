using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace SchoolManagement.Data.Repositorios
{
    public class ResponsavelRepositorio : RepositorioBase<Responsavel>/*, IResponsavelRepositorio*/
    {
        private readonly AlunoRepositorio _alunoRep;

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
                                  where d.Alunos.All(a => a.Id == idAluno)
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

        public bool RemoverResponsavel(int ResponsavelId)
        {
            try
            {
                var ResponsavelIdParameter = new SqlParameter("@ResponsavelId", ResponsavelId);
                var query = this.Db.Database.ExecuteSqlCommand("", ResponsavelIdParameter);
                if (query.GetTypeCode() != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public Responsavel RecuperarDadosResponsavel(int ResponsavelId)
        {
            try
            {
                SqlConnection conn = (SqlConnection)Db.Database.Connection;
                SqlCommand command = new SqlCommand("SELECT * FROM Usuario AS U INNER JOIN Responsavel AS R ON U.Id = R.Id INNER JOIN ResponsavelAluno AS RA ON R.Id = RA.Responsavel_Id WHERE U.Id = @ResponsavelId" + ResponsavelId, conn);
                conn.Open();

                Responsavel resp = new Responsavel();


                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        List<Aluno> ListaAlunos = new List<Aluno>();
                        Aluno aluno = new Aluno();
                        aluno = (new AlunoRepositorio().RecuperarDadosAluno(reader.GetInt32(27)));
                        ListaAlunos.Add(aluno);

                        resp = new Responsavel()
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            DataNascimento = reader.GetDateTime(2),
                            DataCadastro = reader.GetDateTime(3),
                            Rg = reader.GetString(4),
                            Cpf = reader.GetString(5),
                            Nacionalidade = reader.GetString(6),
                            Naturalidade = reader.GetString(7),
                            Sexo = (Sexo)reader.GetInt32(9),
                            Endereco = new Endereco()
                            {
                                NomeRua = reader.GetString(10),
                                Numero = reader.GetInt32(11),
                                Cep = reader.GetString(12),
                                Bairro = reader.GetString(13),
                                Complemento = reader.GetString(14),
                                Cidade = reader.GetString(15),
                                Estado = reader.GetString(16),
                                Pais = reader.GetString(17)
                            },
                            Contato = new Contato()
                            {
                                Email = reader.GetString(18),
                                Telefone = reader.GetString(19),
                                Celular = reader.GetString(20)
                            },
                            UserLogin = reader.GetString(21),
                            Senha = reader.GetString(22),
                            FuncaoTrabalhista = reader.GetString(25),
                            Renda = reader.GetDecimal(26),
                            Alunos = ListaAlunos
                        };
                        return resp;
                    }
                    return resp;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            catch(Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public bool AtualizarDadosResponsavel(Responsavel responsavel)
        {
            try
            {
                SqlParameter IdParameter = new SqlParameter("@IdParameter", responsavel.Id);
                SqlParameter NomeParameter = new SqlParameter("@NomeParameter", responsavel.Nome);
                SqlParameter DataNascimentoParameter = new SqlParameter("@DataNascimentoParameter", responsavel.DataNascimento);
                SqlParameter RgParameter = new SqlParameter("@RgParameter", responsavel.Rg);
                SqlParameter CpfParameter = new SqlParameter("@CpfParameter", responsavel.Cpf);
                SqlParameter NacionalidadeParameter = new SqlParameter("@NacionalidadeParameter", responsavel.Nacionalidade);
                SqlParameter NaturalidadeParameter = new SqlParameter("@NaturalidadeParameter", responsavel.Naturalidade);
                SqlParameter SexoParameter = new SqlParameter("@SexoParameter", (int)responsavel.Sexo);
                SqlParameter NomeRuaParameter = new SqlParameter("@NomeRuaParameter", responsavel.Endereco.NomeRua);
                SqlParameter NumeroParameter = new SqlParameter("@NumeroParameter", responsavel.Endereco.Numero);
                SqlParameter CepParameter = new SqlParameter("@CepParameter", responsavel.Endereco.Cep);
                SqlParameter BairroParameter = new SqlParameter("@BairroParameter", responsavel.Endereco.Bairro);
                SqlParameter ComplementoParameter = new SqlParameter("@ComplementoParameter", responsavel.Endereco.Complemento);
                SqlParameter CidadeParameter = new SqlParameter("@CidadeParameter", responsavel.Endereco.Cidade);
                SqlParameter EstadoParameter = new SqlParameter("@EstadoParameter", responsavel.Endereco.Estado);
                SqlParameter PaisParameter = new SqlParameter("@PaisParameter", responsavel.Endereco.Pais);
                SqlParameter EmailParameter = new SqlParameter("@EmailParameter", responsavel.Contato.Email);
                SqlParameter TelefoneParameter = new SqlParameter("@TelefoneParameter", responsavel.Contato.Telefone);
                SqlParameter CelularParameter = new SqlParameter("@CelularParameter", responsavel.Contato.Celular);
                SqlParameter UserLoginParameter = new SqlParameter("@UserLoginParameter", responsavel.UserLogin);
                SqlParameter SenhaParameter = new SqlParameter("@SenhaParameter", responsavel.Senha);
                SqlParameter FuncaoTrabalhistaParameter = new SqlParameter("FuncaoTrabalhistaParameter", responsavel.FuncaoTrabalhista);
                SqlParameter RendaParameter = new SqlParameter("@RendaParameter", responsavel.Renda);

                var query = this.Db.Database.ExecuteSqlCommand("BEGIN TRANSACTION; UPDATE Responsavel SET FuncaoTrabalhista = @FuncaoTrabalhistaParameter, Renda = @RendaParameter WHERE Id = @IdParameter UPDATE Usuario SET Nome = @NomeParameter, DataNascimento = @DataNascimentoParameter, Rg = @RgParameter, Cpf = @CpfParameter, Nacionalidade = @NacionalidadeParameter, Naturalidade = @NaturalidadeParameter, Sexo = @SexoParameter, Endereco_NomeRua = @NomeRuaParameter, Endereco_Numero = @NumeroParameter, Endereco_Cep = @CepParameter, Endereco_Bairro = @BairroParameter, Endereco_Complemento = @ComplementoParameter, Endereco_Cidade = @CidadeParameter, Endereco_Estado = @EstadoParameter, Endereco_Pais = @PaisParameter, Contato_Email = @EmailParameter, Contato_Telefone = @TelefoneParameter, Contato_Celular = @CelularParameter, UserLogin = @UserLoginParameter, Senha = @SenhaParameter WHERE Id = @IdParameter COMMIT;",
                    IdParameter, NomeParameter, DataNascimentoParameter, RgParameter, CpfParameter, NacionalidadeParameter,
                    NaturalidadeParameter, SexoParameter, NomeRuaParameter, NumeroParameter, CepParameter, BairroParameter,
                    ComplementoParameter, CidadeParameter, EstadoParameter, PaisParameter, EmailParameter, TelefoneParameter,
                    CelularParameter, UserLoginParameter, SenhaParameter, FuncaoTrabalhistaParameter, RendaParameter);
                return true;

                //return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }
    }
}
