using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesoCrud.Data
{
    public class D_Categories
    {
        public DataTable List_cat()
        {
            DataAccess data = new DataAccess();
            DataTable table = new DataTable();

            try
            {
                data.setProcedure("USP_LISTADO_CAT");
                data.executeRead();

                
                
                    table.Load(data.Reader);
                
                return table;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                data.closeConnection();
            }

        }
    }
}
