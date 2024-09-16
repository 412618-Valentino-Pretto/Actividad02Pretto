using ACTV_02_PII.Data.Interfaces;
using ACTV_02_PII.Models;
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ACTV_02_PII.Data.Implementations
{
    public class ArticuloRepositoryADO : IArticuloRepository
    {
        private SqlConnection _connection;

        public ArticuloRepositoryADO()
        {
            _connection = new SqlConnection(Properties.Resources.Connection);
        }
        public bool DeleteArticulo(int id)
        {
            var parameters = new List<ParametersSQL>();
            parameters.Add(new ParametersSQL("@id", id));
            int rows = DataHelper.GetInstancia().ExecuteSPDML("BORRAR_ARTICULO",parameters);
            return rows == 1;
        }

        public List<Articulo> GetAll()
        {
            List<Articulo> lstArticulos = new List<Articulo>();
            var DataTable = DataHelper.GetInstancia().Consultar("RECUPERAR_ARTICULOS", null);

            foreach(DataRow row in DataTable.Rows)
            {
                Articulo articulo = new Articulo()
                {
                    Id = (int)row[0],
                    Nombre = (string)row[1],
                    Stock = (int)row[2],
                    EstaActivo = (bool)row[3]
                };

                lstArticulos.Add(articulo);
            }

            return lstArticulos;
        }

        public Articulo GetById(int id)
        {
            List<ParametersSQL> parameters = new List<ParametersSQL>();
            parameters.Add(new ParametersSQL("@id", id));
            

                DataTable dataTable = DataHelper.GetInstancia().ExecuteSPQuery("RECUPERAR_ARTICULO_PORID", parameters);

                if (dataTable != null)
                {
                    DataRow row = dataTable.Rows[0];
                    Articulo art = new Articulo()
                    {
                        Id = (int)row[0],
                        Nombre = (string)row[1],
                        Stock = (int)row[2],
                        EstaActivo = (bool)row[3]
                    };

                    return art;
                }
                else
                {
                    return null;
                }     
        }

        public bool UpsertArticulo(Articulo articulo)
        {
            bool result = false;
            string query = "SP_UPSERT_ARTICULO";

            try
            {
                if(articulo.Id != 0)//update
                {
                    _connection.Open();
                    var cmd = new SqlCommand(query,_connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idArticulo",articulo.Id));
                    cmd.Parameters.Add(new SqlParameter("@nombre",articulo.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@stock",articulo.Stock));
                    result = cmd.ExecuteNonQuery() == 1;
                }
                else if(articulo.Id == 0) //insert
                {
                    _connection.Open();
                    var cmd = new SqlCommand(query, _connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idArticulo", articulo.Id));
                    cmd.Parameters.Add(new SqlParameter("@nombre", articulo.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@stock", articulo.Stock));
                    result = cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (SqlException sqlEx)
            {
                result = false;
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return result;
        }
    }
}
