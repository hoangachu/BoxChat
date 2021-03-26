const ACCEPT_LOGIN = 1;
$(document).ready(function () {




});


function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#blah').attr('src', e.target.result);
        }
   
        reader.readAsDataURL(input.files[0]); // convert to base64 string
    }
}


$("#btnRegistUser").click(function () {
    var myfile = document.getElementById("file-input");
    uploadfile(myfile);
 
    var Username = $("#Username").val();
    var Password = $("#Password").val();
    
    
    $.ajax({

        url: "User/RegistUser",
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



