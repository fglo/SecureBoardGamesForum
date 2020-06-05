
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.on("NewMessages", (message) => {
    console.log("NewMessages");
    GetLastMessages();
});

connection
    .start()
    .then(() => {
        console.log('Connection started!');
    })
    .catch(err => console.log('Error while establishing connection'));