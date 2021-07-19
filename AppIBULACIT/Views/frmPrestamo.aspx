<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmPrestamo.aspx.cs" Inherits="AppIBULACIT.Views.frmPrestamo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
     function openModal() {
                 $('#myModal').modal('show'); //ventana de mensajes
        }

        function openModalMantenimiento() {
            $('#myModalMantenimiento').modal('show'); //ventana de mantenimiento
        }    

        function CloseModal() {
            $('#myModal').modal('hide');//cierra ventana de mensajes
        }

        function CloseMantenimiento() {
            $('#myModalMantenimiento').modal('hide'); //cierra ventana de mantenimiento
        }

        $(document).ready(function () { //filtrar el datagridview
            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#MainContent_gvTipoPrestamo tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script> 
    <h1><asp:Label Text="Prestamos" runat="server"></asp:Label></h1>
    <input id="myInput" Placeholder="Buscar" class="form-control" type="text" />
    <asp:GridView ID="gvPrestamo" OnRowCommand="gvPrestamo_RowCommand" runat="server" AutoGenerateColumns="False" 
        CssClass="table table-sm" HeaderStyle-CssClass="thead-dark" HeaderStyle-BackColor="#243054"
        HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="LightBlue" Width="100%">
    <Columns>
        <asp:BoundField HeaderText="Codigo" DataField="Codigo"/>
         <asp:BoundField HeaderText="Fecha Limite" DataField="FechaLimite" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Fecha Inicio" DataField="FechaInicio" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Monto Pago" DataField="MontoPago" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Tipo Prestamo" DataField="TipoPrestamo" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Interes" DataField="Interes" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
         <asp:BoundField HeaderText="Codigo Cuenta" DataField="CodigoCuenta" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Codigo Sucursal" DataField="CodigoSucursal" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:ButtonField HeaderText="Modificar" CommandName="Modificar" ControlStyle-CssClass="btn btn-primary" ButtonType="Button" Text="Modificar" />
        <asp:ButtonField HeaderText="Eliminar" CommandName="Eliminar" ControlStyle-CssClass="btn btn-danger" ButtonType="Button" Text="Eliminar" />
    </Columns>
    </asp:GridView>
    <asp:LinkButton type="button" ID="btnNuevo" OnClick="btnNuevo_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></span> Nuevo"></asp:LinkButton>
    <br />
    <asp:Label ID="lblStatus" ForeColor="Maroon" runat="server" Visible="false" />
    <!-- VENTANA MODAL -->
      <div id="myModal" class="modal fade" role="dialog">
  <div class="modal-dialog modal-sm">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Mantenimiento de Prestamos </h4>
      </div>
      <div class="modal-body">
        <p><asp:Literal ID="ltrModalMensaje" runat="server" /><asp:Label ID="lblCodigoEliminar" runat="server" /></p>
      </div>
      <div class="modal-footer">
         <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnAceptarModal" OnClick="btnAceptarModal_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" />
         <asp:LinkButton type="button" CssClass="btn btn-danger" ID="btnCancelarModal" OnClick="btnCancelarModal_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cerrar" />
      </div>
    </div>
  </div>
</div>
    <!--VENTANA DE MANTENIMIENTO -->
  <div id="myModalMantenimiento" class="modal fade" role="dialog">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title"><asp:Literal ID="ltrTituloMantenimiento" runat="server"></asp:Literal></h4>
      </div>
      <div class="modal-body">
          <table style="width: 100%;">
              <tr>
                  <td><asp:Literal ID="ltrCodigoMant" Text="Codigo" runat="server" /></td>
                  <td><asp:TextBox ID="txtCodigoMant" runat="server" Enabled="false" CssClass="form-control" /></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrFechaLimite" Text="FechaLimite" runat="server" /></td>
                  <td><asp:Calendar ID="CldFechaLimite" runat="server" Enabled="false" CssClass="form-control" /></td>
              </tr>
               <tr>
                  <td><asp:Literal ID="ltrFechaInicio" Text="FechaInicio" runat="server" /></td>
                  <td><asp:TextBox ID="txtFechaInicio" runat="server" Enabled="false" CssClass="form-control" /></td>
              </tr>
               <tr>
                  <td><asp:Literal ID="ltrMontoPago" Text="MontoPago" runat="server" /></td>
                  <td><asp:TextBox ID="txtMontoPago" runat="server" Enabled="false" CssClass="form-control" /></td>
              </tr>
               <tr>
                  <td><asp:Literal ID="ltrTipoPrestamo" Text="TipoPrestamo" runat="server" /></td>
                  <td><asp:TextBox ID="txtTipoPrestamo" runat="server" Enabled="false" CssClass="form-control" /></td>
              </tr>
               <tr>
                  <td><asp:Literal ID="ltrInteres" Text="Interes" runat="server" /></td>
                  <td><asp:TextBox ID="txtInteres" runat="server" Enabled="false" CssClass="form-control" /></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrCodigoCuenta" Text="CodigoCuenta" runat="server" /></td>
                  <td><asp:TextBox ID="txtCodigoCuenta" runat="server" Enabled="false" CssClass="form-control" /></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrCodigoSucursal" Text="CodigoSucursal" runat="server" /></td>
                  <td><asp:TextBox ID="txtCodigoSucursal" runat="server" Enabled="false" CssClass="form-control" /></td>
              </tr>
          </table>
          <asp:Label ID="lblResultado" ForeColor="Maroon" Visible="False" runat="server" />
      </div>
      <div class="modal-footer">
        <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnAceptarMant" OnClick="btnAceptarMant_Click"  runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" />
        <asp:LinkButton type="button"  CssClass="btn btn-danger" ID="btnCancelarMant" OnClick="btnCancelarMant_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cerrar" />
      </div>
    </div>
  </div>
</div>
</asp:Content>



