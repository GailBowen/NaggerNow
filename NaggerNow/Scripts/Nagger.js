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

function Card(id, title, description, board, frequencyID, lastDone, columnID, dueDate, skipCount) {
    var self = this;

    self.id = id;

    self.title = title;

    self.description = description;

    self.board = board;

    self.frequencyID = frequencyID;

    self.lastDone = lastDone;

    self.columnID = columnID;

    self.dueDate = dueDate;

    self.skipCount = skipCount;
}

function CardsViewModel() {

    var self = this;
        
    self.AllCards = ko.observableArray("");
    
    self.getAllCards = function () {

        var url = "/NagService.asmx/GetAllNags";
        NaggerConnect.getData(url, self.populateAllCards, 'GET', 'json', false);
    };

    self.populateAllCards = function (allData) {
        var temp = $.map(allData, function (item) { return new Card(item.id, item.title, item.description, item.board, item.frequencyID, item.lastDone, item.columnID, item.dueDate, item.skipCount) });
        self.AllCards(temp);
    };
    
    self.getAllCards();

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


    self.SkipCards = ko.pureComputed(function () {
        if (self.AllCards() != null) {
            return ko.utils.arrayFilter(self.AllCards(), function (c) {
                return c.columnID == COLUMN.SKIP.value;
            });
        }
        else
            return ko.observableArray("");
    });
    
 }

function MoveInfo(event, ui) {
    
    var previousColumn = ui.sender.attr('id');

    var column = $(this).attr('id');
        
    var id = ui.item.attr('id');
    var title = ui.item.attr('title');
    var board = ui.item.attr('board');
    var frequencyID = ui.item.attr('frequencyID');
    var lastDone = ui.item.attr('lastDone');
    var dueDate = ui.item.attr('dueDate');
    var skipCount = ui.item.attr('skipCount');
    var description = ui.item[0].childNodes['3'].innerHTML;
                            
    var currentCard = new Card(id, title, description, board, frequencyID, lastDone, value_of_enum(COLUMN, column), dueDate, skipCount);
          
    currentCard = JSON.stringify(currentCard);

    var encoded = encodeURIComponent(currentCard);
    
    var url = "/NagService.asmx/ProcessCard?Nag=" + encoded + "&fromColumn=" + previousColumn.substring(3) + "&toColumn=" + column.substring(3);
            
    NaggerConnect.getData(url, getResult, 'GET', 'json', false);

    location.reload();

    return true;
}

function getResult(allData) {
    //if (allData.successMessage.length > 0) {
    //    alert(allData.successMessage);
    //}
}

function doStuff() {

    var svm = new CardsViewModel();

    ko.applyBindings(svm, document.getElementById("CardsContainer"));
    
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