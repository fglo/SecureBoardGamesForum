var gettingLastMessages = false;

$('a.fb-button').click(function () {
    let chatbody = $('div#chatbody');
    setTimeout(() => {
        chatbody[0].scrollTop = chatbody[0].scrollHeight;
    }, 500);
});

var sanitizeHTML = function (str) {
    var temp = document.createElement('div');
    temp.textContent = str;
    return temp.innerHTML;
};

function GetLastMessages() {
    gettingLastMessages = true;
    $.get("/Chat/GetLastMessages")
        .done(function (data, textStatus, jqXHR) {
            floodTheChat(data);
            gettingLastMessages = false;
        })
        .fail(function (data, textStatus, jqXHR) {
            console.log(data);
            gettingLastMessages = false;
        });
}

function GetPreviousPage() {
    $.get("/Chat/GetPreviousPage")
        .done(function (data, textStatus, jqXHR) {
            addPreviousMessages(data);
        })
        .fail(function (data, textStatus, jqXHR) {
            console.log(data);
        });
}

function GetMessages() {
    $.get("/Chat/GetMessages")
        .done(function (data, textStatus, jqXHR) {
            floodTheChat(data);
        })
        .fail(function (data, textStatus, jqXHR) {
            console.log(data);
        });
} GetMessages();

function floodTheChat(messages) {
    console.log(messages);
    if (messages.length > 0 && $('div#firstmessage')) {
        $('div#firstmessage').remove();
    }
    let anchor = $('div#chatbody').find('#anchor');
    messages.forEach(message => {
        if (message.isMyMessage) {
            anchor.before(getMyMessage(sanitizeHTML(message.content)));
        } else {
            anchor.before(getOutsideMessage(sanitizeHTML(message.content), message.author.firstName, message.author.lastName));
        }
    });
}

function addPreviousMessages(messages) {
    if (messages.length > 0 && $('div#firstmessage')) {
        $('div#firstmessage').remove();
    }

    let top = $('div#chatbody div:nth-child(2)');
    messages.forEach(message => {
        let chattop = $('div#chatbody').find('#chattop');
        if (message.isMyMessage) {
            chattop.after(getMyMessage(sanitizeHTML(message.content)));
        } else {
            chattop.after(getOutsideMessage(sanitizeHTML(message.content), message.author.firstName, message.author.lastName));
        }
        UIkit.scroll('div#chatbody').scrollTo(top);
    });
}

$('input#chatinput').keypress(function (event) {
    let message = sanitizeHTML(this.value);
    if (message && message !== "" && event.keyCode == 13) {
        sendMessage(message);
        let chatbody = $('div#chatbody');
        let anchor = chatbody.find('#anchor');

        anchor.before(getMyMessage(message));

        $(this).val("");
        chatbody[0].scrollTop = chatbody[0].scrollHeight;

        if ($('div#firstmessage')) {
            $('div#firstmessage').remove();
        }
    }
});

$('div#chatbody').scroll(function (ev) {
    if (!gettingLastMessages && this.scrollTop === 0) {
        GetPreviousPage();
    }
});

function getOutsideMessage(text, name, surname) {
    return `<div style="margin-top: 8px; margin-bottom: 8px;" class="guest uk-grid-small uk-flex-middle uk-flex-left" uk-grid>
                                <div class="uk-width-auto">
                                    <div class="initials_border">
                                      <div class="name-container">
                                        <div class="name" uk-tooltip="title: ${name} ${surname}; pos: bottom-left">
                                          ${name.charAt(0).toUpperCase()}${surname.charAt(0).toUpperCase()}
                                        </div>
                                      </div>
                                    </div>
                                </div>
                                <div class="chat-message uk-width-auto">
                                    <div style="padding: 8px;" class="uk-card uk-card-body uk-card-small uk-card-default uk-border-rounded uk-text-break">
                                        <p class="uk-margin-remove uk-text-normal uk-text-small">${text}</p>
                                    </div>
                                </div>
                            </div>`;
}


function getMyMessage(text) {
    return `<div style="margin-top: 8px; margin-bottom: 8px;" class="me uk-grid-small uk-flex-middle uk-flex-right uk-text-right" uk-grid>
                                <div class="chat-message uk-width-auto">
                                    <div style="padding: 8px;" class="uk-card uk-card-body uk-card-small uk-card-primary uk-border-rounded uk-text-break">
                                        <p class="uk-margin-remove uk-text-normal uk-text-small" style="color: white;">${text}</p>
                                    </div>
                                </div>
                        </div>`;
}

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