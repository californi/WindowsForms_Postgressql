using AcessoADados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcessaDadosPostgreSQL
{
    public class BandaDAO
    {
        public List<Banda> List()
        {
            List<Banda> lista = new List<Banda>();
            try
            {
                DataTable objDataTable = null;
                //Se quiser personalizar a busca
                string strSQL = "select cod_banda, descricao_banda from tblBanda";
                objDataTable = AcessoDadosPostgres.ExecutaConsultar(CommandType.Text, strSQL);
                if (objDataTable.Rows.Count <= 0)
                {
                    return lista;
                }

                foreach (DataRow objLinha in objDataTable.Rows)
                {
                    Banda objNovaBanda = new Banda();
                    objNovaBanda.Codigo = objLinha["cod_banda"] != DBNull.Value ? Convert.ToInt32(objLinha["cod_banda"]) : 0;
                    objNovaBanda.Descricao = objLinha["descricao_banda"] != DBNull.Value ? Convert.ToString(objLinha["descricao_banda"]) : "";

                    lista.Add(objNovaBanda);
                }
                return lista;
            }
            catch (Exception)
            {
                return lista;
            }
        }

        public bool Create(Banda banda)
        {
            try
            {
                AcessoDadosPostgres.LimparParametros();

                object objRetorno = null;
                if (banda != null)
                {
                    //AcessoDados.AcessoDadosPostgreSQL.AdicionarParametros("@intCod_banda", banda.Codigo);
                    AcessoDadosPostgres.AdicionarParametros("@vchDescricao_banda", banda.Descricao);

                    string strSQL = "insert into tblBanda (descricao_banda) values ( @vchDescricao_banda); select CURRVAL('banda_cod_seq');";
                    objRetorno = AcessoDadosPostgres.ExecutarManipulacao(CommandType.Text, strSQL);
                }

                int intResultado = 0;
                if (objRetorno != null)
                {
                    if (int.TryParse(objRetorno.ToString(), out intResultado))
                        return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Edit(Banda banda)
        {
            try
            {
                AcessoDadosPostgres.LimparParametros();

                object objRetorno = null;
                if (banda != null)
                {
                    AcessoDadosPostgres.AdicionarParametros("@intCod_banda", banda.Codigo);
                    AcessoDadosPostgres.AdicionarParametros("@vchDescricao_banda", banda.Descricao);

                    string strSQL = "update tblBanda set descricao_banda = @vchDescricao_banda where cod_banda = @intCod_banda; select @intCod_banda;";
                    objRetorno = AcessoDadosPostgres.ExecutarManipulacao(CommandType.Text, strSQL);
                }

                int intResultado = 0;
                if (objRetorno != null)
                {
                    if (int.TryParse(objRetorno.ToString(), out intResultado))
                        return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Banda banda)
        {
            try
            {
                AcessoDadosPostgres.LimparParametros();

                object objRetorno = null;
                if (banda != null)
                {
                    AcessoDadosPostgres.AdicionarParametros("@intCod_banda", banda.Codigo);

                    string strSQL = "delete from tblBanda where cod_banda = @intCod_banda; select @intCod_banda;";
                    objRetorno = AcessoDadosPostgres.ExecutarManipulacao(CommandType.Text, strSQL);
                }

                int intResultado = 0;
                if (objRetorno != null)
                {
                    if (int.TryParse(objRetorno.ToString(), out intResultado))
                        return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
