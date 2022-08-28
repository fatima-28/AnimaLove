"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
console.log(connection);
////Disable send button until connection is established
//var myDiv = document.querrySelector('.chat-message-right');
//console.log(myDiv)

//connection.on("ReceiveMessage", function (user, message) {
//var div = document.createElement("div");
//div.classList.add("chat-message-right");
//div.classList.add("mb-4");
//document.getElementById("messagesList").appendChild(div);

/*div.textContent = `${user} says ${message}`;*/
//    // We can assign user-supplied strings to an element's textContent because it
//    // is not interpreted as markup. If you're assigning in any other way, you 
//    // should be aware of possible script injection concerns.
//    li.textContent = `${user} says ${message}`;
//});

//connection.start().then(function () {
//    document.getElementById("sendButton").disabled = false;
//}).catch(function (err) {
//    return console.error(err.toString());
//});


//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMessage", user, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});
var messagelist = document.querySelector("#messagesList");
$("#sendButton").click(function () {
    var message = document.querySelector('#messageInput').value;
    console.log(message);
    var div = document.createElement("div");
    div.classList.add("chat-message-right");
    div.classList.add("mb-4");
    var firstDiv = document.createElement("div");
    var img = document.createElement('img');
    img.src =
        'http://localhost:12168/assets/img/05eb2ecb-afbd-48b7-8d29-0ea0ccbc1833_image-girllll.jpg';
    img.classList.add("rounded-circle");
    img.classList.add("mr-1");
    img.style.width = "40px";
    img.style.height = "40px";
    var TimeDiv = document.createElement("div");
    TimeDiv.classList.add("text-muted");
    TimeDiv.classList.add("small");
    TimeDiv.classList.add("text-nowrap");
    TimeDiv.classList.add("mt-2");
    const now = new Date();
    TimeDiv.innerText = now.getHours() + ':' + now.getMinutes();

    var SecondDiv = document.createElement("div");

    SecondDiv.classList.add("flex-shrink-1");
    SecondDiv.classList.add("bg-light");
    SecondDiv.classList.add("rounded");
    SecondDiv.classList.add("py-2");
    SecondDiv.classList.add("px-3");
    SecondDiv.classList.add("mr-3");
    var You = document.createElement("div");
    You.classList.add("font-weight-bold");
    You.classList.add("mb-1");
    You.innerText = "You";
    var textp = document.createElement("p");
    textp.innerText = message;
    SecondDiv.appendChild(You);
    SecondDiv.appendChild(textp);
    firstDiv.appendChild(img);
    firstDiv.appendChild(TimeDiv);
    div.appendChild(firstDiv);
    div.appendChild(SecondDiv);

    messagelist.appendChild(div);
  /*  message.innerText = "";*/
});

