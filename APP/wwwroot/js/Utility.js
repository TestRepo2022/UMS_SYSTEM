
//genric javacript function to validate textbox value and datatype
function requiredTextField(controlId, errormsg, validationType) {
    var ctrlId = "#txt" + controlId
    var formGroup = "#formGroup" + controlId
    var errmsg = "#err" + controlId
    var val = $(ctrlId).val()
    if (val == "" || val == undefined || val == null) {
        $(formGroup).addClass("error-control")
        $(errmsg).html("Please enter " + errormsg)
        $(ctrlId).focus()
        return false
    }
    else if (!GetRegx(validationType).test(val)) {
        $(formGroup).addClass("error-control")
        $(errmsg).html("Please enter valid " + errormsg)
        $(ctrlId).focus()
        return false
    }
    else {
        $(formGroup).removeClass("error-control")
        $(errmsg).html("")
        return true
    }
}




function requiredSelectFiled(control, ErrorMessage) {
    var id = "#ddl" + control
    var err = "#err" + control
    var formGroup = "#formGroup" + control
    var txtVal = $(id).val()
    if (txtVal == "" || txtVal == null || txtVal == "-1") {
        $(err).html("Please select " + ErrorMessage).addClass("error-control")
        $(formGroup).addClass("error-control")
        return false
    }
    else {

        $(err).html("").removeClass("error-control")
        $(formGroup).removeClass("error-control")
        return true
    }
}

function comparePassword(control1, control2) {
    var id = "#txt" + control1
    var id1 = "#txt" + control2
    var err = "#err" + control2
    var formGroup = "#formGroup" + control2
    var txtVal = $(id).val()
    var txtVal1 = $(id1).val()
    if (txtVal != txtVal1) {
        $(err).html("Confirm password not matched !").addClass("error-control")
        $(formGroup).addClass("error-control")
        return false
    }
    else {

        $(err).html("").removeClass("error-control")
        $(formGroup).removeClass("error-control")
        return true
    }
}

function GetRegx(type) {

    var regx = /.+/s;
    switch (type) {
        case "email":
            regx = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            break;

        case "mobile":
            regx = /^[1-9]{1}[0-9]{9}$/;
            break;
    }
    return regx;
}

function FillDropDownList(url, params, ddlId, async = true) {

    var ddl = "<option value='-1'>Select</option>"
    $.ajax({
        "url": base_url + url,
        "method": "GET",
        contentType: JSON,
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        "data": params,
        async: async,
        "success": function (response) {
            if (response.ok) {

                response.data.forEach(function (item, i) {
                    ddl += "<option value='" + item.id + "'>" + item.name + "</option>"
                })
                $("#" + ddlId).html(ddl)
            }

        },
        "error": function (err) {
            console.log(err)
        }
    })
}


function apiCallWithoutAuth(url, type, data, fnSuccess, async = true) {
   var base_url= "https://localhost:44396/api/"
    $.ajax({
        url: base_url+url,
        method: type,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(data),
        async: async,
        success: function (response) {
            fnSuccess(response)
        },
        "error": function (err) {
            console.error(err)
        }
    })
}

function apiCallWithoutAuth(url, type, data, fnSuccess, fnError, async = true) {
    $.ajax({
        "url": base_url + "Claim/RequestClaim",
        "method": "POST",
        cache: false,
        contentType: false,
        processData: false,
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        "data": data,
        "success": function (response) {
            if (response.ok) {
                $("#msg").html(response.message).css("color", "green")
                setTimeout(function () {
                    location.reload()
                }, 3000)
            }
            else {
                $("#msg").html(response.message).css("color", "red")
            }
        },
        "error": function (err) {
            console.log(err)
        }
    })
}

$(document).ready(function () {
    $("#btnReload").click(function () {
        window.location.reload()
    })
})