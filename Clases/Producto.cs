using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaFidelidadPaws.Clases
{
    [Serializable]
    public class Producto
    {
     
        //Atributos/Propiedades
        public string nombre { get; set; }
        public int precio { get; set; }
        public int puntos { get; set; }
        
        private int stock { get; set; }
        public string imagenUrl { get; set; }

        //Constructor
        public Producto(string nombre, int precio, int puntos, string imagenUrl)
        {
            setNombre(nombre);
            setPrecio(precio);
            setPuntos(puntos);
            setImagenUrl(imagenUrl);
        }

        //Producto para la lista 'ProductosStock' de 'PawsClub'
        public Producto(string nombreProducto, int cantidadStock)
        {
            this.nombre = nombreProducto;
            this.stock = cantidadStock;
        }

        //Getters
        public string getNombre()
        {
            return this.nombre;
        }

        public int getPrecio
        {
            get { return precio; }
        }

        public int getPuntos()
        {
            return this.puntos;
        }

        public string getImagenUrl()
        {
             return this.imagenUrl; 
        }

        public int getStock()
        {
            return this.stock;
        }


        //Setters
        public void setNombre(string nombre)
        {
            if (!string.IsNullOrEmpty(nombre))
            {
                this.nombre = nombre;
            }
            else
            {
                Console.WriteLine("El nombre no puede ser nulo o vacio.");
            }
        }

        public void setPrecio(int precio)
        {
            if (precio > 0)
            {
                this.precio = precio;
            }
            else
            {
                Console.WriteLine("El precio del producto debe ser mayor a 0.");
            }
        }

        public void setPuntos(int puntos)
        {
            if (puntos > 0)
            {
                this.puntos = puntos;
            }
            else
            {
                Console.WriteLine("Los puntos del producto deben ser mayor a 0.");
            }
        }

        public void setImagenUrl(string imagenUrl)
        {
            if (!string.IsNullOrEmpty(imagenUrl))
            {
                this.imagenUrl = imagenUrl;
            }
            else
            {
                Console.WriteLine("La url de la img no puede ser nula o vacia.");
            }
        }
    }
}