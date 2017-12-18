// global variable for storing username of comment
var username;

function rateCard(cardId) {

    $.post('/Cards/RateCard', { id: cardId })
        .done(function (rating) {

            var totalRating = parseInt($('#likes').text());

            switch (rating) {
                case 0:
                    $('#like-card').css('background-color', 'white');
                    $('#likes').text(totalRating - 1);
                    break;
                case 1:
                    $('#like-card').css('background-color', 'green');
                    $('#likes').text(totalRating + 1);
                    break;
            }
        });
}

function rateComment(commentId) {

    $.post('/Cards/RateComment', { commentId: commentId })
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
$('.comments-vm-container').on('click', '.edit-comment-link', function (e) {
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
$('.comments-vm-container').on('submit', '#edit-form', function (e) {
    e.preventDefault();

    var comment = document.forms['edit-form']['edit'].value;
    var commentId = document.forms['edit-form']['edit'].id;
    $.post('/Cards/EditComment', { commentId: commentId, comment: comment })
        .done(function () {
            $('#edit-form').replaceWith(
                $('<p id="container-' + commentId + '" class="comment" />')
                    .append('<strong id="username">' + username + '</strong> : <span id="text-' + commentId + '">' + comment + '</span>')
            );
        });
});

// deletes comment from database
// if successful removes content from client
$('.comments-vm-container').on('click', '.delete-comment-link', function (e) {
    e.preventDefault();

    var commentId = this.id;
    $.post('/Cards/DeleteComment', { commentId: commentId })
        .done(function () {
            $('#container-' + commentId + '-container').remove();
        });
});

// report comment
$('.comments-vm-container').on('click', '.report-comment-link', function (e) {
    e.preventDefault();

    var commentId = this.id;
    $.post('/Cards/ReportComment', { commentId: commentId })
        .done(function () {
            alert('Your report has been filed and the comment will be reviewed.')
        });
});

$(document).ready(function () {
    $('.comments-vm-container').on('submit', '#comment-form', function (e) {
        e.preventDefault();

        var comment = document.forms['comment-form']['comment-text'].value;
        document.forms['comment-form']['comment-text'].value = '';
        var cardId = document.getElementsByClassName('comments-vm-container')[0].id;
        
        $.post('/Cards/PostComment', { id: cardId, comment: comment }, function () {
            $.get('/Cards/LoadComments', { id: cardId }, function (comments) {
                $('.comments-container').replaceWith(comments);
            });
        });
    });
});
