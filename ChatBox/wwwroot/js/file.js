function uploadfile(myfile, userID) {

    var formData = new FormData();

    if (myfile.files.length > 0) {
        for (var i = 0; i < myfile.files.length; i++) {
            formData.append('file', myfile.files[i]);
        }
    }
    formData.append('userID', userID);
    $.ajax({
        url: "/File/SaveFile/",
        type: "POST",
        dataType: "json",
        data: formData,
        contentType: false,
        processData: false,
        async: false,
        success: function (data) {
            if (data.data > 0) {
              
            }
            else {
            }

        },
        error: function (data) {

        }
    })
}