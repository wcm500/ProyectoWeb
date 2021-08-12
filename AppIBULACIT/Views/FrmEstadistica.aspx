<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEstadistica.aspx.cs" Inherits="AppIBULACIT.Views.FrmEstadistica" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p></p>
    <p></p>
     <h1><asp:Label Text="Estadisticas" runat="server"></asp:Label></h1>
  
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css" />
<link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.2.2/css/buttons.dataTables.min.css" />
<script type="text/javascript" src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.2/js/dataTables.buttons.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"></script>
<script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/pdfmake.min.js"></script>
<script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/vfs_fonts.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.2/js/buttons.html5.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">

<script type="text/javascript">
    $.noConflict();
    jQuery(document).ready(function ($) {
        $('[id*=gvEstadisticas]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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
    <asp:GridView ID="gvEstadisticas" OnRowCommand="gvEstadisticas_RowCommand" runat="server" AutoGenerateColumns="False" 
        CssClass="table table-sm" HeaderStyle-CssClass="thead-dark" HeaderStyle-BackColor="#243054"
        HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="LightBlue" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
<AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
    <Columns>
        <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
        <asp:BoundField HeaderText="Codigo Usuario" DataField="CodigoUsuario" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" >
<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField HeaderText="FechaHora" DataField="FechaHora" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" >
<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField HeaderText="Navegador" DataField="Navegador" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" >
<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField HeaderText="Fabricante Dispostivo" DataField="FabricanteDispostivo" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" >
<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField HeaderText="Plataforma Dispositivo" DataField="PlataformaDispositivo" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" >
<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField HeaderText="Vista" DataField="Vista" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" >
<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField HeaderText="Accion" DataField="Accion" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" >      
<ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
        </asp:BoundField>
    </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

<HeaderStyle BackColor="#5D7B9D" CssClass="thead-dark" ForeColor="White" Font-Bold="True"></HeaderStyle>
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <asp:Label ID="lblStatus" ForeColor="Maroon" runat="server" Visible="false" />
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

</asp:Content>
