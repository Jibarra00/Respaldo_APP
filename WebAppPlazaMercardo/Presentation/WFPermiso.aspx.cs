        using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFPermiso : System.Web.UI.Page
    {
        PermisoLog objPer = new PermisoLog();

        private int _id;
        private string _name, _description;
        private bool executed = false;
        /*
       *  Variables de tipo pública que indiquen si el usuario tiene
       *  permiso para ver los botones editar y eliminar.
       */
        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BTSave.Visible = false;
                BTUpdate.Visible = false;
                FrmPermission.Visible = false;
                PanelAdmin.Visible = false;
            }
            validatePermissionRol();
        }
        /*
         * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
         * parte de un servicio web, lo que significa que puede ser invocado de manera
         * remota a través de HTTP.
         */
        [WebMethod]
        public static object ListPermiso()
        {
            PermisoLog objPer = new PermisoLog();

            var dataSet = objPer.showPermisos();

            var permisoList = new List<object>();

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                permisoList.Add(new
                {
                    PermisoID = row["per_id"],
                    Nombre = row["per_nombre"],
                    Descripcion = row["per_descripcion"]
                });
            }
            return new { data = permisoList };
        }

        [WebMethod]
        public static bool DeletePermiso(int id)
        {
            PermisoLog objPer = new PermisoLog();

            return objPer.deletePermiso(id);
        }

        // Metodo para validar permisos roles
        private void validatePermissionRol()
        {
            // Se Obtiene el usuario actual desde la sesión
            var objUser = (User)Session["User"];

            // Variable para acceder a la MasterPage y modificar la visibilidad de los enlaces.
            var masterPage = (Main)Master;

            if (objUser == null)
            {
                // Redirige a la página de inicio de sesión si el usuario no está autenticado
                Response.Redirect("Default.aspx");
                return;
            }
            // Obtener el rol del usuario
            var userRole = objUser.Rol.Nombre;

            if (userRole == "Administrador")
            {
                //LblMsg.Text = "Bienvenido, Administrador!";

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmPermission.Visible = true;// Se pone visible el formulario
                            BTSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmPermission.Visible = true;
                            BTUpdate.Visible = true;
                            PanelAdmin.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsj.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else if (userRole == "Empleado")
            {
                //LblMsg.Text = "Bienvenido, Gerente!";

                masterPage.linkUser.Visible = false;// Se oculta el enlace de Usuario
                masterPage.linkPermission.Visible = false;
                masterPage.linkPermissionRol.Visible = false;// Se oculta el enlace de Permiso Rol

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmPermission.Visible = true;
                            BTSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmPermission.Visible = false;
                            BTUpdate.Visible = false;
                            PanelAdmin.Visible = false;
                            _showEditButton = false;
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = false;
                            _showDeleteButton = false;
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsj.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }

            }
            else if (userRole == "Cliente")
            {
                //LblMsg.Text = "Bienvenido, Secretaria!";
                masterPage.linkUser.Visible = false;
                masterPage.linkPermission.Visible = false;
                masterPage.linkPermissionRol.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmPermission.Visible = true;
                            BTSave.Visible = true;
                            PanelAdmin.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmPermission.Visible = true;
                            BTUpdate.Visible = false;
                            PanelAdmin.Visible = false;
                            _showEditButton = false;
                            break;
                        case "MOSTRAR":
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            PanelAdmin.Visible = false;
                            _showDeleteButton = false;
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsj.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else
            {
                // Si el rol no es reconocido, se deniega el acceso
                LblMsj.Text = "Rol no reconocido. No tienes permisos suficientes para acceder a esta página.";
                Response.Redirect("WFInicio.aspx");
            }
        }

        private void clear()
        {
            HFPermisoID.Value = "";
            DDLNombrePer.SelectedIndex = 0;
            TBDescripcion.Text = "";
        }



        protected void BTSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                _name = DDLNombrePer.SelectedValue.ToUpper();
                _description = TBDescripcion.Text;

                executed = objPer.savePermiso(_name, _description);
                if (executed)
                {
                    LblMsj.Text = "Se guardo correctamente";
                    clear();
                }
                else
                {
                    LblMsj.Text = "Error al guardar ";
                }
            }
        }

        protected void BTUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (string.IsNullOrEmpty(HFPermisoID.Value))
                {
                    LblMsj.Text = "No se ha seleccionado ningun permiso para actualizar.";
                    return;
                }

                _id = Convert.ToInt32(HFPermisoID.Value);
                _name = DDLNombrePer.SelectedValue.ToUpper();
                _description = TBDescripcion.Text;

                executed = objPer.updatePermiso(_id, _name, _description);

                if (executed)
                {
                    LblMsj.Text = "Se actualizo correctamente";
                    clear();
                }
                else
                {
                    LblMsj.Text = "Error al actualizar ";
                }
            }
        }
    }
}