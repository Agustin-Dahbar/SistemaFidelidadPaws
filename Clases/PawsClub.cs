using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaFidelidadPaws.Clases
{
    public class PawsClub
    {
        // Cambiado para que productosStock sea un arreglo bidimensional [nombre, cantidad]
        private List<string[]> productosStock;
        private List<Producto> productos;
        private List<Cliente> clientes;

        // Constructor
        public PawsClub()
        {
            this.productos = new List<Producto>();
            this.productosStock = new List<string[]>();  // Inicialización de productosStock
            this.clientes = new List<Cliente>();
        }

        // Método que busca al cliente en la lista según su nombre de usuario (atributo identificatorio)
        private Cliente buscarCliente(string nombreUsuario)
        {
            Cliente encontrado = null;
            int i = 0;

            while (this.clientes.Count > i && encontrado == null)
            {
                Cliente iterado = this.clientes[i];

                if (iterado.getNombreUsuario().Equals(nombreUsuario))
                {
                    encontrado = iterado;
                }
                else
                {
                    i++;
                }
            }

            return encontrado;
        }

        //Busca el producto en la lista de productos generales
        private Producto buscarProducto(string nombreProducto)
        {
            Producto encontrado = null;
            int i = 0;

            while (this.productos.Count > i && encontrado == null)
            {
                Producto iterado = this.productos[i];

                if (iterado.getNombre().Equals(nombreProducto))
                {
                    encontrado = iterado;
                }
                else
                {
                    i++;
                }
            }

            return encontrado;
        }

        //Busca el producto en la lista de productos generales
        private Producto buscarProductoEnStock(string nombreProducto)
        {
            Producto encontrado = null;  // Variable para almacenar el producto encontrado
            int i = 0;

            while (i < this.productosStock.Count && encontrado == null)
            {
                if (this.productosStock[i][0].Equals(nombreProducto))
                {
                    // Crear un nuevo producto con el nombre y la cantidad en stock
                    encontrado = new Producto(nombreProducto, int.Parse(this.productosStock[i][1]));
                }
                else
                {
                    i++;  // Incrementar el índice si no es el producto correcto
                }
            }

            return encontrado; // Solo un return al final
        }


        // Método que devuelve cuántas unidades quedan de un producto
        public int cuantoStock(string nombreProducto)
        {
            Producto producto = buscarProducto(nombreProducto);
            int cantidadStock = 0;

            if (producto == null)
            {
                Console.WriteLine("Producto no encontrado.");
            }
            else
            {
                // Usar el mismo método de búsqueda para buscar en el stock
                Producto encontradoEnStock = buscarProductoEnStock(nombreProducto);

                if (encontradoEnStock == null)
                {
                    Console.WriteLine("Producto no disponible en stock.");
                    cantidadStock = 0; // Si no está en stock
                }
                else
                {
                    // Si se encuentra, obtener la cantidad de stock
                    cantidadStock = int.Parse(encontradoEnStock.getStock().ToString());
                }
            }

            return cantidadStock; ; // Si no se encuentra en stock, retorna 0
        }

        // Getters
        public List<Cliente> getClientes()
        {
            return this.clientes;
        }

        public Cliente getCliente(string nombreUsuario)
        {
            return buscarCliente(nombreUsuario);
        }

        public List<Producto> getProductos()
        {
            return this.productos;
        }

        public List<string[]> ProductosStock()
        {
            return this.productosStock;
        }

        // Adds
        public void addCliente(Cliente cliente)
        {
            if (cliente != null)
            {
                this.clientes.Add(cliente);
            }
            else
            {
                Console.WriteLine("El cliente es nulo");
            }
        }

        public void addProducto(Producto producto)
        {
            if (producto != null)
            {
                this.productos.Add(producto);
            }
            else
            {
                Console.WriteLine("El producto es nulo");
            }
        }

        public void addProductoStock(string nombreProducto, int cantidad)
        {
            // Buscar si el producto ya está en stock
            bool encontrado = false;
            foreach (var item in this.productosStock)
            {
                if (item[0].Equals(nombreProducto))
                {
                    // Si ya existe, actualizamos la cantidad
                    item[1] = (int.Parse(item[1]) + cantidad).ToString();
                    encontrado = true;
                    break;
                }
            }

            // Si no se encuentra el producto, lo agregamos
            if (!encontrado)
            {
                this.productosStock.Add(new string[] { nombreProducto, cantidad.ToString() });
            }
        }

        // Deletes
        public void deleteCliente(string nombreUsuario)
        {
            Cliente encontrado = buscarCliente(nombreUsuario); // buscamos al cliente por su nombre usuario, devuelve su instancia Cliente o null.

            if (encontrado != null) // Si lo encontramos
            {
                this.clientes.Remove(encontrado); // lo removemos
            }
            else
            {
                Console.WriteLine("Nombre de usuario incorrecto");
            }
        }
    }
}
