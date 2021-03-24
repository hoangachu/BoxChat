$(document).ready(function () {




});


$("#btnlogin").click(function () {
   
    var Username = $("#Username").val();
    var Password = $("#Password").val();
    $.ajax({

        url: "Home/Login",
        type: 'POST',
        data: { Username: Username, Password: Password },
        success: function (result) {
            $("#div1").html(result);
        }
        

    });



});