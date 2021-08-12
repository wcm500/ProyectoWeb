<%@ Page Async="true" Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppIBULACIT._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.5.0/Chart.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link href="Scripts/bootstrap.js" rel="stylesheet">
    <script src="Scripts/bootstrap.min.js"></script>
    <script src=”js/popper.min.js”></script>
    <script src="Scripts/bootstrap.js"></script>


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
       /* .card .card-header .card-title {
          margin-bottom: 3px;
          text-align: center;
           margin-top: 15px;
          color: #3C4858;
          padding: 7px;
          margin-top: -20px;
          padding-top: 10px;
          margin-bottom: 0.75rem;

        }*/

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

        .divbackTree {
              height: 400px;
              border-radius: 3px;
              background: #FCFCFC;
              box-shadow: 0 2px 2px 0 rgb(0 0 0 / 14%), 0 3px 1px -2px rgb(0 0 0 / 20%), 0 1px 5px 0 rgb(0 0 0 / 12%);
              border-radius: 6px;
         }

    </style>
    <div class="jumbotron">
        <div class="card-body">
        <h1>Dash Board </h1>
        <p class="lead">Control bancario de la aplicación de todos los movimientos transferencias ocurridas en tiempo real</p>
            </div>
    </div>

   <%-- <div class="card-deck">
  <div class="card">
      <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
    <img class="card-img-top" src="img/ganancia.png" " alt="Card image cap">
          </div>
    <div class="card-body">
      <h5 class="card-title">Card title</h5>
      <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</p>
    </div>
    <div class="card-footer">
      <small class="text-muted">Last updated 3 mins ago</small>
    </div>
  </div>
  <div class="card">
    <img class="card-img-top" src="..." alt="Card image cap">
    <div class="card-body">
      <h5 class="card-title">Card title</h5>
      <p class="card-text">This card has supporting text below as a natural lead-in to additional content.</p>
    </div>
    <div class="card-footer">
      <small class="text-muted">Last updated 3 mins ago</small>
    </div>
  </div>
  <div class="card">
    <img class="card-img-top" src="..." alt="Card image cap">
    <div class="card-body">
      <h5 class="card-title">Card title</h5>
      <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This card has even longer content than the first to show that equal height action.</p>
    </div>
    <div class="card-footer">
      <small class="text-muted">Last updated 3 mins ago</small>
    </div>
  </div>
</div>--%>

    <div class="container">
    <div class="row">
        <div class="column col-md-4">
            <div class="col-sm-6">
              <div class="card text-white bg-secondary mb-3">
                <div class="card-header card-header-warning card-header-icon">
                    
                  <h3 style="text-align:center" class="card-title"><span class="glyphicon glyphicon-equalizer" aria-hidden="true"></span>   Soporte Cliente
                  </h3>
                </div>
                <div class="card-text">
                  <div class="stats">
                    <%--<a class="astyle" href="Views/frmServicioCliente.aspx">Mantenimiento de servicio cliente</a>--%>
                  </div>
                </div>
              </div>
            </div>
         </div>
           <div class="column col-md-4">
            <div class="col-sm-6">
              <div class="card text-white bg-primary  mb-3">
                <div class="card-header card-header-warning card-header-icon">
                  <h3 style="text-align:center" class="card-title"><span class="glyphicon glyphicon-equalizer" aria-hidden="true"></span>   Soporte Cliente
                  </h3>
                </div>
                <div class="card-text">
                  <div class="stats">
                    <%--<a class="astyle" href="Views/frmServicioCliente.aspx">Mantenimiento de servicio cliente</a>--%>
                  </div>
                </div>
              </div>
            </div>
         </div>
        <div class="column col-md-4"">
            <div class="col-sm-6">
              <div class="card text-white bg-success mb-3">
                <div class="card-header card-header-warning card-header-icon">
                  <h3 style="text-align:center" class="card-title"><span class="glyphicon glyphicon-equalizer" aria-hidden="true"></span>   Soporte Cliente
                  </h3>
                </div>
                <div class="card-text">
                  <div class="stats">
                    <%--<a class="astyle" href="Views/frmServicioCliente.aspx">Mantenimiento de servicio cliente</a>--%>
                  </div>
                </div>
              </div>
            </div>
         </div>

        </div>
       </div>

        <div class="row">
        <div class="col-md-4">
             <div class="divbackTree">



             </div>
            
           
        </div>
        <div class="col-md-4">
            <div class="divbackTree">



            </div>
          
           
        </div>
        <div class="col-md-4">
            <div class="divbackTree">



            </div>
        
        </div>
    </div>

   


    <div class="container">
    <div class="row">
        <div class="column">
            <div class="col-sm-6">
              <div class="card text-white bg-danger mb-3">
                <div class="card-header card-header-warning card-header-icon">
                    
                  <h3 style="text-align:center" class="card-title"><span class="glyphicon glyphicon-equalizer" aria-hidden="true"></span>   Soporte Cliente
                  </h3>
                </div>
                <div class="card-text">
                  <div class="stats">
                    <%--<a class="astyle" href="Views/frmServicioCliente.aspx">Mantenimiento de servicio cliente</a>--%>
                  </div>
                </div>
              </div>
            </div>
         </div>
           <div class="column">
            <div class="col-sm-6">
              <div class="card text-white bg-info mb-3">
                <div class="card-header card-header-warning card-header-icon">
                  <h3 style="text-align:center" class="card-title"><span class="glyphicon glyphicon-equalizer" aria-hidden="true"></span>   Soporte Cliente
                  </h3>
                </div>
                <div class="card-text">
                  <div class="stats">
                    <%--<a class="astyle" href="Views/frmServicioCliente.aspx">Mantenimiento de servicio cliente</a>--%>
                  </div>
                </div>
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
    </div>


</asp:Content>
