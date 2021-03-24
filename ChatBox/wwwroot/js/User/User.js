const ACCEPT_LOGIN = "1";
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
          
            if (result.value.data == ACCEPT_LOGIN) {
              
                window.open(result.value.url, '_self')
            }

        }


    });

   


});
