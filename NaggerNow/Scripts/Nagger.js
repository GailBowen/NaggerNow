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

    self.getOptionalCards = function () {

        var url = "/NagService.asmx/GetOptionalNags";
        NaggerConnect.getData(url, self.populateOptionalCards, 'GET', 'json', false);
    };

    self.populateOptionalCards = function (allData) {
        var temp = $.map(allData, function (item) { return new Card(item.id, item.title, item.description, item.board, item.cardType, item.token, item.tokensAwarded, item.lastDone) });
        self.OptionalCards(temp);
    };

    self.getOptionalCards();

    //Get Skipped cards
    self.SkippedCards = ko.observableArray("");

    self.getSkippedCards = function () {

        var url = "/NagService.asmx/GetSkippedNags";
        NaggerConnect.getData(url, self.populateSkippedCards, 'GET', 'json', false);
    };

    self.populateSkippedCards = function (allData) {
        var temp = $.map(allData, function (item) { return new Card(item.id, item.title, item.description, item.board, item.cardType, item.token, item.tokensAwarded, item.lastDone) });
        self.SkippedCards(temp);
    };

    self.getSkippedCards();
    
    self.tokenCount = ko.observable("");

    self.sum = function (items, prop) {
        return items.reduce(function (a, b) {
            return a + b[prop];
        }, 0);
    };

    self.tokenCount = self.sum(self.OptionalCards(), 'tokensAwarded');

}

function MoveInfo(event, ui) {
        
    var column = $(this).attr('id');
    
    var currentCard = new MiniCard(ui.item.attr('id'), 'test');

    currentCard = JSON.stringify(currentCard);

    var encoded = encodeURIComponent(currentCard);

    switch (column) {
        case 'colOpt':
            var url = "/NagService.asmx/NagMovedToOptional?Nag=" + encoded;
            break;

        case 'colMan':
            var url = "/NagService.asmx/NagMovedToMandated?Nag=" + encoded;
            break;

        case 'colDone':
            var url = "/NagService.asmx/NagDone?Nag=" + encoded;
            break;

        case 'colSkip':
            var url = "/NagService.asmx/NagMovedToSkipped?Nag=" + encoded;
            break;

        default:
            alert('A despicable error has occured');
    }

    



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