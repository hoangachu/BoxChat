"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("btnSendMsg").disabled = true;

connection.on("ReceiveMessage", function (userIDSend, user, message, ckUser, imgUrl) {
    var userIDReceive = $("#userID").val();
    var today = new Date();
    var dateTime = today.getDate() + "/" + (today.getMonth() + 1) + "/" + today.getFullYear() + "  " + today.getHours() + ":" + today.getMinutes();
    if (userIDSend != userIDReceive) {
        var div0 = document.createElement("div");

        div0.className += "d-flex justify-content-start mb-4 ml-2 mt-3";
        var div1 = document.createElement("div");
        div1.className += "img_cont_msg";
        div1.innerHTML = '<img src = "' + imgUrl + '" class=" rounded-circle user_img_msg">';

        var div2 = document.createElement("div");
        div2.className += "msg_cotainer";

        div2.innerHTML = message + '<span class="msg_time">' + dateTime + '</span>';
        div0.appendChild(div1);
        div0.appendChild(div2);
        document.getElementById("messagesList").appendChild(div0);


    }
    else {
        var div0 = document.createElement("div");

        div0.className += "d-flex justify-content-end mb-4 mr-2 mt-3";
        var div1 = document.createElement("div");
        div1.className += "msg_cotainer_send";
        div1.innerHTML = message + '<span class="msg_time_send">' + dateTime + '</span>'

        var div2 = document.createElement("div");
        div2.className += "img_cont_msg";

        div2.innerHTML = ' <img src = "' + imgUrl + '" class=" rounded-circle user_img_msg"> ';
        div0.appendChild(div1);
        div0.appendChild(div2);
        document.getElementById("messagesList").appendChild(div0);

    }
    var innerhtmlmsg = document.getElementById("messagesList").innerHTML;
    $.ajax({
        url: "CreatXMLFile",
        data: { userid: $('#userID').val(), message: innerhtmlmsg },
        type: 'POST',
        sync: true

    });
    $(".emojionearea-editor").html('');
    $("#messageInput").val('');
    $('.outgoing_msg').css('border', 'none')
    $('.message-table-scroll').scrollTop($('.message-table-scroll')[0].scrollHeight);
});

connection.start().then(function () {
    document.getElementById("btnSendMsg").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("btnSendMsg").addEventListener("click", function (event) {
    var userIDReceive = document.getElementById("userID").value;
    var message = $('.emojionearea-editor').html();
    var imgURL = document.getElementById("imgURL").value;
    var username = document.getElementById("userName").value;
    connection.invoke("SendMessage", userIDReceive, username, message, imgURL).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

function scrollToLatestChatMessage(chatContainer) {
    console.log("Entry::scrollToLatestChatMessage in chat.js " + chatContainer);
    $(".msg_container_base").animate({
        scrollTop: $('.msg_container_base').prop("scrollHeight")
    }, 1000);
    console.log("Entry::scrollToLatestChatMessage in chat.js ");
}
