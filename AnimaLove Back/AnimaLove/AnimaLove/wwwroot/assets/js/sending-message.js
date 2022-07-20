
function SendMessage() {
var mainDiv=document.querySelector(".messages-content");
var input= document.querySelector('input').value;
var messageBox=document.createElement("div");
var InnermessageBox=document.createElement("div");
var messageSender=document.createElement("p");
if (input!=="") {
    messageBox.classList.add("messages-content-inner");
InnermessageBox.classList.add("messages-content-inner-item");
messageSender.classList.add("message-sender-name-you");
messageSender.innerHTML="you";
var msgContext=document.createElement("p");
var MsgDate=document.createElement("p");
MsgDate.classList.add("date");
MsgDate.innerHTML=Date();
msgContext.classList.add("message-context");
msgContext.innerHTML=input;

InnermessageBox.appendChild(messageSender);
InnermessageBox.appendChild(msgContext);

InnermessageBox.appendChild(MsgDate);
messageBox.appendChild(InnermessageBox);
mainDiv.appendChild(messageBox)

var context=document.querySelector('.message-context').innerHTML;
context=input; 
var input=document.querySelector('input').value;
input="";
}
else {
    console.log("just it");
}

}

