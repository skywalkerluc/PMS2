using SchoolManagement.Domain.Cross;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolManagement.Data.Repositorios.NewReps
{
    public class RepositorioBase
    {
        #region Atributos

        private SqlConnection defaultConnection;
        private SqlTransaction controlTransaction;
        private SqlCommand executionCommand;
        private List<string> outputData;

        private const string defaultConnectionString = "Data Source=SKYWALKER-PC;Initial Catalog=SchoolManagementDB;Integrated Security=True;TrustServerCertificate=True";

        #endregion

        public RepositorioBase()
        {
            defaultConnection = new SqlConnection();

            try
            {
                defaultConnection.ConnectionString = defaultConnectionString;
            }
            catch
            {
                throw new NotImplementedException("::TODO::");
            }
        }

        #region Métodos Públicos

        public bool AbrirConexao()
        {
            try
            {
                if (defaultConnection.State == 0)
                {
                    defaultConnection.Open();
                }
                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool FecharConexao()
        {
            try
            {
                if (defaultConnection.State != 0)
                {
                    defaultConnection.Close();
                }
                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool FecharTransaction()
        {
            try
            {
                controlTransaction = (SqlTransaction)defaultConnection.BeginTransaction();
                executionCommand.Transaction = controlTransaction;
                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EncerrarTransacao(bool commit)
        {
            try
            {
                if (commit)
                {
                    controlTransaction.Commit();
                }
                else
                {
                    controlTransaction.Rollback();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AdicionarParametro(string paramName, object paramValue, SqlDbType paramType)
        {
            // If not informed, the parameter's value must be considered as an Output
            ParameterDirection paramDirection = (paramValue == DBNull.Value ? ParameterDirection.Output : ParameterDirection.Input);

            AdicionarParametro(paramName, paramValue, paramDirection, paramType);
        }

        public void AdicionarParametro(string paramName, object paramValue, ParameterDirection paramDirection, SqlDbType paramType)
        {
            try
            {
                IDataParameter param;

                param = new SqlParameter(paramName, paramType);
                param.Direction = paramDirection;

                if (paramValue != DBNull.Value)
                {
                    param.Value = paramValue;
                }

                executionCommand.Parameters.Add(param);
                if (paramDirection == ParameterDirection.Output)
                {
                    outputData.Add(paramName);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            outputData = new List<string>();
            executionCommand.Parameters.Clear();
            executionCommand.Dispose();
        }

        public RetornoBase ExecutarComando(string procedure)
        {
            RetornoBase retorno = new RetornoBase();
            CamposOutput output = new CamposOutput();
            int changedRegister = 0;

            try
            {
                executionCommand.CommandText = procedure;
                executionCommand.CommandType = CommandType.StoredProcedure;
                executionCommand.Connection = defaultConnection;

                changedRegister = executionCommand.ExecuteNonQuery();

                retorno.RegistroRetorno = changedRegister;

                if (changedRegister >= 1)
                {
                    retorno.Status = true;
                    foreach (var item in outputData)
                    {
                        output.Nome = item;
                        output.Valor = executionCommand.Parameters[item].Value.ToString();

                        retorno.ListaOutput.Add(output);
                    }
                }
                else
                {
                    retorno.Status = false;
                }

                Dispose();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public int ExecutarBuscaRegistros(string procedure)
        {
            try
            {
                executionCommand.CommandText = procedure;
                executionCommand.CommandType = CommandType.StoredProcedure;
                executionCommand.Connection = defaultConnection;

                return (Int32)executionCommand.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public SqlDataReader ExecutarBuscaDados(string procedure)
        {
            try
            {
                executionCommand.CommandText = procedure;
                executionCommand.CommandType = CommandType.StoredProcedure;
                executionCommand.Connection = defaultConnection;

                return (SqlDataReader)executionCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet RecuperarResultadosDatabase(SqlDataReader dataReader, string nameDataSet)
        {
            if (dataReader == null)
            {
                return new DataSet();
            }

            var _dataSet = new DataSet(nameDataSet);

            while (!dataReader.IsClosed)
            {
                var _dataTable = new DataTable(dataReader.GetName(0));
                _dataTable.Load(dataReader);

                _dataSet.Tables.Add(_dataTable);
            }
            return _dataSet;
        }

        #endregion
    }
}
