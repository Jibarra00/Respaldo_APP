<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentation.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Iniciar Sesión</title>
    <link href="resources/css/Default.css" rel="stylesheet" />
    <%--CSS Bootstrap--%>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <div class="card" style="width: 24rem;">
                <!-- Imagen de cargando... -->
                <div id="loadingGif" class="text-center" style="display: none;">
                    <img src="resources/images/loading-7528_128.gif" alt="Cargando..." />
                </div>
            <div>
                <asp:Label ID="Label2" runat="server" Text="">Iniciar Sesion</asp:Label><br />
                <asp:Label ID="Label1" runat="server" Text="">Correo</asp:Label>
                <asp:TextBox ID="TBCorreo" runat="server"></asp:TextBox><br />
                <asp:Label ID="Label3" runat="server" Text="">Contraseña</asp:Label>
                <asp:TextBox ID="TBContrasena" runat="server" TextMode="Password"></asp:TextBox><br />
                <asp:Button ID="BtnIniciar" runat="server" Text="Iniciar" OnClick="BtnIniciar_Click" OnClientClick="showLoading();" /><br />
                <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </form>
    <%-- JS Bootstrap--%>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
    <%--Script para mostrar y ocultar la animacion de cargando...--%>
    <script type="text/javascript">
        function showLoading() {
            document.getElementById("loadingGif").style.display = "block";
        }

        function hideLoading() {
            document.getElementById("loadingGif").style.display = "none";
        }
    </script>
</body>
</html>
