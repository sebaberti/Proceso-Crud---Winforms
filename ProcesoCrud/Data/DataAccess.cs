using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net;

namespace ProcesoCrud.Data
{
    internal class DataAccess
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public SqlDataReader Reader
        {
            get { return reader; }
        }

        public DataAccess()
        {
            connection = new SqlConnection("server=.\\SQLEXPRESS; database = CRUD_DB; integrated security = true; User ID=user_sb; Password=Rata1234");
            command = new SqlCommand();
        }

        public void setProcedure(string sp)
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = sp;
        }

        public void setParameter(string name, object value)
        {
            command.Parameters.AddWithValue(name, value);
        }

        public void executeRead()
        {
            command.Connection = connection;
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int executeAction()
        {
            command.Connection = connection;
            try
            {
                connection.Open();
                return  command.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void closeConnection()
        {
            if(reader != null)
                reader.Close();
                connection.Close();
        }

    }
}
