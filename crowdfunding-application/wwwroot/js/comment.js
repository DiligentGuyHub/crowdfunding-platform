"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/commentHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("RecieveComment", function (user, message) {
    document.getElementById("commentList").innerHTML += '<p class="list-group-item"><b>' + user + "</b><br/>" + message + "</p>"
    //var p = document.createElement("p");
    //p.className = "list-group-item";
    //document.getElementById("commentList").appendChild(p);
    //p.textContent = `${user}: ${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendComment", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});