$(document).on('click', '.bttnerror',
    function () {
        $('.gackomodal').addClass('hide');
        setTimeout(() => { $('.modal-overlay').hide(); }, 600);
        if ($('.gackomodal').hasClass("reload")) {
            $('#successmodal').removeClass("reload");
            location.reload(true);
        }
    });

$(document).on('click', '.bttnsuccess',
    function () {
        $('.gackomodal').addClass('hide');
        setTimeout(() => { $('.modal-overlay').hide(); }, 600);
        if ($('.gackomodal').hasClass("reload")) {
            $('#successmodal').removeClass("reload");
            location.reload(true);
        }
    });

function showErrorModal(data, reload) {
    let message = "";
    console.log(data);
    if (data.responseJSON)
        message = data.responseJSON.message;
    else if (data.message)
        message = data.message;
    else
        message = data.value;

    $('#errormodal #errormodal_message').html(message);
    $('.modal-overlay').show();
    $('#errormodal').removeClass("hide");
    $('#errormodal').removeAttr("hidden");

    if (reload === true) {
        $('.gackomodal').addClass('reload');
    }
}

function showErrorMessage(message) {
    if (message !== "") {
        $('#errormodal #errormodal_message').html(message);
        $('.modal-overlay').show();
        $('#errormodal').removeClass("hide");
        $('#errormodal').removeAttr("hidden");
    }
}

function showSuccessModal(data, reload) {
    let message = "";
    if (data.responseJSON)
        message = data.responseJSON.message;
    else if (data.message)
        message = data.message;
    else
        message = data.value;

    $('#successmodal #successmodal_message').html(message);
    $('.modal-overlay').show();
    $('#successmodal').removeClass("hide");
    $('#successmodal').removeAttr("hidden");

    if (reload === true) {
        $('.gackomodal').addClass('reload');
    }
}