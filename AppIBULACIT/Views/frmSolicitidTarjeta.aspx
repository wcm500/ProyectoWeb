<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmSolicitidTarjeta.aspx.cs" Inherits="AppIBULACIT.Views.frmSolicitidTarjeta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css" />
<link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.2.2/css/buttons.dataTables.min.css" />
<script type="text/javascript" src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.2/js/dataTables.buttons.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"></script>
<script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/pdfmake.min.js"></script>
<script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/vfs_fonts.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.2/js/buttons.html5.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.5.0/Chart.min.js"></script>

<script type="text/javascript">

    $.noConflict();
    jQuery(document).ready(function ($) {
        $('[id*=gvSolicitudTarjetas]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
            dom: 'Bfrtip',
            'aoColumnDefs': [{ 'bSortable': false, 'aTargets': [0] }],
            'iDisplayLength': 5,
            buttons: [
                { extend: 'copy', text: 'Copiar Teclado', className: 'exportExcel', exportOptions: { modifier: { page: 'all' } } },
                { extend: 'excel', text: 'Exportar a Excel', className: 'exportExcel', filename: 'SolicitudTarjetas_Excel', exportOptions: { modifier: { page: 'all' } } },
                { extend: 'csv', text: 'Exportar a  CSV', className: 'exportExcel', filename: 'SolicitudTarjetas_Csv', exportOptions: { modifier: { page: 'all' } } },
                { extend: 'pdf', text: 'Exportar a  PDF', className: 'exportExcel', filename: 'SolicitudTarjetas_Pdf', orientation: 'landscape', pageSize: 'LEGAL', exportOptions: { modifier: { page: 'all' }, columns: ':visible' } }
            ]
        });
    });
</script>




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

        //$(document).ready(function () { //filtrar el datagridview
        //    $("#myInput").on("keyup", function () {
        //        var value = $(this).val().toLowerCase();
        //        $("#MainContent_gvSolicitudTarjetas tr").filter(function () {
        //            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        //        });
        //    });
        //});
         </script> 

    <h1><asp:Label Text="Mantenimiento de Solicitud Tarjeta" runat="server"></asp:Label></h1>
    <%--<input id="myInput" Placeholder="Buscar" class="form-control" type="text" />--%>
    <asp:GridView ID="gvSolicitudTarjetas" OnRowCommand="gvSolicitudTarjetas_RowCommand" runat="server" AutoGenerateColumns="False" 
        CssClass="table table-sm" HeaderStyle-CssClass="thead-dark" HeaderStyle-BackColor="#243054"
        HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="LightBlue" Width="100%">
        <Columns>
            <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
            <asp:BoundField HeaderText="CodigoCliente" DataField="CodigoCliente" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField HeaderText="FechaSolicitud" DataField="FechaSolicitud" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField HeaderText="CondicionLaboral" DataField="CondicionLaboral" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField HeaderText="IngresoMensual" DataField="IngresoMensual" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField HeaderText="CodigoTipoTarjeta" DataField="CodigoTipoTarjeta" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
            <asp:ButtonField HeaderText="Modificar" CommandName="Modificar" ControlStyle-CssClass="btn btn-primary" ButtonType="Button" Text="Modificar" />
            <asp:ButtonField HeaderText="Eliminar" CommandName="Eliminar" ControlStyle-CssClass="btn btn-danger" ButtonType="Button" Text="Eliminar" />
        </Columns>
    </asp:GridView>
    <asp:LinkButton type="button" ID="btnNuevo" OnClick="btnNuevo_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></span> Nuevo" ></asp:LinkButton>
    <br />
    <asp:Label ID="lblStatus" ForeColor="Maroon" runat="server" Visible="false"></asp:Label>


        <!-- VENTANA MODAL -->
      <div id="myModal" class="modal fade" role="dialog">
  <div class="modal-dialog modal-sm">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Mantenimiento de Solicitud Tarjeta </h4>
      </div>
      <div class="modal-body">
        <p><asp:Literal ID="ltrModalMensaje" runat="server" /><asp:Label ID="lblCodigoEliminar" runat="server" /></p>
      </div>
      <div class="modal-footer">
         <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnAceptarModal"  OnClick="btnAceptarModal_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" />
         <asp:LinkButton type="button"  CssClass="btn btn-danger" ID="btnCancelarModal"  OnClick="btnCancelarModal_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cerrar" />
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
                  <td><asp:Literal ID="ltrCodigoCliente" Text="Codigo Cliente" runat="server" /></td>
                  <td><asp:TextBox ID="txtCodigoCliente" runat="server" Enabled="false" CssClass="form-control" /></td>      
              </tr>
              <tr>
                 <td><asp:Literal ID="ltrFechaSolicitud" Text="Fecha Solicitud" runat="server" /></td>
                 <td><asp:TextBox ID="txtFechaSolicitud" runat="server" Enabled="false" CssClass="form-control" /></td>
              </tr>
              
              <tr>    
                  <td><asp:Literal Text="Condicion Laboral" runat="server"  /></td>
                  <td> <asp:DropDownList ID="ddlCondicionLaboral"  CssClass="form-control" runat="server">
                    <asp:ListItem Value="Empleado">Empleado</asp:ListItem>
                    <asp:ListItem Value="Desempleado">Desempleado</asp:ListItem>
                  </asp:DropDownList></td>
              </tr>

               <tr>
                  <td><asp:Literal ID="ltrIngresoMensual" Text="Ingreso Mensual" runat="server" /></td>
                  <td><asp:TextBox ID="txtIngresoMensual" runat="server" CssClass="form-control" /></td>
                  <asp:RegularExpressionValidator ID='vldNumber' ControlToValidate='txtIngresoMensual' Display='Dynamic' ErrorMessage='En el ingreso mensual no se aceptan letras'
                      ValidationExpression='^[1-9]\d*(,\d+)?$'  ForeColor="Red" runat ='server'>
                  </asp:RegularExpressionValidator>
              </tr>

              <tr>
                   <%--<ItemTemplate>
                   <td><asp:Literal ID="ltrCodigoTipoTarjeta" Text='<%# Eval("Codigo") %>' runat="server"/></td>
                   </ItemTemplate>
                   <EditItemTemplate>
                    <td> <asp:DropDownList ID="ddlCodigoTipoTarjeta" CssClass="form-control" runat="server" >
                  </asp:DropDownList></td>
                  </EditItemTemplate>  --%>              
                   <td><asp:Literal Text="Codigo Tarjeta" runat="server"  /></td>
                  <td> <asp:DropDownList ID="ddlCodigoTipoTarjeta" CssClass="form-control" Text='<%# Eval("Codigo") %>' runat="server">
                  </asp:DropDownList></td>

              </tr>

          </table>
            <asp:Label ID="lblResultado" ForeColor="Maroon" Visible="False" runat="server" />
            </div>
        <div class="modal-footer">
        <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnAceptarMant" OnClick="btnAceptarMant_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" />
         <asp:LinkButton type="button"  CssClass="btn btn-danger" ID="btnCancelarMant" OnClick="btnCancelarMant_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cerrar" />
      </div>
    </div>
  </div>
</div>



</asp:Content>
