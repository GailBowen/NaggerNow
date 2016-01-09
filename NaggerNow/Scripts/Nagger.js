
Nagger = window.Nagger || {};

var baseURL = "";

var Nagger = function () {
    var self = this;

    getData = function (serviceURL, dataPopulationFunction, type, dataType, async, params) {
     
        $.ajax({
            url: serviceURL,
            type: type,
            async: async,
            datatype: dataType,
            success: function (allData) {
                if (dataPopulationFunction)
                    dataPopulationFunction(allData,params);
            },
            error: function (jqxhr, textStatus, error) {
                self.LogError(jqxhr, textStatus, error);
                //For certain error redirect back home
                if (jqxhr && jqxhr.status && (jqxhr.status == 401 || jqxhr.status == 550))
                    window.location.href = "../wfLogin.aspx?ReturnUrl=" + encodeURIComponent(window.location.pathname + window.location.search);
            }
        });
    }


    postData = function (serviceURL, data, returnFunction, type, dataType, async) {
        $.ajax({
            url: serviceURL,
            type: type,
            async: async,
            contentType: dataType,
            data: data,
            success: function (allData) {
                if (returnFunction)
                    returnFunction(allData);
            },
            error: function (jqxhr, textStatus, error) {
                self.LogError(jqxhr, textStatus, error);
                //For certain error redirect back home
                if (jqxhr && jqxhr.status && (jqxhr.status == 401 || jqxhr.status == 550))
                    window.location.href = "../wfLogin.aspx?ReturnUrl=" + encodeURIComponent(window.location.pathname + window.location.search);
            }
        });
    }

    //Helper functions
    //Returns the ID from an Knockout object - checks to see if object is null first
    objectIDToString = function (value) {
        var returnValue = "";
        if (value != null)
            returnValue = value.id;
        return returnValue;
    }

    valueToString = function (value) {
        var returnValue = "";
        if (value != null)
            returnValue = value;
        return returnValue;
    }


    //Error logging
    LogError = function (jqxhr, textStatus, error) {
        var errorDetail = $.parseJSON(jqxhr.responseText);
        var errorID = jqxhr.status;
        if (typeof window.console !== 'undefined') {
            if (errorDetail.Message) {
                console.error("Request Failed: " + error + ": " + errorDetail.Message);
                if (errorDetail.ExceptionMessage)
                    console.error("Request Failed: " + errorDetail.ExceptionMessage);
            }
            else if (error.message)
                console.error("Request Failed: " + error + ": " + error.message);
        }
        if (errorID == '500') {
            $('.ui-content').children().hide();
            var $errorMessageDiv = $("<div class='errormessage'>" + errorDetail.Message + "</div>");
            $('.ui-content[role=main]').append($errorMessageDiv);
            if (errorDetail.ExceptionMessage) {
                var $exceptionMessageDiv = $("<div class='errormessage'>" + errorDetail.ExceptionMessage + "</div>");
                $('.ui-content[role=main]').append($exceptionMessageDiv);
            }
            if (errorDetail.ExceptionType) {
                var $exceptionTypeDiv = $("<div class='errormessage'>" + errorDetail.ExceptionType + "</div>");
                $('.ui-content[role=main]').append($exceptionTypeDiv);
            }
            if (errorDetail.StackTrace) {
                var $stackTraceDiv = $("<div class='errormessage'>" + errorDetail.StackTrace + "</div>");
                $('.ui-content[role=main]').append($stackTraceDiv);
            }

        }
    }

    CheckLocation = function (redirectURL, queryString) {

        if (queryString == null)
            queryString = "";

        if (window.location.hash != "") {
            if (redirectURL)
                window.location.href = redirectURL + queryString;
            else
                window.location.href = window.location.pathname + queryString;
            return false;
        }
    }


    isDecimalNumber = function ( evt) {
  
        var theEvent = evt || window.event;
        var charCode = (theEvent.which) ? theEvent.which : theEvent.keyCode;

        if (charCode >= 96 && charCode <= 105)
            return true;

        if (charCode != 46 && charCode > 31
          && (charCode < 48 || (charCode > 57 && charCode != 190 && charCode != 110)))
            return false;

        return true;
    }

    isNumberKey = function ( evt) {
        var theEvent = evt || window.event;
        var charCode = (theEvent.which) ? theEvent.which : theEvent.keyCode;
        if (charCode != 46 && charCode > 31
          && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }

    isAlpaNumeric = function (evt) {

        var charCode = (evt.which) ? evt.which : event.keyCode;

        if ((charCode > 47 && charCode < 58) || (charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123)) {
            return true;
        }

        return false;
    }

    //The following converts a date to the number of millisecond since January 1, 1970. The extra date formatting is required so it works in Safari
    toMilliSeconds = function (dateString) {
        var arrDate = dateString.split(/[^0-9]/);
        var d = new Date(arrDate[0], arrDate[1] - 1, arrDate[2], arrDate[3], arrDate[4], arrDate[5]);
        return Date.parse(d);
    }



    return {
        "getData": getData,
        "postData": postData,
        "objectIDToString": objectIDToString,
        "valueToString" : valueToString,
        "CheckLocation": CheckLocation,
        "isNumberKey": isNumberKey,
        "isAlpaNumeric": isAlpaNumeric,
        "toMilliSeconds": toMilliSeconds,
        "isDecimalNumber": isDecimalNumber
    };
}();


String.prototype.startsWith = function (prefix) {
    return this.indexOf(prefix) === 0;
}

String.prototype.endsWith = function (suffix) {
    return this.match(suffix + "$") == suffix;
};

