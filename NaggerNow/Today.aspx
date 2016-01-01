<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <title>Nagger</title>
  
  <link href="Content/themes/smoothness/jquery-ui.smoothness.css" rel="stylesheet" />

  <script src="Scripts/jquery-1.6.4.js"></script>
  <script src="Scripts/jquery-ui-1.11.1.js"></script>

  <style>

  .tilt {
    transform: rotate(20deg);
    -moz-transform: rotate(20deg);
    -webkit-transform: rotate(20deg);
  }

  body {
    min-width: 30px;
  }

  .columnHeader {
    width: 288px;
    padding-bottom: 0.5px;
    font-family: Verdana,Arial,sans-serif;
	font-size: 1em;
  }

  .colMan {
    width: 288px;
    float:left;
    padding-bottom: 2px;
    background-color: orange;
  }

  .colOpt {
    width: 288px;
    float:left;
    padding-bottom: 2px;
    background-color: yellow;
  }

   .colDone {
    width: 288px;
    float:left;
    padding-bottom: 2px;
    background-color: greenyellow;
  }

   .colSkip {
    width: 288px;
    float:left;
    padding-bottom: 2px;
    background-color: gray;
  }
     

  .portlet {
    margin: 1em 1em 1em 1em;
    padding: 0.3em;
  }
  .portlet-header {
    padding: 0.2em 0.3em;
    margin-bottom: 0.5em;
    position: relative;
    background-color: mediumpurple;
    background-image: none;
  }

   .portlet-header-healthy {
    background-color: #92dc13;
  }


  .portlet-header-optional {
    background-color: lightblue;
  }
  .portlet-toggle {
    position: absolute;
    top: 50%;
    right: 0;
    margin-top: -8px;
  }
  .portlet-content {
    padding: 0.4em;
   
  }
  .portlet-placeholder {
    border: 1px dotted black;
    margin: 0 1em 1em 0;
    height: 50px;
  }
  </style>
  <script>
      $(onPageLoad);

      function onPageLoad() {
    
          $(".colMan").sortable({
              connectWith: ".colDone",
              handle: ".portlet-header",
              cancel: ".portlet-toggle",
              start: function (event, ui) {
                  ui.item.addClass('tilt');
              },
              stop: function (event, ui) {
                  ui.item.removeClass('tilt');
              }
          });

          $(".colDone").sortable({
              connectWith: ".colOpt, .colMan",
              handle: ".portlet-header",
              cancel: ".portlet-toggle",
              start: function (event, ui) {
                  ui.item.addClass('tilt');
              },
              stop: function (event, ui) {
                  ui.item.removeClass('tilt');
              }
          });


          $(".colOpt").sortable({
              connectWith: ".colSkip, .colDone",
              handle: ".portlet-header",
              cancel: ".portlet-toggle",
              start: function (event, ui) {
                  ui.item.addClass('tilt');
              },
              stop: function (event, ui) {
                  ui.item.removeClass('tilt');
              }
          });

          $(".colSkip").sortable({
              connectWith: ".colOpt",
              handle: ".portlet-header",
              cancel: ".portlet-toggle",
              start: function (event, ui) {
                  ui.item.addClass('tilt');
              },
              stop: function (event, ui) {
                  ui.item.removeClass('tilt');
              }
          });

      


          $(".portlet")
            .addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
            .find(".portlet-header")
              .addClass("ui-widget-header ui-corner-all")
              .prepend("<span class='ui-icon ui-icon-minusthick portlet-toggle'></span>");

          $(".portlet-toggle").click(function () {
              var icon = $(this);
              icon.toggleClass("ui-icon-minusthick ui-icon-plusthick");
              icon.closest(".portlet").find(".portlet-content").toggle();
          });
      }
  </script>

</head>
<body>





    <table>
        <tr>
            <td>
                <div class="columnHeader">
                    <h2 style="text-align:center">Must Do</h2>
                </div>
            </td>
            <td>
                <div class="columnHeader">
                    <h2 style="text-align:center">Optional</h2>
                </div>
            </td>
            <td>
                <div class="columnHeader">
                    <h2 style="text-align:center">Done</h2>
                </div>
            </td>
            <td>
                <div class="columnHeader">
                    <h2 style="text-align:center">Skipped</h2>
                </div>
            </td>
        </tr>

        <tr>
            <td style="vertical-align:top">
            <div class="colMan">

                <div class="portlet">
                    <div class="portlet-header">Money</div>
                    <div class="portlet-content">Sat Nav sync?</div>
                </div>

                <div class="portlet">
                    <div class="portlet-header">Brave</div>
                    <div class="portlet-content">Climbing?</div>
                </div>

                <div class="portlet">
                    <div class="portlet-header">Work</div>
                    <div class="portlet-content">Pluralsight</div>
                </div>

                <div class="portlet">
                    <div class="portlet-header">Artists</div>
                    <div class="portlet-content">
                        Del Piombo 
                                <br />
                        Parmigiano
                                <br />
                        Bruegel
                                <br />
                        Altdorfer
                                <br />
                        Pontormo
                    </div>
                </div>

                <div class="portlet">
                    <div class="portlet-header">Politics</div>
                    <div class="portlet-content">Spectator podcast</div>
                </div>

                <div class="portlet">
                    <div class="portlet-header">Blog</div>
                    <div class="portlet-content">Sugar</div>
                </div>

                <div class="portlet">
                    <div class="portlet-header portlet-header-healthy">Vegetables</div>
                    <div class="portlet-content">Winter Squash</div>
                </div>


                <div class="portlet">
                    <div class="portlet-header portlet-header-healthy">Fruit</div>
                    <div class="portlet-content">Blackberries</div>
                </div>

            </div>
            </td>

            <td style="vertical-align:top">
            <div class="colOpt">

                <div class="portlet">
                    <div class="portlet-header portlet-header-optional">Sofa</div>
                    <div class="portlet-content">Polish the sofa with nourishing cream</div>
                </div>

                 <div class="portlet">
                    <div class="portlet-header portlet-header-optional">Cat</div>
                    <div class="portlet-content">Play with cat</div>
                </div>

            </div>
            </td>

            <td style="vertical-align:top">
            <div class="colDone">

                <div class="portlet">
                    <div class="portlet-header">Politics</div>
                    <div class="portlet-content">Spectator podcast</div>
                </div>

                 <div class="portlet">
                    <div class="portlet-header">Better place</div>
                    <div class="portlet-content">MicroLoan?</div>
                </div>

            </div>
            </td>

            <td style="vertical-align:top">
            <div class="colSkip">

                <div class="portlet">
                    <div class="portlet-header portlet-header-optional">Bike</div>
                    <div class="portlet-content">Fix tyre</div>
                </div>

                <div class="portlet">
                    <div class="portlet-header portlet-header-optional">Meters</div>
                    <div class="portlet-content">Gas and Electricity</div>
                </div>

            </div>
            </td>
        </tr>
    </table>
</body>
</html>