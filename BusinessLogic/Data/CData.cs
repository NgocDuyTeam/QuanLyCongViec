using BusinessLogic.Model;
using Framework.Extensions;
using SQLDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class CData
    {
        private bool connectStatus = true;
        public bool ConnectStatus
        {
            get { return connectStatus; }
            set { connectStatus = value; }
        }
        private SqlConnection _conn;
        public SqlConnection conn
        {
            get { return _conn; }
            set { _conn = value; }
        }

        public CData()
        {
            try
            {
                if (conn == null || conn.State == ConnectionState.Closed)
                    initConnection();
            }
            catch (SqlException e)
            {
                ConnectStatus = false;
            }
        }

        private void initConnection()
        {        
            if (_conn == null)
            {
                string strConn = ConfigurationUtility.GetConnectionStringValue("connectionString");
                _conn = new SqlConnection(strConn);
            }

            if (_conn != null && _conn.State != ConnectionState.Open)
            {
                _conn.Open();
                ConnectStatus = true;
            }
        }
        public void Dispose()
        {
            if (_conn != null || _conn.State != ConnectionState.Closed)
            {
                _conn.Close();
                ConnectStatus = false;
            }
        }
        protected void PrepareCommand(SqlCommand cmd, string sql, List<SqlParameter> paramlist, List<SqlParameter> optParamList)
        {
            // Init SqlCommand
            if (conn == null || conn.State == ConnectionState.Closed)
                initConnection();
            cmd.CommandTimeout = 1000;
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            // Process paramlist
            if (paramlist != null)
            {
                foreach (SqlParameter param in paramlist)
                {
                    cmd.Parameters.Add(param);
                }
            }
            // Process output paramlist
            if (optParamList != null)
            {
                foreach (SqlParameter param in optParamList)
                {
                    cmd.Parameters.Add(param);
                }
            }
        }
        public DataSet ExecuteDSQuery(string sql, List<SqlParameter> paramlist)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, sql, paramlist, null);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            cmd.Dispose();
            adapter.Dispose();
            return dataset;
        }

        public DataSet ExecuteDSQuery(string sql, List<SqlParameter> paramlist
            , List<SqlParameter> optParamList, out Dictionary<string, object> optValues)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, sql, paramlist, optParamList);
            SqlDataAdapter adapter = new SqlDataAdapter
            {
                SelectCommand = cmd
            };
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            // Ouput values
            var vOptValues = new Dictionary<string, object>();
            foreach (var optParam in optParamList)
            {
                vOptValues.Add(optParam.ParameterName, Convert.ToString(optParam.Value));
            }
            optValues = vOptValues;
            cmd.Dispose();
            adapter.Dispose();
            return dataset;
        }
    }
}
