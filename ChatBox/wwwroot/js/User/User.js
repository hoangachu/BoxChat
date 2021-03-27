

const REGIST_SUCCESS = 1;
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


function RegistUser() {
    var myfile = document.getElementById("file-input");

    var formData = new FormData();

    if (myfile.files.length > 0) {
        for (var i = 0; i < myfile.files.length; i++) {
            formData.append('file', myfile.files[i]);
        }
    }
    var Username = $("#Username").val();
    var Password = $("#Password").val();
    formData.append('Username', Username);
    formData.append('Password', Password);

    $.ajax({

        url: "User/RegistUser",
        type: 'POST',
        async: false,
        data: formData,
        contentType: false,
        processData: false
       


    });
    window.location.href = "home";
  
};



