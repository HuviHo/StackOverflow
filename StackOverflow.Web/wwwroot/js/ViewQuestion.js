$(() => {
    $("#like").on('click', function () {
        let QuestionId = $(this).data('question-id')

        $.post('/user/postLike', { QuestionId }, function (NumberOfLikes) {
            $("#number-of-likes").text(NumberOfLikes)
            $("#like").prop('disabled', true);
            $("#like").hide();
        });
    });

    $("#post-answer").on('click', function () {
        let Answer = {
            QuestionId: $(this).data('question-id'),
            Text: $("#text").val()
        }

        $.post('/user/postAnswer', { Answer: Answer }, function (Text) {
            $("#text").val("")
            $("#answers").append(
                `<div class="row">
                    <div class="col-md-6 well">
                       <h5>${Text}</h5>
                     </div>
                 </div>`
            );

        });
    });

});