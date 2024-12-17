using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaFidelidadPaws
{
    public partial class SiteMaster : MasterPage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void OcultarBusqueda()
        {
            txtBusqueda.Attributes["Style"] = "display:none;";
            button.Attributes["Style"] = "display:none;";

        }
    }
}