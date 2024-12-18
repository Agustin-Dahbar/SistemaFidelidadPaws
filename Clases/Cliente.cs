using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaFidelidadPaws.Clases
{
    public class Cliente
    {
        //Propiedades/atributos
        private string nombreUsuario;
        private string contraseña;
        private List<Producto> productosAdquiridos;
        private int puntos;
        private int saldo;

        //Constructor
        public Cliente(string nombreUsuario, string contraseña)
        {
            setNombreUsuario(nombreUsuario);
            setContraseña(contraseña);
            this.productosAdquiridos = new List<Producto>();
            this.puntos = 0;
        }

        //Constructor que posiblita asignarle puntos al cliente desde que se instancia.
        public Cliente(string nombreUsuario, string contraseña, int puntos)
        {
            setNombreUsuario(nombreUsuario);
            setContraseña(contraseña);
            this.productosAdquiridos = new List<Producto>();
            setPuntos(puntos);
        }

        //Constructor con saldo
        public Cliente(string nombreUsuario, string contraseña, int puntos, int saldo)
        {
            setNombreUsuario(nombreUsuario);
            setContraseña(contraseña);
            setPuntos(puntos);
            setSaldo(saldo);
            this.productosAdquiridos = new List<Producto>();
        }

        //Getters.
        public string getNombreUsuario()
        {
            return this.nombreUsuario;
        }

        public List<Producto> getProductosAdquiridos()
        {
            return this.productosAdquiridos;
        }

        public int getPuntos()
        {
            return this.puntos;
        }

        //Setters.
        public void setNombreUsuario(string nombreUsuario)
        {
            if (!string.IsNullOrEmpty(nombreUsuario))
            {
                this.nombreUsuario = nombreUsuario;
            }
            else
            {
                Console.WriteLine("El nombre de usuario no puede ser nulo o vacío.");
            }
        }

        public void setContraseña(string contraseña)
        {
            if (!string.IsNullOrEmpty(contraseña))
            {
                this.contraseña = contraseña;
            }
            else
            {
                Console.WriteLine("La contraseña no puede ser nula o vacía");
            }
        }

        public void setProductosAdquiridos(List<Producto> productos)
        {
            if(productos != null)
            {
                this.productosAdquiridos = productos;
            }
            else
            {
                Console.WriteLine("La lista de productos no puede ser nula.");
            }

        }

        public void setPuntos(int puntos)
        {
            if(puntos > 0)
            {
                this.puntos = puntos;
            }
            else
            {
                Console.WriteLine("Los puntos deben ser mayor a 0.");
            }
        }

        public void setSaldo(int saldo)
        {
            if(saldo > 0)
            {
                this.saldo = saldo;
            }
            else
            {
                Console.WriteLine("El saldo debe ser mayor a 0.");
            }
        }
    }
}