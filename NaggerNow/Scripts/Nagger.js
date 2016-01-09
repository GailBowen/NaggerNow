
InfracastAPI = window.InfracastAPI || {};

var baseURL = "";

var InfracastAPI = function () {
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


function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}


function daysBetween(first, second) {

    // Copy date parts of the timestamps, discarding the time parts.
    var one = new Date(first.getFullYear(), first.getMonth(), first.getDate());
    var two = new Date(second.getFullYear(), second.getMonth(), second.getDate());

    // Do the math.
    var millisecondsPerDay = 1000 * 60 * 60 * 24;
    var millisBetween = two.getTime() - one.getTime();
    var days = millisBetween / millisecondsPerDay;

    // Round down.
    return Math.floor(days);
}

function SaveTourStatus(pagename) {
    //InfracastAPI.postData('wsRefData.asmx/SaveTourStatus', pagename, function (data) { }, 'POST', 'text/plain', false);
    //var postData = JSON.stringify({ formVars: pagename });
    //var jd = JSON.parse(postData);
    //var errorReport = '';
    var formVars = 'wfSendMessage';
    $.ajax({
        type: "Get",
        url: "wsRefData.asmx/SaveTourStatus?formVars=" + pagename,
        contentType: "plain/text; charset=utf-8",
        data: formVars,
        dataType: "text",
        cache: false,
        //processData: true,
        success: function (data) {
        },
        async: false,
        error: function (jqXHR, textStatus, errorThrown) {
            //var responseData = $.parseJSON(jqXHR.responseText);
            //var parsedData = $.parseJSON(test.d);
            //send error to client
        }
    });

}