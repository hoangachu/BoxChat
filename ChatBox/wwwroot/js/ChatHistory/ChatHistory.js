
$(document).ready(function () {

    setInterval(function () {
        //alert("Chào mừng bạn đến với freetuts.net");
    }, 3000);
    var input = document.getElementById("messageInput");
    input.addEventListener("keyup", function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            document.getElementById("btnSendMsg").click();
        }
    });
});




