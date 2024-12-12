using SistemaFidelidadPaws.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaFidelidadPaws
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Lista simulada de productos (puedes reemplazarla con datos de tu base de datos)
                List<Producto> productos = new List<Producto>
                {
                    new Producto("Eukanuba 15K", 50, 5, "Imagenes/Eukanuba15k.jpg"),
                    new Producto("Logo de nuestra marca :)", 100, 10, "Imagenes/Logo Paws.jpg"),
                    new Producto("Mon Ami & Bocadillos", 1500, 150, "Imagenes/MonAmi.jpg"),
                    new Producto("ProPlan 18K", 2000, 200, "Imagenes/ProPlan18k.jpg")
                };

                // Enlazar la lista de productos al Repeater
                rptProductos.DataSource = productos;
                rptProductos.DataBind();
            }
        }
    }
}