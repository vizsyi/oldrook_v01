// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


//import { signalR } from "../lib/signalr/dist/browser/signalr";

var chatterName = 'Visitor';
var dialogE1 = document.getElementById('chatDialog');

// Initialize the SignalR client
var connection = new signalR.HubConnectionBuilder()
    .withUrl('rookHub')
    .build();

connection.on('ReceiveMessage', renderMessage);

connection.onclose(function () {
    onDisconnected();
    console.log('Reconnecting in 5 seconds. . .');
    setTimeout(startConnection, 5000);
})

function startConnection() {
    connection.start()
        .then(onConnected)
        .catch(function (err) {
            console.error(err);
        });
}

function onDisconnected() {
    dialogE1.classList.add('disconnected');
}

function onConnected() {
    dialogE1.classList.remove('disconnected');

    var messageTextboxE1 = document.getElementById('messageTextbox');
    messageTextboxE1.focus();
}

function showChatDialog() {
    dialogE1.style.display = 'block';
}

function sendMessage(text) {
    if (text && text.length) {
        connection.invoke('SendMessage', chatterName, text);
    }
}

function ready() {
    setTimeout(showChatDialog, 750);

    var chatFormE1 = document.getElementById('chatForm');
    chatFormE1.addEventListener('submit', function (e) {
        e.preventDefault();

        var text = e.target[0].value;
        e.target[0].value = '';
        sendMessage(text);
    });

    var welcomePanelE1 = document.getElementById('chatWelcomePanel');
    welcomePanelE1.addEventListener('submit', function (e) {
        e.preventDefault();

        var name = e.target[0].value;
        if (name && name.length) {
            welcomePanelE1.style.display = 'none';
            chatterName = name;
            startConnection();
        }
    });
}

function renderMessage(name, time, message) {
    var nameSpan = document.createElement('span');
    nameSpan.className = 'name';
    nameSpan.textContent = name;

    var timeSpan = document.createElement('span');
    timeSpan.className = 'time';
    var friendlyTime = moment(time).format('H:mm');
    timeSpan.textContent = friendlyTime;

    var headerDiv = document.createElement('div');
    headerDiv.appendChild(nameSpan);
    headerDiv.appendChild(timeSpan);

    var messageDiv = document.createElement('div');
    messageDiv.className = 'message';
    messageDiv.textContent = message;

    var newItem = document.createElement('li');
    newItem.appendChild(headerDiv);
    newItem.appendChild(messageDiv);

    var chatHistoryE1 = document.getElementById('chatHistory');
    chatHistoryE1.appendChild(newItem);
    chatHistoryE1.scrollTop = chatHistoryE1.scrollHeight - chatHistoryE1.clientHeight;
}

document.addEventListener('DOMContentLoaded', ready);