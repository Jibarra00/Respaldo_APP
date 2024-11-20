﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFCategoria.aspx.cs" Inherits="Presentation.WFCategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Estilos --%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
    <link href="resources/css/Categoria.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">

        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Gestión De Categorías</h1>

        </div>
        <!-- /.container-fluid -->

    </div>
    <form runat="server">


        <%-- Id --%>
        <asp:HiddenField ID="HFCategoryID" runat="server" />
        <br />

        <%--Descripción--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese la descripción de la categoría"></asp:Label>
        <asp:TextBox ID="TBDescription" runat="server"></asp:TextBox>
        <br />

        <%--Botón guardar y actualizar--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>

    <%--Lista de categorías--%>
    <h2>Lista de Categorías</h2>
    <table id="categoriesTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Descripcion</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Categorías--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#categoriesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFCategoria.aspx/ListCategories",// Se invoca el WebMethod Listar Productos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de productos del resultado
                    }
                },
                "columns": [
                    { "data": "CategoryID" },
                    { "data": "Description" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.CategoryID}">Editar</button>
                              <button class="delete-btn" data-id="${row.CategoryID}">Eliminar</button>`;

                        }
                    }
                ],
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página",
                    "zeroRecords": "No se encontraron resultados",
                    "info": "Mostrando página _PAGE_ de _PAGES_",
                    "infoEmpty": "No hay registros disponibles",
                    "infoFiltered": "(filtrado de _MAX_ registros totales)",
                    "search": "Buscar:",
                    "paginate": {
                        "first": "Primero",
                        "last": "Último",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                }

            });

            // Editar un producto
            $('#categoriesTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#categoriesTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadCategoryData(rowData);
            });

            // Eliminar un producto
            $('#categoriesTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID de la categoria
                if (confirm("¿Estás seguro de que deseas eliminar este producto?")) {
                    deleteCategory(id);// Invoca a la función para eliminar el producto
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadCategoryData(rowData) {
            $('#<%= HFCategoryID.ClientID %>').val(rowData.CategoryID);
            $('#<%= TBDescription.ClientID %>').val(rowData.Description);
        }

        // Función para eliminar un producto
        function deleteCategory(id) {
            $.ajax({
                type: "POST",
                url: "WFCategoria.aspx/DeleteCategory",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#categoriesTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Categoría eliminada exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar la categoría.");
                }
            });
        }
    </script>


</asp:Content>
