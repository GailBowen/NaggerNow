<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <title>Nagger</title>
  
  <link href="Content/themes/smoothness/jquery-ui.smoothness.css" rel="stylesheet" />

  <script src="Scripts/jquery-2.1.4.js"></script>
  <script src="Scripts/jquery-ui-1.11.4.js"></script>
  <script src="Scripts/knockout-3.4.0.js"></script>
  <script src="Scripts/Nagger.js"></script>

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

  .col {
    width: 288px;
    float:left;
    padding-bottom: 2px;
    min-height:6em;
 }

  #colMan {
    background-color: orange;
  }

  #colOpt {
    background-color: yellow;
  }

   #colDone {
    background-color: greenyellow;
  }

   #colSkip {
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
   

          function Card(id, title, board, list, cardType, token, tokensAwarded) {
              var self = this;

              self.id = id;

              self.title = title;

              self.board = board;

              self.list = list;

              self.cardType = cardType;

              self.token = token;

              self.tokensAwarded = tokensAwarded;

          }
          

          function CardsViewModel() {

              var self = this;

              self.Cards = ko.observableArray("");
                            
              self.getCards = function () {

                  var url = "/NagService.asmx/GetNags";
                  Nagger.getData(url, self.populateCards, 'GET', 'json', false);
              };

              self.populateCards = function (allData) {
                  var temp = $.map(allData, function (item) { return new Card(item.id, item.title, item.board, item.list, item.cardType, item.token, item.tokensAwarded) });
                  self.Cards(temp);
              };

              self.getCards();


              //alert(self.Cards()[0].title);
              //alert(self.Cards()[0].cardType);

              self.tokenCount = ko.observable("");

              self.sum = function (items, prop) {
                  return items.reduce(function (a, b) {
                      return a + b[prop];
                  }, 0);
              };

              self.tokenCount = self.sum(self.Cards(), 'tokensAwarded');
              
              //alert(self.tokenCount);
          }


          var svm = new CardsViewModel();
          
          ko.applyBindings(svm, document.getElementById("CardsContainer"));
           

          $("#colMan").sortable({
              connectWith: "#colDone",
              receive: MoveInfo,
              handle: ".portlet-header",
              cancel: ".portlet-toggle",
              start: function (event, ui) {
                  ui.item.addClass('tilt');
              },
              stop: function (event, ui) {
                  ui.item.removeClass('tilt');
              }
          });

          $("#colDone").sortable({
              connectWith: "#colOpt, #colMan",
              receive: MoveInfo,
              handle: ".portlet-header",
              cancel: ".portlet-toggle",
              start: function (event, ui) {
                  ui.item.addClass('tilt');
              },
              stop: function (event, ui) {
                  ui.item.removeClass('tilt');
              }
          });


          $("#colOpt").sortable({
              connectWith: "#colSkip, #colDone",
              receive: MoveInfo,
              handle: ".portlet-header",
              cancel: ".portlet-toggle",
              start: function (event, ui) {
                  ui.item.addClass('tilt');
              },
              stop: function (event, ui) {
                  ui.item.removeClass('tilt');
              }
          });

          $("#colSkip").sortable({
              connectWith: "#colOpt",
              receive: MoveInfo,
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


      function MoveInfo(event, ui) {
          alert($(this).attr('id'));
          alert(ui.item.attr('id'));
          return true;
      }

      
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
            
           <div id="colOpt" class="col" data-bind="foreach: Cards">

                <div class="portlet" data-bind="attr: { id: id }">
                  <div class="portlet-header portlet-header-optional" data-bind="text: title + ' (' + tokensAwarded + ')'"></div>
                  <div class="portlet-content" data-bind="text: cardType"></div>
                </div>

            </div>

            </td>

            <td style="vertical-align:top">
            <div id="colMan" class="col">

                <div class="portlet" id="Money">
                    <div class="portlet-header">Money</div>
                    <div class="portlet-content">Sat Nav sync?</div>
                </div>

                <div class="portlet" id="Brave">
                    <div class="portlet-header">Brave</div>
                    <div class="portlet-content">Climbing?</div>
                </div>

                <div class="portlet" id="Work">
                    <div class="portlet-header">Work</div>
                    <div class="portlet-content">Pluralsight</div>
                </div>

                <div class="portlet" id="Artists">
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

                <div class="portlet" id="Blog">
                    <div class="portlet-header">Blog</div>
                    <div class="portlet-content">Sugar</div>
                </div>

                <div class="portlet" id="Vegetables">
                    <div class="portlet-header portlet-header-healthy">Vegetables</div>
                    <div class="portlet-content">Winter Squash</div>
                </div>


                <div class="portlet" id="Fruit">
                    <div class="portlet-header portlet-header-healthy">Fruit</div>
                    <div class="portlet-content">Blackberries</div>
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