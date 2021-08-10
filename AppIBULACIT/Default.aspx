<%@ Page Async="true" Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppIBULACIT._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.5.0/Chart.min.js"></script>

    <div class="jumbotron">
        <h1>Dash Board Admin</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>
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
    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
