<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEmpleado.aspx.cs" Inherits="Presentation.WFEmpleado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Estilos --%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
    <link href="resources/css/Empleados2.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Gestión De Empleados</h1>
        </div>
    </div>

    <form runat="server" class="form-container">
        <%-- Id --%>
        <asp:HiddenField ID="HFEmployeeID" runat="server" />

        <%-- Identificacion --%>
        <label for="TBCC" class="form-label">Ingrese su identificación</label>
        <asp:TextBox ID="TBCC" runat="server" CssClass="form-control"></asp:TextBox>

        <%-- Nombre --%>
        <label for="TBname" class="form-label">Ingrese su Nombre</label>
        <asp:TextBox ID="TBname" runat="server" CssClass="form-control"></asp:TextBox>

        <%-- Apellido --%>
        <label for="TBlastname" class="form-label">Ingrese su Apellido</label>
        <asp:TextBox ID="TBlastname" runat="server" CssClass="form-control"></asp:TextBox>

        <%-- Teléfono --%>
        <label for="TBphone" class="form-label">Ingrese su Teléfono/Celular</label>
        <asp:TextBox ID="TBphone" runat="server" CssClass="form-control"></asp:TextBox>

        <%-- Dirección --%>
        <label for="TBaddres" class="form-label">Ingrese su Dirección</label>
        <asp:TextBox ID="TBaddres" runat="server" CssClass="form-control"></asp:TextBox>

        <%-- Botones Guardar y Actualizar --%>
        <div class="text-center mt-3">
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CssClass="btn btn-primary" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" CssClass="text-danger"></asp:Label>
        </div>
    </form>

    <!-- Lista de Empleados -->
    <h2 class="text-center mt-5">Lista de Empleados</h2>
    <div class="table-container">
        <table id="employeesTable" class="display">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Identificación</th>
                    <th>Nombre</th>
                    <th>Apellido</th>
                    <th>Teléfono</th>
                    <th>Dirección</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody></tbody>
        </table>
    </div>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#employeesTable').DataTable({
                "ajax": {
                    "url": "WFEmpleado.aspx/ListEmployees",
                    "type": "POST",
                    "contentType": "application/json; charset=utf-8",
                    "dataType": "json",
                    "dataSrc": function (json) {
                        return json.d.data || [];
                    }
                },
                "columns": [
                    { "data": "EmployeeID" },
                    { "data": "Identification" },
                    { "data": "Name" },
                    { "data": "lastName" },
                    { "data": "Phone" },
                    { "data": "Addres" },
                    {
                        "data": null,
                        "render": function (data) {
                            return `<button class="edit-btn" data-id="${data.EmployeeID}">Editar</button>
                                    <button class="delete-btn" data-id="${data.EmployeeID}">Eliminar</button>`;
                        }
                    }
                ]
            });

            // Editar un empleado
            $('#employeesTable').on('click', '.edit-btn', function () {
                const rowData = $('#employeesTable').DataTable().row($(this).parents('tr')).data();
                loadEmployeeData(rowData);
            });

            // Eliminar un empleado
            $('#employeesTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');
                if (confirm("¿Estás seguro de que deseas eliminar este empleado?")) {
                    deleteEmployee(id);
                }
            });
        });

        function loadEmployeeData(rowData) {
            $('#<%= HFEmployeeID.ClientID %>').val(rowData.EmployeeID);
            $('#<%= TBCC.ClientID %>').val(rowData.Identification);
            $('#<%= TBname.ClientID %>').val(rowData.Name);
            $('#<%= TBlastname.ClientID %>').val(rowData.lastName);
            $('#<%= TBphone.ClientID %>').val(rowData.Phone);
            $('#<%= TBaddres.ClientID %>').val(rowData.Addres);
        }

        function deleteEmployee(id) {
            $.ajax({
                type: "POST",
                url: "WFEmpleado.aspx/DeleteEmployee",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function () {
                    $('#employeesTable').DataTable().ajax.reload();
                    alert("Empleado eliminado exitosamente.");
                }
            });
        }
    </script>
</asp:Content>

