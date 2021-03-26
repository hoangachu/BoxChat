/*/*const { Toast } = require("bootstrap");*/

const REGIST_SUCCESS = 1;
$(document).ready(function () {

    $.notify("Your Break is Approved", {
        style: 'happyblue',
        className: 'superblue', // <-- just a comma, but really important
        title: "E-Contact Application"
    });


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
    
 
    var Username = $("#Username").val();
    var Password = $("#Password").val();
    
    
    $.ajax({

        url: "User/RegistUser",
        type: 'POST',
        async: false,
        data: { Username: Username, Password: Password},
        
        success: function (result) {
          
            if (result.data == REGIST_SUCCESS) {
                toastr.success('Regist success');
                /*toastr.info('Are you the 6 fingered man?');*/
                uploadfile(myfile, result.userID);
               
                
            }

        }


    });

});



