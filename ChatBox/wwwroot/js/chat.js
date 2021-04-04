"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("btnSendMsg").disabled = true;

connection.on("ReceiveMessage", function (userIDSend, user, message, ckUser, imgUrl) {
    var userIDReceive = $("#userID").val();
    var today = new Date();
    var dateTime = today.getDate() + "/" + (today.getMonth() + 1) + "/" + today.getFullYear() + "  " + today.getHours() + ":" + today.getMinutes();
    if (userIDSend != userIDReceive) {
        var div0 = document.createElement("tr");
        var div1 = document.createElement("td");
        div1.className += "incoming_msg_img";
        div1.innerHTML = '<div> <img class="imgbor" src="' + imgUrl + '" alt="sunil"> </div>';
        var div2 = document.createElement("td");
        div2.className += "received_msg";
        var div3 = document.createElement("div");
        div3.className += "received_withd_msg";
        div3.innerHTML = '<p>' + message + '</p>' + '<span class="time_date">' + dateTime + '</span>';
        div2.appendChild(div3);
        div0.appendChild(div1);
        div0.appendChild(div2);
        document.getElementById("messagesList").appendChild(div0);
     
      
    }
    else {
        var div0 = document.createElement("tr");
        var div2 = document.createElement("td");
        div2.className += "outgoing_msg";
        var div3 = document.createElement("div");
        div3.className += "sent_msg";
        div3.innerHTML = '<p>' + message + '</p>' + '<span class="time_date">' + dateTime + '</span>';
        div2.appendChild(div3);
        /*div0.appendChild(div1);*/
        div0.appendChild(div2);
        document.getElementById("messagesList").appendChild(div0);
        
    }
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
