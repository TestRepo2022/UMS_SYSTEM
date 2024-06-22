$(document).ready(function () {
    $("#btnSubmit").click(function () {
        LoginUser()
    })
})

function LoginUser() {
    isvalid = requiredTextField("UserName", "user name", "email")
    if (!isvalid) { return false }

    isvalid = requiredTextField("Password", "password", "all")
    if (!isvalid) { return false }

    if (isvalid) {
        var url = "Auth/ValidateUser"
        var data = {
            "UserName": $("#txtUserName").val(),
            "Password": $("#txtPassword").val()
        }
        var fnSuccess = function (response) {
            console.log(response)
        }
        apiCallWithoutAuth(url, "POST", data, fnSuccess, true)
    }

}