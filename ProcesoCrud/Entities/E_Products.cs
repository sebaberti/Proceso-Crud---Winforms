using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesoCrud.Entities
{
    public class E_Products
    {
        public int codigo_pr { get; set; }

        public string descripcion_pr { get; set; }

        public string marca_pr { get; set; }

        public int codigo_me { get; set; }
        
        public int codigo_cat { get; set; }

        public decimal stock_actual{ get; set; }


    }
}
