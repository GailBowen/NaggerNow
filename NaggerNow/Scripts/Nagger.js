
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
        NaggerConnect.getData(url, self.populateCards, 'GET', 'json', false);
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

function MoveInfo(event, ui) {
    alert($(this).attr('id'));
    alert(ui.item.attr('id'));
    return true;
}

function doStuff() {

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