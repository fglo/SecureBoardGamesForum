var sanitizeHTML = function (str) {
    var temp = document.createElement('div');
    temp.textContent = str;
    return temp.innerHTML;
};

function GetLastMessages() {
    $.get("/Chat/GetLastMessages")
        .done(function (data, textStatus, jqXHR) {
            floodTheChat(data);
        })
        .fail(function (data, textStatus, jqXHR) {
            console.log(data);
        });
}
GetLastMessages();

function floodTheChat(messages) {
    console.log(messages);
    messages.forEach(message => {
        let chatbody = $('div#chatbody');
        let lastMessage = chatbody.children().last();
        lastMessage.after(`<div style="margin-top: 8px; margin-bottom: 8px;" class="guest uk-grid-small uk-flex-middle uk-flex-left" uk-grid>
                                <div class="uk-width-auto">
                                    <img class="uk-border-circle" width="32" height="32" src="https://randomuser.me/api/portraits/men/97.jpg">
                                </div>
                                <div class="uk-width-auto">
                                    <div style="padding: 8px;" class="uk-card uk-card-body uk-card-small uk-card-default uk-border-rounded">
                                        <p class="uk-margin-remove">${message.content}</p>
                                    </div>
                                </div>
                            </div>`);
        if (lastMessage.attr('id') === "firstmessage") {
            $('div#firstmessage').remove();
        }
    });
}

$('input#chatinput').keypress(function (event) {
    let message = sanitizeHTML(this.value);
    if (message && message !== "" && event.keyCode == 13) {
        sendMessage(message);
        let chatbody = $('div#chatbody');
        let lastMessage = chatbody.children().last();
        lastMessage.after(`<div style="margin-top: 8px; margin-bottom: 8px;" class="me uk-grid-small uk-flex-middle uk-flex-right uk-text-right" uk-grid>
                                <div class="chat-message uk-width-auto">
                                    <div style="padding: 8px;" class="uk-card uk-card-body uk-card-small uk-card-primary uk-border-rounded uk-text-break">
                                        <p class="uk-margin-remove uk-text-small">${message}</p>
                                    </div>
                                </div>
                                <div class="uk-width-auto">
                                    <img class="uk-border-circle" width="32" height="32" src="https://randomuser.me/api/portraits/men/32.jpg">
                                </div>
                        </div>`);
        if (lastMessage.attr('id') === "firstmessage") {
            $('div#firstmessage').remove();
        }

        $(this).val("");
        chatbody[0].scrollTop = chatbody[0].scrollHeight;
    }
});


async function sendMessage(message) {
    // Default options are marked with *
    const response = await fetch("/Chat/SendMessage", {
        method: 'POST', // *GET, POST, PUT, DELETE, etc.
        mode: 'cors', // no-cors, *cors, same-origin
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, *same-origin, omit
        headers: {
            'Content-Type': 'application/json'
            // 'Content-Type': 'application/x-www-form-urlencoded',
        },
        redirect: 'follow', // manual, *follow, error
        referrerPolicy: 'no-referrer', // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
        body: JSON.stringify(message) // body data type must match "Content-Type" header
    });
    return response; // parses JSON response into native JavaScript objects
}