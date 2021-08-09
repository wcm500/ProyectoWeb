<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmSesion.aspx.cs" Inherits="AppIBULACIT.Views.FrmSesion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><asp:Label Text="Bitacora de sesiones" runat="server"></asp:Label></h1>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css" />
<link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.2.2/css/buttons.dataTables.min.css" />
<script type="text/javascript" src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.2/js/dataTables.buttons.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"></script>
<script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/pdfmake.min.js"></script>
<script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/vfs_fonts.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.2/js/buttons.html5.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('[id*=gvErrores]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
            dom: 'Bfrtip',
            'aoColumnDefs': [{ 'bSortable': false, 'aTargets': [0] }],
            'iDisplayLength': 5,
            buttons: [
                { extend: 'copy', text: 'Copy to clipboard', className: 'exportExcel', exportOptions: { modifier: { page: 'all' } } },
                { extend: 'excel', text: 'Export to Excel', className: 'exportExcel', filename: 'Errores_Excel', exportOptions: { modifier: { page: 'all' } } },
                { extend: 'csv', text: 'Export to CSV', className: 'exportExcel', filename: 'Errores_Csv', exportOptions: { modifier: { page: 'all' } } },
                { extend: 'pdf', text: 'Export to PDF', className: 'exportExcel', filename: 'Errores_Pdf', orientation: 'landscape', pageSize: 'LEGAL', exportOptions: { modifier: { page: 'all' }, columns: ':visible' } }
            ]
        });
    });
</script>

    <asp:GridView ID="gvSesiones" OnRowCommand="gvSesiones_RowCommand" runat="server"></asp:GridView>


</asp:Content>
