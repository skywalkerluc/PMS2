using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using SchoolManagement.Data.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;

namespace SchoolManagement.Data.Repositorios
{
    public class AlunoRepositorio : RepositorioBase<Aluno>, IAlunoRepositorio
    {
        private readonly TurmaRepositorio _turmaRep = new TurmaRepositorio();

        public Aluno IncluirAluno(Aluno param)
        {
            try
            {
                Db.Alunos.Add(param);
                
                Db.SaveChanges();
                return param;

            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public IEnumerable<Aluno> PesquisarAlunoPorNome(string nomeAluno)
        {
            return Db.Alunos.Where(p => p.Nome.Contains(nomeAluno));
        }

        public IEnumerable<Aluno> PesquisarAlunoPorNomeEmTurma(string nomeAluno, int codigoTurma)
        {
            var alunosEmTurma =
                from a in Db.Alunos
                where (a.Nome == nomeAluno && a.Turma.TurmaId == codigoTurma)
                select a;

            IEnumerable<Aluno> FiltroAlunoTurma = alunosEmTurma;

            return FiltroAlunoTurma;
        }

        public IEnumerable<Frequencia> RecuperarFrequenciaAluno(Aluno aluno)
        {
            var alunoDomain = Recuperar(aluno.Id);

            var frequencia = from a in Db.Frequencia
                             where a.Aluno.Id == alunoDomain.Id
                             select a;
            return frequencia;
        }

        public IEnumerable<ResultadosProvas> RecuperarResultadosAluno(Aluno aluno)
        {
            var alunoDomain = Recuperar(aluno.Id);

            var resultados = from a in Db.ResultadosProvas
                             where a.Aluno.Id == alunoDomain.Id
                             select a;
            return resultados;
        }

        public IEnumerable<Aluno> FiltrarAluno(string nomeAluno, int? turmaId)
        {
            if (turmaId.HasValue)
            {
                var enumAlunos = from a in Db.Alunos
                                 where a.Nome.Contains(nomeAluno)
                                 join t in Db.Turmas on a.Turma.TurmaId equals t.TurmaId
                                 where t.TurmaId == turmaId
                                 select a;
                IEnumerable<Aluno> RetornoAluno = enumAlunos;
                return RetornoAluno;
            }
            else if (nomeAluno != string.Empty)
            {
                var enumAlunosNome = from a in Db.Alunos
                                     where a.Nome.Contains(nomeAluno)
                                     select a;
                IEnumerable<Aluno> RetornoAluno = enumAlunosNome;
                return RetornoAluno;
            }
            else
            {
                var enumTodos = RecuperarTodos();
                IEnumerable<Aluno> RetornoAluno = enumTodos;
                return RetornoAluno;
            }
        }

        public IEnumerable<Aluno> RecuperarAlunosTurma(int TurmaId)
        {
            var alunosTurma = from a in Db.Alunos
                              where a.Turma.TurmaId == TurmaId
                              select a;

            return alunosTurma;
        }

        public bool RematricularAlunos(ICollection<Aluno> alunos)
        {
            throw new NotImplementedException();
        }

        public Aluno RecuperarDadosAluno(int AlunoId)
        {

            SqlConnection conn = (SqlConnection)Db.Database.Connection;

            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Usuario AS U INNER JOIN Aluno AS A ON U.Id = A.Id WHERE U.Id = " + AlunoId, conn);
                conn.Open();

                Aluno aluno = new Aluno();


                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        aluno = new Aluno()
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
                            indicadorAcesso = reader.GetInt32(23),
                            Turma = _turmaRep.Recuperar(reader.GetInt32(25)),
                            Observacoes = "",
                            StatusCadastro = (StatusCadastro)reader.GetInt32(27)
                        };
                        conn.Close();
                        return aluno;
                    }
                    conn.Close();
                    return aluno;

                }
                else
                {
                    conn.Close();
                    return aluno;
                }

               
            }
            catch (Exception ex)
            {
                conn.Close();
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public static string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return string.Empty;
        }

