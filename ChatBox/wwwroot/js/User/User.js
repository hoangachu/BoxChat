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
    var reader = new FileReader();
/*var file = $('#file-input')[0].files[0];*/
    var formData = new FormData($('#form_user')[0]);
    var Username = $("#Username").val();
    var Password = $("#Password").val();
    formData.append('image', $('input[type=file]')[0].files[0]); 
    formData.append('Username', Username); 
    formData.append('Password', Password); 
    /*var formData = new FormData('image', $('input[type=file]')[0].files[0]);*/
    
    
    $.ajax({

        url: "User/RegistUser",
        type: 'POST',
        data: {/* Username: Username, Password: Password,*/ base64img: formData },
        cache: false,
        processData: false,
        success: function (result) {
          
            if (result.value.data == ACCEPT_LOGIN) {
              
                window.open(result.value.url, '_self')
            }

        }


    });

   


});
function encodeImageFileAsURL(element) {
    var file = element.files[0];
    var reader = new FileReader();
    reader.onloadend = function () {
        imagebase64 = reader.result;
    }
    reader.readAsDataURL(file);
} 