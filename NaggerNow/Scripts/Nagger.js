var COLUMN = {
    NONE:  { value: 1, name: "colNone"},
    COULD: { value: 2, name: "colCould" },
    SHOULD: { value: 3, name: "colShould" },
    MUST: { value: 4, name: "colMust" },
    DONE: { value: 5, name: "colDone" },
    SKIP: { value: 6, name: "colSkip" }
};

function value_of_enum(col, colName) 
{
    for (var k in col)
    {
   
        if (col[k].name == colName)
        {
            return col[k].value;
        }
    }
}

function Card(id, title, description, board, cardType, token, tokensAwarded, lastDone, columnID) {
    var self = this;

    self.id = id;

    self.title = title;

    self.description = description;

    self.board = board;

    self.cardType = cardType;

    self.token = token;

    self.tokensAwarded = tokensAwarded;

    self.lastDone = lastDone;

    self.columnID = columnID;

}

function CardsViewModel() {

    var self = this;
    
    //Get All Cards
    self.AllCards = ko.observableArray("");

   

    self.getAllCards = function () {

        var url = "/NagService.asmx/GetAllNags";
        NaggerConnect.getData(url, self.populateAllCards, 'GET', 'json', false);
    };

    self.populateAllCards = function (allData) {
        var temp = $.map(allData, function (item) { return new Card(item.id, item.title, item.description, item.board, item.cardType, item.token, item.tokensAwarded, item.lastDone, item.columnID) });
        self.AllCards(temp);
    };
    
    self.getAllCards();
       
    
    self.MustCards = ko.pureComputed(function () {
        if (self.AllCards() != null) {
            return ko.utils.arrayFilter(self.AllCards(), function (c) {
                return c.columnID == COLUMN.MUST.value;
            });
        }
        else
            return ko.observableArray("");
    });


    self.DoneCards = ko.pureComputed(function () {
        if (self.AllCards() != null) {
            return ko.utils.arrayFilter(self.AllCards(), function (c) {
                return c.columnID == COLUMN.DONE.value;
            });
        }
        else
            return ko.observableArray("");
    });


    self.CouldCards = ko.pureComputed(function () {
        if (self.AllCards() != null) {
            return ko.utils.arrayFilter(self.AllCards(), function (c) {
                return c.columnID == COLUMN.COULD.value;
            });
        }
        else
            return ko.observableArray("");
    });



    self.ShouldCards = ko.pureComputed(function () {
        if (self.AllCards() != null) {
            return ko.utils.arrayFilter(self.AllCards(), function (c) {
                return c.columnID == COLUMN.SHOULD.value;
            });
        }
        else
            return ko.observableArray("");
    });



    self.SkipCards = ko.pureComputed(function () {
        if (self.AllCards() != null) {
            return ko.utils.arrayFilter(self.AllCards(), function (c) {
                return c.columnID == COLUMN.SKIP.value;
            });
        }
        else
            return ko.observableArray("");
    });

      
    self.tokenCount = ko.observable("");

    self.sum = function (items, prop) {
        return items.reduce(function (a, b) {
            return a + b[prop];
        }, 0);
    };

    self.tokenCount = self.sum(self.AllCards(), 'tokensAwarded');

}

function MoveInfo(event, ui) {
        
    var column = $(this).attr('id');
    
    var id = ui.item.attr('id');
    var title = ui.item.attr('title');
    var board = ui.item.attr('board');
    var cardType = ui.item.attr('cardType');
    var lastDone = ui.item.attr('lastDone');
    var description = ui.item[0].childNodes['3'].innerHTML;
    
    var currentCard = new Card(id, title, description, board, cardType, 0, 0, lastDone, value_of_enum(COLUMN, column));
          
    currentCard = JSON.stringify(currentCard);

    var encoded = encodeURIComponent(currentCard);

    var url = "/NagService.asmx/UpdateCard?Nag=" + encoded;
        
    NaggerConnect.getData(url, null, 'GET', 'json', false);
    return true;
}

function doStuff() {

    var svm = new CardsViewModel();

    ko.applyBindings(svm, document.getElementById("CardsContainer"));

   


    $("#colMust").sortable({
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
        connectWith: "#colCould, #colShould, #colMust",
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


    $("#colShould").sortable({
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

    $("#colCould").sortable({
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
        connectWith: "#colCould, #colShould",
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