        public bool AtualizarDadosAluno(Aluno aluno)
        {
            try
            {
                SqlParameter IdParameter = new SqlParameter("@IdParameter", aluno.Id);
                SqlParameter NomeParameter = new SqlParameter("@NomeParameter", aluno.Nome);
                SqlParameter DataNascimentoParameter = new SqlParameter("@DataNascimentoParameter", aluno.DataNascimento);
                SqlParameter RgParameter = new SqlParameter("@RgParameter", aluno.Rg);
                SqlParameter CpfParameter = new SqlParameter("@CpfParameter", aluno.Cpf);
                SqlParameter NacionalidadeParameter = new SqlParameter("@NacionalidadeParameter", aluno.Nacionalidade);
                SqlParameter NaturalidadeParameter = new SqlParameter("@NaturalidadeParameter", aluno.Naturalidade);
                SqlParameter SexoParameter = new SqlParameter("@SexoParameter", (int)aluno.Sexo);
                SqlParameter NomeRuaParameter = new SqlParameter("@NomeRuaParameter", aluno.Endereco.NomeRua);
                SqlParameter NumeroParameter = new SqlParameter("@NumeroParameter", aluno.Endereco.Numero);
                SqlParameter CepParameter = new SqlParameter("@CepParameter", aluno.Endereco.Cep);
                SqlParameter BairroParameter = new SqlParameter("@BairroParameter", aluno.Endereco.Bairro);
                SqlParameter ComplementoParameter = new SqlParameter("@ComplementoParameter", aluno.Endereco.Complemento);
                SqlParameter CidadeParameter = new SqlParameter("@CidadeParameter", aluno.Endereco.Cidade);
                SqlParameter EstadoParameter = new SqlParameter("@EstadoParameter", aluno.Endereco.Estado);
                SqlParameter PaisParameter = new SqlParameter("@PaisParameter", aluno.Endereco.Pais);
                SqlParameter EmailParameter = new SqlParameter("@EmailParameter", aluno.Contato.Email);
                SqlParameter TelefoneParameter = new SqlParameter("@TelefoneParameter", aluno.Contato.Telefone);
                SqlParameter CelularParameter = new SqlParameter("@CelularParameter", aluno.Contato.Celular);
                SqlParameter UserLoginParameter = new SqlParameter("@UserLoginParameter", aluno.UserLogin);
                SqlParameter SenhaParameter = new SqlParameter("@SenhaParameter", aluno.Senha);
                SqlParameter ObservacoesParameter = new SqlParameter("@ObservacoesParameter", aluno.Observacoes);
                SqlParameter TurmaIdParameter = new SqlParameter("@TurmaIdParameter", aluno.Turma.TurmaId);

                var query = this.Db.Database.ExecuteSqlCommand("BEGIN TRANSACTION; UPDATE Usuario SET Nome = @NomeParameter, DataNascimento = @DataNascimentoParameter, Rg = @RgParameter, Cpf = @CpfParameter, Nacionalidade = @NacionalidadeParameter, Naturalidade = @NaturalidadeParameter, Sexo = @SexoParameter, Endereco_NomeRua = @NomeRuaParameter, Endereco_Numero = @NumeroParameter, Endereco_Cep = @CepParameter, Endereco_Bairro = @BairroParameter, Endereco_Complemento = @ComplementoParameter,  Endereco_Cidade = @CidadeParameter, Endereco_Estado = @EstadoParameter,  Endereco_Pais = @PaisParameter, Contato_Email = @EmailParameter, Contato_Telefone = @TelefoneParameter, Contato_Celular = @CelularParameter, UserLogin = @UserLoginParameter, Senha = @SenhaParameter WHERE Id = @IdParameter UPDATE Aluno SET Observacoes = @ObservacoesParameter, Turma_TurmaId = @TurmaIdParameter WHERE Id = @IdParameter COMMIT;", 
                    IdParameter, NomeParameter, DataNascimentoParameter, RgParameter, CpfParameter, NacionalidadeParameter, 
                    NaturalidadeParameter, SexoParameter, NomeRuaParameter, NumeroParameter, CepParameter, BairroParameter, 
                    ComplementoParameter, CidadeParameter, EstadoParameter, PaisParameter, EmailParameter, TelefoneParameter, 
                    CelularParameter, UserLoginParameter, SenhaParameter, ObservacoesParameter, TurmaIdParameter);
                return true;

                //return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public bool RemoverAluno(int AlunoId)
        {
            try
            {
                SqlParameter AlunoIdParameter = new SqlParameter("@AlunoId", AlunoId);
                var query = this.Db.Database.ExecuteSqlCommand("BEGIN TRANSACTION; DELETE FROM ResponsavelAluno WHERE Aluno_Id = @AlunoId DELETE FROM Aluno WHERE Id = @AlunoId DELETE FROM Usuario WHERE Id = @AlunoId COMMIT;", AlunoIdParameter);
                return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
            
        }

    }
}
