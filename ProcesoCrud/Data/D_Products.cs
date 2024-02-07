using ProcesoCrud.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesoCrud.Data
{
    public class D_Products
    {
        public DataTable List_pr(string cText)
        {
            DataAccess data = new DataAccess();
            DataTable table = new DataTable();
            data.setParameter("@cTexto", cText);

            try
            {
                data.setProcedure("USP_LISTADO_PR");
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

        public string Save_pr( int nOpcion,E_Products newProduct)
        {
            string Resp = "";
            DataAccess data = new DataAccess();

            try
            {
                data.setProcedure("USP_GUARDAR_PR");
                data.setParameter("@Opcion",nOpcion);
                data.setParameter("nCodigo_pr", newProduct.codigo_pr);
                data.setParameter("cDescripcion_pr", newProduct.descripcion_pr);
                data.setParameter("cMarca_pr", newProduct.marca_pr);
                data.setParameter("nCodigo_me", newProduct.codigo_me);
                data.setParameter("nCodigo_cat", newProduct.codigo_cat);
                data.setParameter("nStock_actual", newProduct.stock_actual);
                Resp = data.executeAction() >= 1 ? "OK" : "No se pudo registrar los datos";
                
            }
            catch (Exception ex)
            {

                Resp = ex.Message; 
            }
            finally
            {
                data.closeConnection();
            }
            return Resp;
        }

        public string ActiveProduct(int nOpcion, bool bActiveState)
        {
            string Resp = "";
            DataAccess data = new DataAccess();
            try
            {
                data.setProcedure("ACTIVO_PR");
                data.setParameter("nCodigo_pr",nOpcion);
                data.setParameter("@bEstado_activo", bActiveState);
                Resp = data.executeAction() >= 1 ? "OK" : "No se pudo cambiar el estado activo del producto";

            }
            catch (Exception ex)
            {

                Resp = ex.Message;
            }
            finally
            {
                data.closeConnection();
            }
            return Resp;
        }
       
    }
}
