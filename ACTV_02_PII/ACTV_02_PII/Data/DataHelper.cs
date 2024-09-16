using System.Data;
using System.Data.SqlClient;

namespace ACTV_02_PII.Data
{
    public class DataHelper
    {
        private static DataHelper instancia = null;
        private SqlConnection _connection;

        private DataHelper()
        {
            _connection = new SqlConnection(Properties.Resources.Connection);
        }

        public static DataHelper GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new DataHelper();

            }
            return instancia;
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }


        public DataTable Consultar(string nombreSP, List<SqlParameter>? parameters = null)
        {
            _connection.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = _connection;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = nombreSP;

            if (parameters != null)
            {
                foreach (var param in parameters)
                    comando.Parameters.AddWithValue(param.ParameterName, param.Value);
            }

            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            _connection.Close();
            return tabla;
        }

        public DataTable ExecuteSPQuery(string sp, List<ParametersSQL>? parametros)
        {
            DataTable t = new DataTable();
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var param in parametros)
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                }

                t.Load(cmd.ExecuteReader());
                _connection.Close();
            }
            catch (SqlException)
            {
                t = null;
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return t;
        }


        public int ExecuteSPDML(string sp, List<ParametersSQL>? parametros)
        {
            int rows;
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var param in parametros)
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                }

                rows = cmd.ExecuteNonQuery();
                _connection.Close();
            }
            catch (SqlException)
            {
                rows = 0;
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return rows;
        }
    }
}
