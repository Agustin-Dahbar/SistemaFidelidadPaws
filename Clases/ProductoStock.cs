using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaFidelidadPaws.Clases
{
    public class ProductoStock
    {
        private string nombreProducto;
        private int cantidadEnStock;

        public ProductoStock(string nombreProducto, int cantidad) 
        {
            this.nombreProducto = nombreProducto;
            this.cantidadEnStock = cantidad;
        }

        public string getNombreProducto()
        {
            return this.nombreProducto;
        }

        public int getCantidadEnStock()
        {
            return this.cantidadEnStock;
        }

        public void setCantidadEnStock(int cantidad)
        {
            if(cantidad > 0)
            {
                this.cantidadEnStock = cantidad;
            }
            else
            {
                Console.WriteLine("La cantidad debe ser mayor a 0.");
            }
        }
    }
}