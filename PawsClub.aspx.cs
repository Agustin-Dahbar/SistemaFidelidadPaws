using SistemaFidelidadPaws.Clases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaFidelidadPaws
{
    public partial class _Default : Page
    {

        //Variables globales ya que son utilizadas en múltiples metodos del code behind.
        //Almacenamos el horario actual
        DateTime horario = DateTime.Now;
        //Var que almacenará si es de dia, tarde y noche y lo reflejara en el mensaje de datosUsuario.
        string etapaActual = "";
        //Creamos 3 variables booleanas que representarán cada etapa del dia e indicarán si el horario coincide con ellas.
        bool mañana = false;
        bool tarde = false;
        bool noche = false;

        PawsClub paws = new PawsClub();
        Cliente agustin = new Cliente("Agustin", "RiverPlate2018", 200);
            

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si la página tiene MasterPage
                if (Master != null)
                {
                    // Llamar directamente al método OcultarBusqueda de la MasterPage
                    //((SiteMaster)Master).OcultarBusqueda();
                }
                
                //Creamos el cliente que se mostrará en la tarjeta
                //Creamos una variable de sesión donde almacenamos al cliente .
                Session["Cliente"] = agustin;

                //Creamos la lista de los productos a mostrarse en el repeaer
                List<Producto> productos = new List<Producto>
                {
                    new Producto("Eukanuba 15K", 50, 5, "Imagenes/Eukanuba15k.jpg"),
                    new Producto("Logo de nuestra marca", 100, 10, "Imagenes/Logo Paws.jpg"),
                    new Producto("Mon Ami & Bocadillos", 1500, 150, "Imagenes/MonAmi.jpg"),
                    new Producto("ProPlan 18K", 2000, 200, "Imagenes/ProPlan18k.jpg"),
                    new Producto("Eukanuba 15K", 50, 5, "Imagenes/Eukanuba15k.jpg"),
                    new Producto("Logo de nuestra marca", 100, 10, "Imagenes/Logo Paws.jpg"),
                    new Producto("Mon Ami & Bocadillos", 1500, 150, "Imagenes/MonAmi.jpg"),
                    new Producto("ProPlan 18K", 2000, 200, "Imagenes/ProPlan18k.jpg"),
                    new Producto("Eukanuba 15K", 50, 5, "Imagenes/Eukanuba15k.jpg"),
                    new Producto("Logo de nuestra marca", 100, 10, "Imagenes/Logo Paws.jpg"),
                    new Producto("Mon Ami & Bocadillos", 1500, 150, "Imagenes/MonAmi.jpg"),
                    new Producto("ProPlan 18K", 2000, 200, "Imagenes/ProPlan18k.jpg"),
                    new Producto("Eukanuba 15K", 50, 5, "Imagenes/Eukanuba15k.jpg"),
                    new Producto("Logo de nuestra marca", 100, 10, "Imagenes/Logo Paws.jpg"),
                    new Producto("Mon Ami & Bocadillos", 1500, 150, "Imagenes/MonAmi.jpg"),
                    new Producto("ProPlan 18K", 2000, 200, "Imagenes/ProPlan18k.jpg")

                };

                paws.addListaProductos(productos);

                //Creamos un ViewState que almacenará la lista de productos.
                ViewState["Productos"] = productos;

                //Asignamos la lista como origen de datos del repeater.
                rptProductos.DataSource = productos;
                rptProductos.DataBind();

                calcularEtapaDia();

                // Actualizamos los valores en los controles de servidor
                bienvenidaUsuario.InnerText = $"¡Buen{etapaActual}, {agustin.getNombreUsuario()}!";
                puntosUsuario.InnerText = $"Tus puntos: {agustin.getPuntos()}";

            }

        }

        //Metodo que se dispara al clickear en el boton "Canjear"
        protected void btnCanjear_Command(object sender, CommandEventArgs e)
        {
            //Recuperamos del CommandArgument (atributo referenciado en el Eval() en este caso el nombre del producto que el usuario quiere canjear. Realizamos un casteo final.
            string nombreProducto = e.CommandArgument.ToString();

            //Recuperamos la lista de productos almacenada en el ViewState
            List<Producto> productos = ViewState["Productos"] as List<Producto>;

            // Recuperamos el cliente desde la variable de sesión
            Cliente cliente = (Cliente)Session["Cliente"];
            //Cliente cliente = Session["Cliente"] as Cliente; Es otra opción
            
            //Si el cliente y la lista de productos no son nulos
            if(cliente != null && productos != null)
            {
                //Con FirstOrDefault() buscamos el producto canjeado por el usuario en la lista 'productos'. Iterará por las instancias(representadas por 'p') de la misma y hará un get del nombre de cada una para compararlo con el nombreDelProducto obtenido por el CommandArgument, si no lo encuentra su valor será null.
                Producto productoSeleccionado = productos.FirstOrDefault(p => p.getNombre() == nombreProducto);

                //Si el producto fue encontrado
                if (productoSeleccionado != null)
                {
                    //Validamos si al cliente le alcanza para realizar el canje.
                    if (cliente.getPuntos() >= productoSeleccionado.getPuntos())
                    {
                        //cliente.getProductosAdquiridos().Add(productoSeleccionado); //Añadimos el producto canjeado a su lista de productos obtenidos. Código viejo sin el "addProducto" desarrollado.
                        cliente.addProducto(productoSeleccionado); //Añadimos el producto canjeado a la lista de productos del cliente.
                        cliente.setPuntos(cliente.getPuntos() - productoSeleccionado.getPuntos()); //Actualizamos los puntos del cliente restando los que se acaba de gastar.
                        mensajeCanje.Text = "Producto canjeado correctamente"; //Mensaje de éxito.
                        ActualizarInformacionCliente(); //Actualizamos la info del cliente.
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideMessage", "mostrarMensajeYOcultar();", true);
                    }
                    else //Si no le alcanza: 
                    {
                        mensajeCanje.Text = "No tienes suficientes puntos para canjear este producto";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideMessage", "mostrarMensajeYOcultar();", true);
                    }
                }
                else //Si no se encontró el producto: 
                {
                    mensajeCanje.Text = "El producto seleccionado no existe";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HideMessage", "mostrarMensajeYOcultar();", true);
                }
            }
            else if(cliente == null) //Si el cliente es nulo: 
            {
                mensajeCanje.Text = "No se encontró la información del cliente. Por favor, inicie sesión";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideMessage", "mostrarMensajeYOcultar();", true);
                return; //Dejamos de ejecutar el código ya que el cliente es nulo.
            }
            else //Por descarte, si la lista de productos es nula: 
            {
                mensajeCanje.Text = "No se encontraron los productos. Intente nuevamente.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideMessage", "mostrarMensajeYOcultar();", true);
                return;
            }

        }

        //Metodo que mostrará el modal con el inventario del cliente.
        protected void btnVerProductos_Click(object sender, EventArgs e)
        {
            // Recuperar el cliente de la sesión
            Cliente cliente = Session["Cliente"] as Cliente;

            //Si el cliente se obtuvo correctamente: 
            if (cliente != null)
            {
                // Obtenemos su lista de productos adquiridos
                List<Producto> productosAdquiridos = cliente.getProductosAdquiridos();

                //Si la lista no es null y tiene al menos un elemento
                if (productosAdquiridos != null && productosAdquiridos.Count > 0)
                {
                    var productosAgrupados = productosAdquiridos.GroupBy(p => p.getNombre()).Select(g => new { Nombre = g.Key, Puntos = g.First().getPuntos(), Cantidad = g.Count(), imagenUrl = g.First().getImagenUrl()}).ToList();

                    rptProductosAdquiridos.DataSource = productosAgrupados;
                    rptProductosAdquiridos.DataBind();
                }
                else //Si no tiene productos en el inventario
                {
                    //No le damos origen de datos al rpt ya que nos los hay.
                    rptProductosAdquiridos.DataSource = null;
                    rptProductosAdquiridos.DataBind();
                }
            }

            // Abrir el modal con un script
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", "$(document).ready(function(){$('#productosModal').modal('show');});", true);
        }

        // Método para manejar la búsqueda de productos adquiridos
        //paws.addListaProductos(productos);
        public void BtnBuscarProducto_OnClick(object sender, EventArgs e)
        {
            //List<Producto> productos = ViewState["Productos"] as List<Producto>;
            //paws.addListaProductos(productos);

            string nombreProducto = Request.Form["txtBuscarProducto"];  // Obtener el valor del campo de texto

            rptProductos.Visible = false;

            if (!string.IsNullOrEmpty(nombreProducto))
            {
                // Buscar productos usando la expresión regular
                object resultado = paws.buscarProductoEnStockConExpresion(nombreProducto);

                if (resultado != null)
                {
                    // Si se encontraron productos, puedes hacer algo con los resultados, como mostrarlos en la página
                    // Aquí puedes actualizar un control en la interfaz, por ejemplo, un GridView o un Repeater
                    List<Producto> productosFiltrados = new List<Producto>();
                    
                    rptProductosBusqueda.DataSource = productosFiltrados;
                    rptProductosBusqueda.DataBind(); // Vincula los productos encontrados al Repeater de búsqueda

                    // Ocultar el Repeater de todos los productos
                    rptProductos.Visible = false;

                    // Mostrar el Repeater de productos filtrados
                    rptProductosBusqueda.Visible = true;

                    if (resultado is List<Producto>)
                    {
                        // Si hay varios productos encontrados, puedes iterar sobre la lista y mostrarla
                        List<Producto> productosEncontrados = (List<Producto>)resultado;
                        // Filtrar los productos que sean canjeables
                        foreach (var producto in productosEncontrados)
                        {
                            productosFiltrados.Add(producto);
                        }
                        // Actualiza la interfaz con los productos encontrados, por ejemplo, en un GridView o Repeater
                    }
                    else if (resultado is Producto)
                    {
                        // Si solo hay un producto encontrado, puedes mostrarlo directamente
                        Producto productoEncontrado = (Producto)resultado;
                        List<Producto> listaProducto = new List<Producto> { productoEncontrado };
                        rptProductosBusqueda.DataSource = listaProducto;
                        rptProductosBusqueda.DataBind();

                        rptProductos.Visible = false;
                        rptProductosBusqueda.Visible = true;
                        // Actualiza la interfaz con el producto encontrado
                    }
                }
                else
                {
                    rptProductos.Visible = false;
                    rptProductosBusqueda.Visible = false;
                    // Si no se encontró ningún producto
                    // Aquí puedes mostrar un mensaje en la interfaz indicando que no se encontraron resultados
                }
            }
            else
            {
                rptProductos.Visible = true;
                rptProductosBusqueda.Visible = false;
                // Si el campo de texto está vacío, muestra un mensaje de error o advertencia
            }
        }


        //"$('#productosModal').modal('show');"

        //Metodo secundario que actualizará los datos de datosUsuario al hacer un canje. (Usado en el anterior)
        private void ActualizarInformacionCliente()
        {
            Cliente cliente = Session["Cliente"] as Cliente;
            
            if (cliente != null)
            {
                calcularEtapaDia();
                // Actualizamos los valores en los controles de servidor
                bienvenidaUsuario.InnerText = $"¡Buen{etapaActual}, {cliente.getNombreUsuario()}!";
                puntosUsuario.InnerText = $"Tus puntos: {cliente.getPuntos()}";
            }
        }

        //Metodo que se llevará a cabo cada vez que se tenga que cargar la etapa del día ya sea por carga inicial o por canje realizado.
        private void calcularEtapaDia()
        {
            //Entre 5 y 12:59 es por la mañana. (8 horas)
            if (horario.Hour >= 5 && horario.Hour < 13)
            {
                mañana = true;
                etapaActual = " día"; //Inicia con separación para evitar el inesperado resultado "Buendía".
            }
            else if (horario.Hour > 12 && horario.Hour < 20) //Entre 13 y 19:59 es por la tarde (7 horas)
            {
                tarde = true;
                etapaActual = "as tardes"; //agregamos 'as' para asi complementar el "buen" de la línea 63 y formar "Buenas".
            }
            else //Entre 20:00 y 4:59 (9 horas)
            {
                noche = true;
                etapaActual = "as noches"; //mismo caso.
            }
        }

    }
}