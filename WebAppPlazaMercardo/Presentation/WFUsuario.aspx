﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFUsuario.aspx.cs" Inherits="Presentation.WFUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resourse/css/datatables.min.css" rel="stylesheet" />
    <link href="resources/css/Usuarios.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">

    <!-- Page Heading -->
    <div class="d-sm-flex align-items-center justify-content-between mb-4">
        <h1 class="h3 mb-0 text-gray-800">Gestión De Pedidos</h1>

    </div>
    <!-- /.container-fluid -->

</div>
<form runat="server">
        <%--ID--%>
        <asp:HiddenField ID="HFUsuarioID" runat="server" />
        <br />
        <%--EMAIL--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el correo electronico "></asp:Label>
        <asp:TextBox ID="TBEmail" runat="server"></asp:TextBox>
        <br />
        <%--CONTRASEÑA--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la contraseña"></asp:Label>
        <asp:TextBox ID="TBPassword" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Escriba el estado"></asp:Label>
        <asp:DropDownList ID="DDLState" runat="server">
            <asp:ListItem Value="0">Seleccione</asp:ListItem>
            <asp:ListItem Value="Activo">Activo</asp:ListItem>
            <asp:ListItem Value="Inactivo">Inactivo</asp:ListItem>
        </asp:DropDownList>
        <br />
        <%--FECHA--%>
        <asp:Label ID="Label5" runat="server" Text="Ingrese la fecha (AAAA-MM-DD)"></asp:Label>
        <asp:TextBox ID="TBDate" runat="server" TextMode="Date"></asp:TextBox>
        <br />
        <%--ROL--%>
        <asp:Label ID="Label6" runat="server" Text="Seleccione el rol"></asp:Label>
        <asp:DropDownList ID="DDLRol" runat="server"></asp:DropDownList>
        <br />
        <%--EMPLEADO--%>
        <asp:Label ID="Label7" runat="server" Text="Seleccione el empleado"></asp:Label>
        <asp:DropDownList ID="DDLEmpleado" runat="server"></asp:DropDownList>
        <br />

        <%--BOTON DE GUARDAR Y ACTUALIZAR--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>

    <%--LISTA DE USUARIOS--%>

    <h2>Lista de Usuarios</h2>
    <table id="UsuarioTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Correo</th>
                <th>Contraseña</th>
                <th>salt</th>
                <th>Estado</th>
                <th>Fecha Creación</th>
                <th>FkRol</th>
                <th>Rol</th>
                <th>FkEmpleado</th>
                <th>Empleado</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>


    <%--DataTables--%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Usuarios--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#UsuarioTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFUsuario.aspx/ListUsuario",// Se invoca el WebMethod Listar los Usuarios
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de usuarios del resultado
                    }
                },
                "columns": [
                    { "data": "UsuarioID" },
                    { "data": "Correo" },
                    { "data": "Contraseña" },
                    { "data": "Salt" },
                    { "data": "Estado" },
                    { "data": "Fecha_creacion" },
                    { "data": "FkRol", "visible": false },
                    { "data": "nameRol" },
                    { "data": "FkEmpleado", "visible": false },
                    { "data": "nameEmpleado" },
                    {
                        "data": null,
                        "render": function (row) {
                            return `<button class="edit-btn" data-id="${row.UsuarioID}">Editar</button>
                                 <button class="delete-btn" data-id="${row.UsuarioID}">Eliminar</button>`;
                        }
                    }
                ],
                "language": {
                    "lengthMenu": "Mostrar MENU registros por página",
                    "zeroRecords": "No se encontraron resultados",
                    "info": "Mostrando página PAGE de PAGES",
                    "infoEmpty": "No hay registros disponibles",
                    "infoFiltered": "(filtrado de MAX registros totales)",
                    "search": "Buscar:",
                    "paginate": {
                        "first": "Primero",
                        "last": "Último",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                }

            });

            // Editar un usuario
            $('#UsuarioTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#UsuarioTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadUsuarioData(rowData);
            });

            // Eliminar un usuario
            //$('#UsuarioTable').on('click', '.delete-btn', function () {
            //    const id = $(this).data('id');// Obtener el ID del usuario
            //    if (confirm("¿Estás seguro de que deseas eliminar este usuario?")) {
            //        DeleteUsuario(id);// Invoca a la función para eliminar el producto
            //    }
            //});
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadUsuarioData(rowData) {
            $('#<%= HFUsuarioID.ClientID %>').val(rowData.UsuarioID);
            $('#<%= TBEmail.ClientID %>').val(rowData.Correo);
            
            $('#<%= DDLState.ClientID %>').val(rowData.Estado);
            $('#<%= TBDate.ClientID %>').val(rowData.Fecha_creacion);
            $('#<%= DDLRol.ClientID %>').val(rowData.FkRol);
            $('#<%= DDLEmpleado.ClientID %>').val(rowData.FkEmpleado);
        }

        // Función para eliminar un producto
        //function DeleteUsuario(id) {
        //    $.ajax({
        //        type: "POST",
        //        url: "WFUsuario.aspx/DeleteUsuario",// Se invoca el WebMethod Eliminar un Producto
        //        contentType: "application/json; charset=utf-8",
        //        data: JSON.stringify({ id: id }),
        //        success: function (response) {
        //            $('#UsuarioTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
        //            alert("El usuario se ha eliminado exitosamente.");
        //        },
        //        error: function () {
        //            alert("Error al eliminar el usuario.");
        //        }
        //    });
        //}
    </script>
</asp:Content>
