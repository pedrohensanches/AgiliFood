$(document).ready(function () {
    $("#botao-sair").click(function () {
        $.ajax({
            url: "/Seguranca/Logout",
            type: "GET",
            async: true,
            success: function () { window.location.href = "/Home/Login" }
        });
    });
});