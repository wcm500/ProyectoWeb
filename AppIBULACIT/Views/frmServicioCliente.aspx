<%@ Page Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmServicioCliente.aspx.cs" Inherits="AppIBULACIT.Views.frmServicioCliente" %>
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
                $("#MainContent_gvServicioCliente tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script> 
    <h1><asp:Label Text="Mantenimiento de servicio cliente" runat="server"></asp:Label></h1>
    <input id="myInput" Placeholder="Buscar" class="form-control" type="text" />
    <asp:GridView ID="gvServicioCliente" OnRowCommand="gvServicioCliente_RowCommand" runat="server" AutoGenerateColumns="False" 
        CssClass="table table-sm" HeaderStyle-CssClass="thead-dark" HeaderStyle-BackColor="#243054"
        HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="LightBlue" Width="100%">
    <Columns>
        <asp:BoundField HeaderText="Codigo Servicio" DataField="CodigoServicio"/>
        <asp:BoundField HeaderText="Codigo Usuario" DataField="CodigoUsuario" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Fecha Creacion" DataField="FechaCreacion" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Tipo Ayuda" DataField="TipoAyuda" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:ButtonField HeaderText="Modificar" CommandName="Modificar" ControlStyle-CssClass="btn btn-primary" ButtonType="Button" Text="Modificar" />
        <asp:ButtonField HeaderText="Eliminar" CommandName="Eliminar" ControlStyle-CssClass="btn btn-danger" ButtonType="Button" Text="Eliminar" />
    </Columns>
    </asp:GridView>
    <asp:LinkButton type="button" OnClick="btnNuevo_Click" CssClass="btn btn-success" ID="btnNuevo"  runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></span> Nuevo" />
    <br />
    <asp:Label ID="lblStatus" ForeColor="Maroon" runat="server" Visible="false" />ENTANA MODAL -->
  <div id="myModal" class="modal fade" role="dialog">
  <div class="modal-dialog modal-sm">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Mantenimiento de servicios</h4>
      </div>
      <div class="modal-body">
        <p><asp:Literal ID="ltrModalMensaje" runat="server" /><asp:Label ID="lblCodigoEliminar" runat="server" /></p>
      </div>
      <div class="modal-footer">
         <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnAceptarModal" OnClick="btnAceptarModal_Click"  runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" />
         <asp:LinkButton type="button"  CssClass="btn btn-danger" ID="btnCancelarModal" OnClick="btnCancelarModal_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cerrar" />
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
                  <td><asp:Literal ID="ltrCodigoUsuario" Text="Codigo Usuario" runat="server" /></td>
                  <td><asp:TextBox ID="txtCodigoUsuario" runat="server" Enabled="false" CssClass="form-control" /></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrDescripcion" Text="Descripcion" runat="server" /></td>
                  <td><asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" /></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrFechaCreacion" Text="Fecha Creacion" runat="server" /></td>
                  <td><asp:TextBox ID="txtFechaCreacion" Enabled="false" runat="server" CssClass="form-control" /></td>
              </tr>
              <tr>
                  <td><asp:Literal Text="Tipo de Ayuda" runat="server" /></td>
                  <td> <asp:DropDownList ID="ddlEstadoMant"  CssClass="form-control" runat="server">
                    <asp:ListItem Value="Evaluacion Credito">Evaluacion Creditacion</asp:ListItem>
                     <asp:ListItem Value="Informacion Prestamos">Informacion Prestamos</asp:ListItem>
                      <asp:ListItem Value="Quejas">Quejas</asp:ListItem>
                     <asp:ListItem Value="Informacion General">Informacion General</asp:ListItem>
                     <asp:ListItem Value="Solicitud de cita">Solicitud de cita</asp:ListItem>


                </asp:DropDownList></td>
              </tr>
          </table>
          <asp:Label ID="lblResultado" ForeColor="Maroon" Visible="False" runat="server" />
      <div class="modal-footer">
        <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnAceptarMant" OnClick="btnAceptarMant_Click"  runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" />
         <asp:LinkButton type="button"  CssClass="btn btn-danger" ID="btnCancelarMant" OnClick="btnCancelarMant_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cerrar" />
      </div>
    </div>
  </div>
</div>
</asp:Content>

