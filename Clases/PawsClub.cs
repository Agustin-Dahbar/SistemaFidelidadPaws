using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaFidelidadPaws.Clases
{
    public class PawsClub
    {
        // Cambiado para que productosStock sea un arreglo bidimensional [nombre, cantidad]
        private List<Cliente> clientes;
        private List<Producto> productos;
        private List<ProductoStock> productosStock;

        // Constructor
        public PawsClub()
        {
            this.productos = new List<Producto>();
            this.productosStock = new List<ProductoStock>();  // Inicialización de productosStock
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
        private ProductoStock buscarProductoEnStock(string nombreProducto)
        {
            ProductoStock encontrado = null;  // Variable para almacenar el producto encontrado
            int i = 0;

            while (i < this.productosStock.Count && encontrado == null)
            {
                ProductoStock iterado = this.productosStock[i];

                if (this.productosStock.Equals(nombreProducto))
                {
                    encontrado = this.productosStock[i];
                }
                else
                {
                    i++;  // Incrementar el índice si no es el producto correcto
                }
            }

            return encontrado; // Solo un return al final
        }

        // Método que devuelve cuántas unidades de un producto hay en stock.
        public int cuantoStock(string nombreProducto)
        {
            //Verificamos que exista el producto en el catálogo.
            Producto producto = buscarProducto(nombreProducto);
            int cantidadStock = 0;

            if (producto != null) //Si existe
            {
                //Verificamos que este en stock 
                ProductoStock encontradoEnStock = buscarProductoEnStock(nombreProducto);

                if (encontradoEnStock != null) //Si está en stock
                {
                    // Obtener la cantidad.
                    cantidadStock = encontradoEnStock.getCantidadEnStock();
                    
                }
                else //Si no
                {
                    Console.WriteLine("Producto no disponible en stock.");
                }
            }
            else //Si no existe 
            {
                Console.WriteLine("Producto no encontrado.");
            }

            return cantidadStock;  // Si no se encuentra en stock, retorna 0
        }


        // Getters de las listas.
        public List<Cliente> getClientes()
        {
            return this.clientes;
        }
        public List<Producto> getProductos()
        {
            return this.productos;
        }

        public List<ProductoStock> getProductoStock()
        {
            return this.productosStock;
        }

        //Getters de las entidades de la lista.
        public Cliente getCliente(string nombreUsuario)
        {
            return buscarCliente(nombreUsuario); 
        }
        public Producto getProducto(string nombreProducto)
        {
            return buscarProducto(nombreProducto); 
        }
        public ProductoStock getProductoEnStock(string nombreProducto)
        {
            return buscarProductoEnStock(nombreProducto); 
        }


        // Add´s

        //Cliente
        public void addCliente(Cliente cliente)
        {
            if (cliente != null) // Si el cliente no es nulo
            {
                // Verificamos si el cliente ya está en la lista
                Cliente encontrado = buscarCliente(cliente.getNombreUsuario()); 

                if (encontrado != null) // Si ya existe
                {
                    Console.WriteLine("El cliente ya está en la lista.");
                }
                else // Si no existe
                {
                    this.clientes.Add(cliente); // Lo agregamos
                }
            }
            else // Si el cliente es nulo
            {
                Console.WriteLine("El cliente es nulo");
            }
        }


        //Producto
        public void addProducto(Producto producto)
        {
            if (producto != null) //Si el producto no es nulo
            {
                //Verificamos si existe en la lista de productos para evitar generar duplicados
                Producto encontrado = buscarProducto(producto.getNombre());

                if(encontrado != null) //Si existe 
                {
                    Console.WriteLine("El producto ya está en la lista."); //Lo informamos
                }
                else //Si no
                {
                    this.productos.Add(producto); //Lo agregamos
                }
            }
            else //Si es nulo
            {
                Console.WriteLine("El producto es nulo"); //Lo informamos
            }
        }

        //ProductoStock
        public void addProductoEnStock(ProductoStock productoStock)
        {
            if (productoStock != null) //Si el producto no es nulo
            {
                //Verificamos si existe en la lista 
                ProductoStock encontrado = buscarProductoEnStock(productoStock.getNombreProducto());

                if (encontrado != null) //Si existe 
                {
                    Console.WriteLine("El producto ya está en stock."); //Lo informamos
                }
                else //Si no
                {
                    this.productosStock.Add(productoStock); //Lo agregamos
                }
            }
            else //Si es nulo
            {
                Console.WriteLine("El producto es nulo"); //Lo informamos
            }
        }

        //Agrega un producto en stock o actualiza su cantidad si ya está en la lista.
        public void addProductoStock(string nombreProducto, int cantidad)
        {
            ProductoStock encontrado = buscarProductoEnStock(nombreProducto);

            if (encontrado != null) //Si lo encontramos
            {
                encontrado.setCantidadEnStock(encontrado.getCantidadEnStock() + cantidad); //Actualizamos su cantidad
            }
            else //Si no 
            {
                productosStock.Add(new ProductoStock(nombreProducto, cantidad)); //Lo agregamos creando la entidad.
            }
        }
        
        // Deletes de ciertas entidades en las listas.
        //Clientes
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

        //Productos
        public void deleteProducto(string nombreProducto)
        {
            Producto encontrado = buscarProducto(nombreProducto);

            if (encontrado != null) 
            {
                this.productos.Remove(encontrado); 
            }
            else
            {
                Console.WriteLine("Nombre producto incorrecto");
            }
        }

        //Productos en stock
        public void deleteProductoEnStock(string nombreProductoStock)
        {
            ProductoStock encontrado = buscarProductoEnStock(nombreProductoStock);

            if (encontrado != null)
            {
                this.productosStock.Remove(encontrado);
            }
            else
            {
                Console.WriteLine("Nombre producto incorrecto");
            }
        }
    }
}
