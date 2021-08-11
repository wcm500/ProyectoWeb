<%@ Page Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmServicioCliente.aspx.cs" Inherits="AppIBULACIT.Views.frmServicioCliente" %>
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
<%--<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.5.0/Chart.min.js"></script>--%>

    <script type="text/javascript">
        $.noConflict();
        jQuery(document).ready(function ($) {
            $('[id*=gvServicioCliente]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                dom: 'Bfrtip',
                'aoColumnDefs': [{ 'bSortable': false, 'aTargets': [0] }],
                'iDisplayLength': 5,
                buttons: [
                    { extend: 'copy', text: 'Copiar Tabla', className: 'exportExcel', exportOptions: { modifier: { page: 'all' } } },
                    { extend: 'excel', text: 'Exportar a Excel', className: 'exportExcel', filename: 'Errores_Excel', exportOptions: { modifier: { page: 'all' } } },
                    { extend: 'csv', text: 'Exportar a CSV', className: 'exportExcel', filename: 'Errores_Csv', exportOptions: { modifier: { page: 'all' } } },
                    { extend: 'pdf', text: 'Exportar a PDF', className: 'exportExcel', filename: 'Errores_Pdf', orientation: 'landscape', pageSize: 'LEGAL', exportOptions: { modifier: { page: 'all' }, columns: ':visible' } }
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
    </script> 

    <h1><asp:Label Text="Mantenimiento de servicio cliente" runat="server"></asp:Label></h1>
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
    <br/>
    <div class="row">
            <div class="col-sm">
     <div id="canvas-holder" style="width:40%">
		            <canvas id="vistas-chart"></canvas>
	            </div>
              <script >
                  $.noConflict();
                  new Chart(document.getElementById("vistas-chart"), {
                      type: 'polarArea',
                      data: {
                          labels: [<%= this.labelsGraficoVistasGlobal %>],
                          datasets: [{
                              label: "Tickets por tipo ayuda",
                              backgroundColor: [<%= this.backgroundcolorsGraficoVistasGlobal %>],
                        data: [<%= this.dataGraficoVistasGlobal %>]
                          }]
                      },
                      options: {
                          responsive: true,
                          title: {
                              display: true,
                              text: 'Tickets por tipo ayuda'
                          }
                      }
                  });
              </script>
                </div>
            </div>
    <asp:Label ID="lblStatus" ForeColor="Maroon" runat="server" Visible="false" />
<%--    <--------VENTANA MODAL -->--%>
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

