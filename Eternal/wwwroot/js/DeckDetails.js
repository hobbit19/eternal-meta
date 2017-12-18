// global variable for storing username of comment
var username;

function rateDeck(deckId) {

    $.post('/Decks/RateDeck', { id: deckId })
        .done(function (rating) {

            var totalRating = parseInt($('#deck-likes').text());

            switch (rating) {
                case 0:
                    $('#like').css('background-color', 'white');
                    $('#deck-likes').text(totalRating - 1);
                    break;
                case 1:
                    $('#like').css('background-color', 'green');
                    $('#deck-likes').text(totalRating + 1);
                    break;
            }

        });
}

function rateComment(commentId) {

    $.post('/Decks/RateComment', { commentId: commentId })
        .done(function (rating) {

            var totalRating = parseInt($('#likes-' + commentId).text());

            switch (rating) {
                case 0:
                    $('#like-comment-' + commentId).css('background-color', 'white');
                    $('#likes-' + commentId).text(totalRating - 1);
                    break;
                case 1:
                    $('#like-comment-' + commentId).css('background-color', 'green');
                    $('#likes-' + commentId).text(totalRating + 1);
                    break;
            }
        });
}

function redirectToLogin() {
    setTimeout(function () {
        window.location.replace('/Users/Login');
    }, 1250);
}

// replaces content of comment with form for editing
$('.comments-section').on('click', '.edit-comment-link', function (e) {
    e.preventDefault();
    var commentId = this.id;
    var comment = $('#text-' + this.id).text();
    username = $('#username').text();
    $('#container-' + commentId)
        .replaceWith(
        $('<form id="edit-form" name="edit-form" />')
            .append('<input class="form-control" name="edit" id="' + commentId + '" value="' + comment + '" />')
            .append('<input type="Submit" hidden />')
        );
});

// edit form handler
// posts updated comment to server
// if successful updates content on client
$('.comments-section').on('submit', '#edit-form', function (e) {
    e.preventDefault();
    var comment = document.forms['edit-form']['edit'].value;
    var commentId = document.forms['edit-form']['edit'].id;
    $.post('/Decks/EditComment', { commentId: commentId, comment: comment })
        .done(function () {
            $('#edit-form').replaceWith(
                $('<p id="container-' + commentId + '" class="comment" />')
                    .append('<strong id="username">' + username + '</strong> : <span id="text-' + commentId + '">' + comment + '</span>')
            );
        });
});

// deletes comment from database
// if successful removes content from client
$('.comments-section').on('click', '.delete-comment-link', function (e) {
    e.preventDefault();
    var commentId = this.id;
    $.post('/Decks/DeleteComment', { commentId: commentId })
        .done(function () {
            $('#container-' + commentId + '-container').remove();
        });
});

// report comment
$('.comments-section').on('click', '.report-comment-link', function (e) {
    e.preventDefault();
    var commentId = this.id;
    $.post('/Decks/ReportComment', { commentId: commentId })
        .done(function () {
            alert('Your report has been filed and the comment will be reviewed.')
        });
});

// show card image when hoving over card link in the deck list
$('.decklist-card-link')
    .hover(function () {
        $('<div class="popup-card" />')
            .append('<img class="popup-card-image" src="../../images/cards/' + $(this).attr('id') + '.png" />')
            .css({
                'left': ($(this).offset().left - 275),
                'top': ($(this).offset().top - 10)
            })
            .appendTo('body');
    }, function () {
        $('.popup-card').remove();
    });


$(document).ready(function () {
    $('.comments-section').on('submit', '#comment-form', function (e) {
        e.preventDefault();
        var comment = document.forms['comment-form']['comment-text'].value;
        document.forms['comment-form']['comment-text'].value = '';
        var deckId = document.getElementsByClassName('comments-section')[0].id;
        $.post('/Decks/PostComment', { id: deckId, comment: comment })
            .done(function () {
                $.get('/Decks/LoadComments', { id: deckId }, function (comments) {
                    $('.comments-container').replaceWith(comments);
                });
            });
    });
});


