using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class ProductosCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnVerProducto1_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://localhost:44331/WFMaracuyaPrimera.aspx");
        }
    }
}