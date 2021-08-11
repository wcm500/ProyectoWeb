<%@ Page Async="true" Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppIBULACIT._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.5.0/Chart.min.js"></script>
    <%--<link href="Stylesheet.css" rel="stylesheet" type="text/css" />--%>

    <style>
                    .column {
              float: left;
              width: 50%;
              padding: 7px;
            }
                    .card .card-footer .stats .material-icons {
  position: relative;
  top: -10px;
  margin-right: 3px;
  margin-left: 3px;
  font-size: 18px;
}

/* Clear floats after the columns */
        .row:after {
          content: "";
          display: table;
          clear: both;
        }

        .card .card-stats {
    background: transparent;
    display: flex;


}
        .card .card-header .card-title {
          margin-bottom: 3px;
          text-align: center;
           margin-top: 15px;
          color: #3C4858;
          padding: 7px;
          margin-top: -20px;
          padding-top: 10px;
          margin-bottom: 0.75rem;

        }

        .astyle{
            text-align: center;
        }

        .card-footer {
  padding: 0.75rem 1.25rem;
  background-color: #fff;
  border-top: 1px solid #eeeeee;
  border-radius: 0 0 calc(0.25rem - 1px) calc(0.25rem - 1px);
  padding: 0.9375rem 1.875rem;
}



        .card {
    border: 0;
    margin-bottom: 30px;
    margin-top: 30px;
    border-radius: 6px;
    color: #333;
    background: #fff;
    width: 200%;
    box-shadow: 0 2px 2px 0 rgb(0 0 0 / 14%), 0 3px 1px -2px rgb(0 0 0 / 20%), 0 1px 5px 0 rgb(0 0 0 / 12%);
    }
        html[dir="rtl"] .card.card-chart {
          direction: ltr;
        }

        .card-stats .card-header.card-header-icon, .card-stats .card-header.card-header-text {
    text-align: right;
        }

        .card [class*=card-header-] .card-icon, .card [class*=card-header-] .card-text {
    border-radius: 3px;
    background-color: #999;
    padding: 15px;
    margin-top: -20px;
    margin-right: 15px;
    float: left;
        }

        .card-stats .card-header .card-category:not([class*=text-]) {
    color: #999;
    font-size: 14px;
        }
        .card .card-header-success .card-icon,
        .card .card-header-success .card-text,
        .card .card-header-success:not(.card-header-icon):not(.card-header-text),
        .card.bg-success,
        .card.card-rotate.bg-success .front,
        .card.card-rotate.bg-success .back {
          background: linear-gradient(60deg, #66bb6a, #43a047);
        }

        .divback {
              height: 600px;
              border-radius: 3px;
              background: #FCFCFC;
              box-shadow: 0 2px 2px 0 rgb(0 0 0 / 14%), 0 3px 1px -2px rgb(0 0 0 / 20%), 0 1px 5px 0 rgb(0 0 0 / 12%);
              border-radius: 6px;
              padding: 1px;
         }

    </style>

    <div class="jumbotron">
        <h1>Dash Board Admin</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>
   <%-- <div class="row">
        <div class="col-lg-3 col-md-6 col-sm-6">
            <div class="card">
                <div class="card-header">
                    <div class="card-icon"></div>
                    <p class="card-category">Estadistica</p>

                </div>




            </div>


        </div>


    </div>--%>

    <div class="row">
            <div class="col-lg-3 col-md-2 col-sm-6">
              <div class="card card-stats">
                <div class="card-header card-header-warning card-header-icon">
                  <h3 class="card-title">Soporte Cliente
                  </h3>
                </div>
                <div class="card-footer">
                  <div class="stats">
                    <%--<a class="astyle" href="Views/frmServicioCliente.aspx">Mantenimiento de servicio cliente</a>--%>
                  </div>
                </div>
              </div>
            </div>
         </div>
    
    <div class="row">
        <div class="column">
             <div class="divback">
        <div id="canvas-holder" style="width:fit-content() auto">
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
        <div class="column">
            <div class="divback"></div>
        </div>
        <div class="column">
            <div class="divback"></div>
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
