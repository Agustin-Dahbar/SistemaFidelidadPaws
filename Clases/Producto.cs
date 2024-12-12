using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaFidelidadPaws.Clases
{
    public class Producto
    {
        private string nombre;
        private int precio;
        private int puntos;
        private string imagenUrl;

        public Producto(string nombre, int precio, int puntos, string imagenUrl)
        {
            setNombre(nombre);
            setPrecio(precio);
            setPuntos(puntos);
            setImagenUrl(imagenUrl);
        }

        public string Nombre
        {
            get { return nombre; }
        }

        public int Precio
        {
            get { return precio; }
        }

        public int Puntos
        {
            get { return puntos; }
        }

        public string ImagenUrl
        {
            get { return imagenUrl; }
        }


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