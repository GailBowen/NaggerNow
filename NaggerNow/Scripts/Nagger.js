function MiniCard(id, description)
{
    var self = this;

    self.id = id;
    
    self.description = description;

}

function Card(id, title, description, board, cardType, token, tokensAwarded, lastDone) {
    var self = this;

    self.id = id;

    self.title = title;

    self.description = description;

    self.board = board;

    self.cardType = cardType;

    self.token = token;

    self.tokensAwarded = tokensAwarded;

    self.lastDone = lastDone;

}

function CardsViewModel() {

    var self = this;

    //Get Mandated Cards
    self.MandatedCards = ko.observableArray("");

    self.getMandatedCards = function () {

        var url = "/NagService.asmx/GetMandatedNags";
        NaggerConnect.getData(url, self.populateMandatedCards, 'GET', 'json', false);
    };

    self.populateMandatedCards = function (allData) {
        var temp = $.map(allData, function (item) { return new Card(item.id, item.title, item.description, item.board, item.cardType, item.token, item.tokensAwarded, item.lastDone) });
        self.MandatedCards(temp);
    };
    
    self.getMandatedCards();


    //Get cards done today
    self.DoneTodayCards = ko.observableArray("");

    self.getDoneTodayCards = function () {

        var url = "/NagService.asmx/GetDoneTodayNags";
        NaggerConnect.getData(url, self.populateDoneTodayCards, 'GET', 'json', false);
    };

    self.populateDoneTodayCards = function (allData) {
        var temp = $.map(allData, function (item) { return new Card(item.id, item.title, item.description, item.board, item.cardType, item.token, item.tokensAwarded, item.lastDone) });
        self.DoneTodayCards(temp);
    };

    self.getDoneTodayCards();
    
    
    //Get optional cards
    self.OptionalCards = ko.observableArray("");

    self.OptionalCards.push(new Card(22, 'Fox', 'Walk the fox', 'Fox', 'Optional', 3, 3, 0));
    
    //alert(self.Cards()[0].title);
    //alert(self.Cards()[0].cardType);

    self.tokenCount = ko.observable("");

    self.sum = function (items, prop) {
        return items.reduce(function (a, b) {
            return a + b[prop];
        }, 0);
    };

    self.tokenCount = self.sum(self.OptionalCards(), 'tokensAwarded');

    //alert(self.tokenCount);
}

function MoveInfo(event, ui) {

    //alert($(this).attr('id'));
    //alert(ui.item.attr('id'));

    var currentCard = new MiniCard(ui.item.attr('id'), 'test');

    currentCard = JSON.stringify(currentCard);

    var encoded = encodeURIComponent(currentCard);

    var url = "/NagService.asmx/NagDone?Nag=" + encoded;
    NaggerConnect.getData(url, null, 'GET', 'json', false);
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