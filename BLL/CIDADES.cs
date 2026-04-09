using System;
using System.Collections.Generic;
using System.Data;

namespace BLL
{
    public class CIDADES
    {
        #region Campos

        private const string SELECT = @"
            SELECT COD_CIDADE, COD_UF, NOME FROM DEV.CIDADES
        ";

        #endregion

        #region Métodos

        /// <summary>
        /// Carrega um registro a partir do seu ID.
        /// </summary>
        /// <param name="id">Chave primária</param>
        /// <returns>DAO.CIDADES</returns>
        public DAO.CIDADES Carregar(decimal id)
        {
            string sql = SELECT + " WHERE COD_CIDADE = :COD_CIDADE ";
            var parametros = new System.Data.Common.DbParameter[1];
            parametros[0] = new Oracle.ManagedDataAccess.Client.OracleParameter("COD_CIDADE", id);
            var dataTable = new DAO.ConexaoBD().Executar(sql, CommandType.Text, parametros);

            if (dataTable.Rows.Count > 0)
            {
                return DAO.Conversor.ConverterParaObjeto<DAO.CIDADES>(dataTable);
            }

            return null;
        }

        /// <summary>
        /// Adiciona um novo registro e retorna o novo objeto adicionado.
        /// </summary>
        /// <param name="entidade">DAO.CIDADES</param>
        /// <returns>UF</returns>
        public DAO.CIDADES Adicionar(DAO.CIDADES entidade)
        {
            var parametros = new System.Data.Common.DbParameter[3];
            parametros[0] = new Oracle.ManagedDataAccess.Client.OracleParameter("out_COD_CIDADE", -1);
            parametros[1] = new Oracle.ManagedDataAccess.Client.OracleParameter("in_COD_UF", entidade.COD_UF);
            parametros[2] = new Oracle.ManagedDataAccess.Client.OracleParameter("in_NOME", entidade.NOME);
            parametros[0].Direction = ParameterDirection.Output;
            new DAO.ConexaoBD().Executar("DEV.DML_CIDADES.INS_CIDADES", CommandType.StoredProcedure, "out_COD_CIDADE", parametros);
            return Carregar(Convert.ToInt32(parametros[0].Value));
        }

        /// <summary>
        /// Edita um registro da entidade [UF].
        /// </summary>
        /// <param name="entidade">UF</param>
        public void Editar(DAO.CIDADES entidade)
        {
            var parametros = new System.Data.Common.DbParameter[3];
            parametros[0] = new Oracle.ManagedDataAccess.Client.OracleParameter("in_COD_CIDADE", entidade.COD_CIDADE);
            parametros[1] = new Oracle.ManagedDataAccess.Client.OracleParameter("in_COD_UF", entidade.COD_UF);
            parametros[2] = new Oracle.ManagedDataAccess.Client.OracleParameter("in_NOME", entidade.NOME);
            new DAO.ConexaoBD().ExecutarSemRetorno("DEV.DML_CIDADES.UPD_CIDADES", CommandType.StoredProcedure, parametros);
        }

        /// <summary>
        /// Remove um registro da entidade [UF] a partir da sua chave primária (PK).
        /// </summary>
        /// <param name="id">Chave primária</param>
        public void Excluir(decimal id)
        {
            var parametros = new System.Data.Common.DbParameter[1];
            parametros[0] = new Oracle.ManagedDataAccess.Client.OracleParameter("in_COD_CIDADE", id);
            new DAO.ConexaoBD().ExecutarSemRetorno("DEV.DML_CIDADES.DEL_CIDADES", CommandType.StoredProcedure, parametros);
        }

        /// <summary>
        /// Carrega todos os itens da entidade.
        /// </summary>
        /// <returns>List(DAO.CIDADES)</returns>
        public List<DAO.CIDADES> CarregarTodos()
        {
            var dataTable = new DAO.ConexaoBD().Executar(SELECT, CommandType.Text);

            if (dataTable.Rows.Count > 0)
            {
                return DAO.Conversor.ConverterParaLista<DAO.CIDADES>(dataTable);
            }

            return null;
        }

        /// <summary>
        /// Carrega todos os itens da entidade por UF
        /// </summary>
        /// <param name="codigoUF">Código da UF</param>
        /// <returns>List(DAO.CIDADES)</returns>
        public List<DAO.CIDADES> CarregarPorUF(decimal codigoUF) // <----------------------------------------------- Ei! Veja esse método aqui.
        {
            // Parece estar faltando algo nessa consulta SQL.
            // Dica: Observe o método Carregar pra ter uma iéia do que fazer! =D
            string sql = SELECT + " WHERE COD_UF = :COD_UF ";

            // Parece que estão faltando alguns código aqui...
            var parametros = new System.Data.Common.DbParameter[1];
            parametros[0] = new Oracle.ManagedDataAccess.Client.OracleParameter("COD_UF", codigoUF);

            var dataTable = new DAO.ConexaoBD().Executar(sql, CommandType.Text, parametros);

            if (dataTable.Rows.Count > 0)
            {
                return DAO.Conversor.ConverterParaLista<DAO.CIDADES>(dataTable);
            }

            return null;
        }

        #endregion
    }
}
