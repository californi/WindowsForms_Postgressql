using Npgsql;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcessoADados
{
    public static class AcessoDadosPostgres
    {
        private static NpgsqlConnection Conexao;

        private static NpgsqlConnection CriarConexao()
        {
            Conexao = new NpgsqlConnection(@"Server=127.0.0.1;Port=5432;Database=ifsp;User Id=postgres;Password=1234;");
            Conexao.Open();
            return Conexao;
        }

        public static void FecharConexao()
        {
            if (Conexao != null)
            {
                Conexao.Close();
                Conexao = null;
            }
        }


        private static NpgsqlParameterCollection objParametros = new NpgsqlCommand().Parameters;

        public static void LimparParametros()
        {
            objParametros.Clear();
        }

        public static void AdicionarParametros(string strNomeParametro, object objValor)
        {
            objParametros.Add(new NpgsqlParameter(strNomeParametro, objValor));
        }

        //Inserir, Alterar e Excluir
        public static object ExecutarManipulacao(CommandType objTipo, string strSql)
        {
            try
            {
                //SP = Stored Procedure (Procedimento Armazenado no MySQL)
                //strSql => é o comando SQL ou o nome da SP
                if (Conexao == null)
                    Conexao = CriarConexao();

                NpgsqlConnection objConexao = Conexao;

                NpgsqlCommand objComando = objConexao.CreateCommand();
                //Informa se será executada uma SP ou um texto SQL
                objComando.CommandType = objTipo;
                objComando.CommandText = strSql;
                objComando.CommandTimeout = 999999999; //Segundos

                //Adicionar os parâmetros para ir para o banco Sql Server
                foreach (NpgsqlParameter objParametro in objParametros)
                    objComando.Parameters.Add(new NpgsqlParameter(objParametro.ParameterName, objParametro.Value));

                return objComando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Consultar registros do banco de dados
        public static DataTable ExecutaConsultar(CommandType objTipo, string strSql)
        {
            try
            {
                if (Conexao == null)
                    Conexao = CriarConexao();

                NpgsqlConnection objConexao = Conexao;
                NpgsqlCommand objComando = objConexao.CreateCommand();
                objComando.CommandType = objTipo;
                objComando.CommandText = strSql;
                objComando.CommandTimeout = 999999999;

                foreach (NpgsqlParameter objParametro in objParametros)
                    objComando.Parameters.Add(new NpgsqlParameter(objParametro.ParameterName, objParametro.Value));

                NpgsqlDataAdapter objAdaptador = new NpgsqlDataAdapter(objComando);
                DataTable objTabelaRecebeDados = new DataTable();

                objAdaptador.Fill(objTabelaRecebeDados);

                return objTabelaRecebeDados;
            }
            catch (Exception objErro)
            {
                throw new Exception(objErro.Message);
            }

        }

    }
}
