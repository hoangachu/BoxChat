const ACCEPT_LOGIN = 1;
$(document).ready(function () {



    $(".toggle-password").click(function () {

        $(this).toggleClass("fa-eye fa-eye-slash");
        var input = $($(this).attr("toggle"));
        if (input.attr("type") == "password") {
            input.attr("type", "text");
        } else {
            input.attr("type", "password");
        }
    });
});

$("#btnlogin").click(function () {
 
    var Username = $("#Username").val();
    var Password = $("#Password").val();
    
    
    $.ajax({

        url: "Home/Login",
        type: 'POST',
        data: { Username: Username, Password: Password},
        cache: false,
        processData: false,
        success: function (result) {
          
            if (result.value.data == ACCEPT_LOGIN) {
              
                window.open(result.value.url, '_self')
            }

        }


    });

});



