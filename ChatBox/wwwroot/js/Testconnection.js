"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("btntestconnection").disabled = true;

connection.on("CheckStatus", function (userID) {
    $.ajax({
        url: "CreatXMLFile",
        data: { userid: $('#userID').val(), message: innerhtmlmsg },
        type: 'POST',
        sync: true


    });
});

connection.start().then(function () {
    document.getElementById("btntestconnection").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("btntestconnection").addEventListener("click", function (event) {
    var userID = document.getElementById("userID").value;
    connection.invoke("TestConnect", userID).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
