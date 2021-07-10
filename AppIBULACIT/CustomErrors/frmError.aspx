<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="frmError.aspx.cs" Inherits="AppIBULACIT.CustomErrors.frmError" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>

<body>

    <asp:ScriptManager runat="server">
        <Scripts>
            <%--To learn more about bundling scripts in ScriptManager see https://go.microsoft.com/fwlink/?LinkID=301884 --%>
            <%--Framework Scripts--%>
            <asp:ScriptReference Name="MsAjaxBundle" />
            <asp:ScriptReference Name="jquery" />
            <asp:ScriptReference Name="bootstrap" />
            <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
            <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
            <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
            <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
            <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
            <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
            <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
            <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
            <asp:ScriptReference Name="WebFormsBundle" />
            <%--Site Scripts--%>
        </Scripts>
    </asp:ScriptManager>



    <div class="container">
        <h3><span aria-hidden='true' class='glyphicon glyphicon-remove'></span>Ocurrió un error</h3>
        <div class="panel-group">
            <div class="panel panel-primary">
                <div class="panel-body">

                    <asp:Label ID="Label1" runat="server" Font-Size="Large" Text="Hubo un error al procesar la solicitud"></asp:Label>
                    <br />
                    <asp:Label ID="lblError" runat="server"></asp:Label>

                </div>
            </div>
        </div>
    </div>
</body>
</html>
