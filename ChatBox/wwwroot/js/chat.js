"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("btnSendMsg").disabled = true;

connection.on("ReceiveMessage", function (user, message, ckUser, imgUrl) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    if (ckUser == 1) {
        var div1 = document.createElement("div");
        div1.className += "incoming_msg_img";
        div1.innerHTML = '<div class="incoming_msg_img"> <img src="' + imgUrl + '" alt="sunil"> </div>';
        var div2 = document.createElement("div");
        div2.className += "received_msg";
        var div3 = document.createElement("div");
        div3.className += "received_withd_msg";
        div3.innerHTML = '<p>' + msg + '</p>' + '<span class="time_date">' + Date.now() + '</span>';
        div2.appendChild(div3);
        document.getElementById("messagesList").appendChild(div1 + div2);
    }
    else {
        //var div1 = document.createElement("div");
        //div1.className += "incoming_msg_img";
        //div1.innerHTML = '<div class="incoming_msg_img"> <img src="' + imgUrl + '" alt="sunil"> </div>';
        var div2 = document.createElement("div");
        div2.className += "received_msg";
        var div3 = document.createElement("div");
        div3.className += "received_withd_msg";
        div3.innerHTML = '<p>' + msg + '</p>' + '<span class="time_date">' + Date.now() + '</span>';
        div2.appendChild(div3);
        document.getElementById("messagesList").appendChild(div1 + div2);
    }

});

connection.start().then(function () {
    document.getElementById("btnSendMsg").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("btnSendMsg").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});