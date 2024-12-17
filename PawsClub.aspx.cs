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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si la página tiene MasterPage
                if (Master != null)
                {
                    // Llamar directamente al método OcultarBusqueda de la MasterPage
                    ((SiteMaster)Master).OcultarBusqueda();
                }

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
                
                //Creamos un ViewState que almacenará la lista de productos.
                ViewState["Productos"] = productos;

                //Asignamos la lista como origen de datos del repeater.
                rptProductos.DataSource = productos;
                rptProductos.DataBind();

                //Creamos el cliente que se mostrará en la tarjeta
                Cliente cliente = new Cliente("Agustin", "RiverPlate2018", 200);
                //Creamos una variable de sesión donde almacenamos al cliente .
                Session["Cliente"] = cliente;

                calcularEtapaDia();

                // Actualizamos los valores en los controles de servidor
                bienvenidaUsuario.InnerText = $"¡Buen{etapaActual}, {cliente.getNombreUsuario()}!";
                puntosUsuario.InnerText = $"Tus puntos: {cliente.getPuntos()}";
                

            }

        }

        //Metodo que se dispara al clickear en el boton "Canjear"
        protected void btnCanjear_Command(object sender, CommandEventArgs e)
        {
            //Recuperamos del CommandArgument (atributo referenciado en el Eval() en este caso el nombre del producto que el usuario quiere canjear. Realizamos un casteo final.
            string nombreProducto = e.CommandArgument.ToString();

            //Obtenemos la lista de productos que almacenamos en el ViewState
            List<Producto> productos = ViewState["Productos"] as List<Producto>;

            // Recuperamos el cliente desde la variable de sesión
            Cliente cliente = Session["Cliente"] as Cliente;
            
            //Evaluamos si no hay cliente, si no lo hay informamos y cortamos el metodo
            if (cliente == null)
            {
                mensajeCanje.Text = "No se encontró la información del cliente. Por favor, inicie sesión";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideMessage", "mostrarMensajeYOcultar();", true);
                return;
            }

            //Evaluamos si hay productos, si no los hay lo informamos y cortamos el metodo.
            if (productos == null)
            {
                mensajeCanje.Text = "No se encontraron los productos. Intente nuevamente.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideMessage", "mostrarMensajeYOcultar();", true);
                return;
            }
            else //Si hay productos obtenemos el seleccionado
            {
                //Buscamos el producto seleccionado entre la lista "productos" con el metodo FirstOrDefault() que iterará por los elementos de la misma y hará un getter del nombre para compararlo con el nombreDelProducto, hasta encontrarlo, si no lo encuentra su valor será null 'p' representa a cada producto iterado de la lista.
                Producto productoSeleccionado = productos.FirstOrDefault(p => p.getNombre() == nombreProducto);

                if(productoSeleccionado != null)
                {
                    //Validamos si al cliente le alcanza el canje con sus puntos.
                    if (cliente.getPuntos() >= productoSeleccionado.getPuntos())
                    {
                        cliente.getProductosAdquiridos().Add(productoSeleccionado); //Añadimos el producto canjeado a su lista de productos obtenidos.
                        cliente.setPuntos(cliente.getPuntos() - productoSeleccionado.getPuntos()); //Actualizamos los puntos del cliente restando los que se acaba de gastar.
                        mensajeCanje.Text = "Producto canjeado correctamente"; //Mensaje de éxito.
                        ActualizarInformacionCliente();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideMessage", "mostrarMensajeYOcultar();", true);
                    }
                    else
                    {
                        mensajeCanje.Text = "No tienes suficientes puntos para canjear este producto";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideMessage", "mostrarMensajeYOcultar();", true);
                    }
                }
                else
                {
                    mensajeCanje.Text = "El producto seleccionado no existe";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HideMessage", "mostrarMensajeYOcultar();", true);
                }
            }

        }

        //Metodo secundario que actualizará los datos de datosUsuario al hacer un canje.
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