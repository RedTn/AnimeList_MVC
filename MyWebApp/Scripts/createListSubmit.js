function toggleCreateState() {
    $('#loadImage').toggle();
    $('#result').toggle();
    $('#submitBtn').toggle();
}

$(document).ready((function () {
    $('form').submit(function () {
        if ($(this).valid()) {
            toggleCreateState();
            var formData = new FormData($('#createForm')[0]);
            $.ajax({
                url: this.action,
                type: this.method,
                data: formData,
                contentType: false,
                processData: false,
                headers: {
                    'RequestVerificationToken': '@TokenHeaderValue()'
                },
                success: function (data) {
                    setTimeout(function () {
                        toggleCreateState();
                        $('#result').html(data);
                    }, 500);
                }
            });
            //TODO: Find better selector than #SeriesType
            $("form :input:not(:submit):not(#SeriesType)").each(function () {
                $(this).val("");
            });
            $('#SeriesType').val(0);
        }
        else {
            $('#result').html("Error, invalid inputs");
        }
        return false;
    });
}));

