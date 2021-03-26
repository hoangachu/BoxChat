function uploadfile(myfile) {

    var formData = new FormData();

    if (myfile.files.length > 0) {
        for (var i = 0; i < myfile.files.length; i++) {
            formData.append('file', myfile.files[i]);
        }
    }

    $.ajax({
        url: "/File/SaveFile/",
        type: "POST",
        dataType: "json",
        data: formData,
        contentType: false,
        processData: false,
        success: function (data) {

        },
        error: function (data) {

        }
    })
}