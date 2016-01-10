<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <title>Nagger</title>
  
  <link href="Content/themes/smoothness/jquery-ui.smoothness.css" rel="stylesheet" />
  <link href="Content/Nagger.css" rel="stylesheet" />

  <script src="Scripts/jquery-2.1.4.js"></script>
  <script src="Scripts/jquery-ui-1.11.4.js"></script>
  <script src="Scripts/knockout-3.4.0.js"></script>

  <script src="Scripts/NaggerConnect.js"></script>
  <script src="Scripts/Nagger.js"></script>
  
  <script>
      $(doStuff);
  </script>

</head>
<body>

      <div id="CardsContainer">
      
        <table>
            <tr>
                <td>
                    <div class="columnHeader">
                        <h2 style="text-align:center">Optional</h2>
                    </div>
                </td>
                <td>
                    <div class="columnHeader">
                        <h2 style="text-align:center">Mandated</h2>
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
            
               <div id="colOpt" class="col" data-bind="foreach: OptionalCards">

                    <div class="portlet" data-bind="attr: { id: id }">
                      <div class="portlet-header portlet-header-optional" data-bind="text: title + ' (' + tokensAwarded + ')'"></div>
                      <div class="portlet-content" data-bind="text: description"></div>
                    </div>

                </div>

                </td>

                <td style="vertical-align:top">
                    <div id="colMan" class="col" data-bind="foreach: Cards">

                    <div class="portlet" data-bind="attr: { id: id }">
                      <div class="portlet-header portlet-header" data-bind="text: title"></div>
                      <div class="portlet-content" data-bind="text: description"></div>
                    </div>

                </div>
                </td>

           

                <td style="vertical-align:top">
                <div id="colDone" class="col">

                    <div class="portlet" id="Politics">
                        <div class="portlet-header">Politics</div>
                        <div class="portlet-content">Spectator podcast</div>
                    </div>

                     <div class="portlet" id="BetterPlace">
                        <div class="portlet-header">Better place</div>
                        <div class="portlet-content">MicroLoan?</div>
                    </div>

                </div>
                </td>

                <td style="vertical-align:top">
                <div id="colSkip" class="col">

                    <div class="portlet" id="Bike">
                        <div class="portlet-header portlet-header-optional">Bike</div>
                        <div class="portlet-content">Fix tyre</div>
                    </div>

                    <div class="portlet" id="Meters">
                        <div class="portlet-header portlet-header-optional">Meters</div>
                        <div class="portlet-content">Gas and Electricity</div>
                    </div>

                </div>
                </td>
            </tr>
        </table>

        <h1 data-bind="text: 'Token Count: ' + tokenCount"></h1>
    </div>
    
</body>
  
</html>