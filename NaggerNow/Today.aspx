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
                        <h2 style="text-align:center">Could Do</h2>
                    </div>
                </td>
                <td>
                    <div class="columnHeader">
                        <h2 style="text-align:center">Should Do</h2>
                    </div>
                </td>
                <td>
                    <div class="columnHeader">
                        <h2 style="text-align:center">Must Do</h2>
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
            
                   <div id="colCould" class="col" data-bind="foreach: CouldCards">

                        <div class="portlet" data-bind="attr: { id: id, title: title, 'description': description, 'board': board, 'cardType': cardType, 'lastDone': lastDone }">
                          <div class="portlet-header portlet-header-optional" data-bind="text: title + ' (' + token + ')'"></div>
                          <div class="portlet-content" data-bind="text: description" contenteditable="true"></div>
                        </div>

                    </div>

                </td>
                
                <td style="vertical-align:top">
            
                   <div id="colShould" class="col" data-bind="foreach: ShouldCards">

                        <div class="portlet" data-bind="attr: { id: id, title: title, 'description': description, 'board': board, 'cardType': cardType, 'lastDone': lastDone }">
                          <div class="portlet-header portlet-header-optional" data-bind="text: title + ' (' + token + ')'"></div>
                          <div class="portlet-content" data-bind="text: description" contenteditable="true"></div>
                        </div>

                    </div>

                </td>

                <td style="vertical-align:top">
                    <div id="colMust" class="col" data-bind="foreach: MustCards">

                        <div class="portlet" data-bind="attr: { id: id, title: title, 'description': description, 'board': board, 'cardType': cardType, 'lastDone': lastDone }">
                          <div class="portlet-header portlet-header" data-bind="text: title"></div>
                          <div class="portlet-content" data-bind="text: description" contenteditable="true"></div>
                        </div>

                    </div>
                </td>

           

                <td style="vertical-align:top">
                    <div id="colDone" class="col" data-bind="foreach: DoneCards">

                        <div class="portlet" data-bind="attr: { id: id, title: title, 'description': description, 'board': board, 'cardType': cardType, 'lastDone': lastDone }">
                          <div class="portlet-header portlet-header" data-bind="text: title"></div>
                          <div class="portlet-content" data-bind="text: description" contenteditable="true"></div>
                        </div>

                    </div>
                

                </td>

                <td style="vertical-align:top">
                 <div id="colSkip" class="col" data-bind="foreach: SkipCards">

                        <div class="portlet" data-bind="attr: { id: id, title: title, 'description': description, 'board': board, 'cardType': cardType, 'lastDone': lastDone }">
                          <div class="portlet-header portlet-header-optional" data-bind="text: title"></div>
                          <div class="portlet-content" data-bind="text: description" contenteditable="true"></div>
                        </div>

                    </div>
                </td>
            </tr>
        </table>

        <h1 data-bind="text: 'Token Count: ' + tokenCount"></h1>
    </div>
    
</body>
  
</html>