function toggleCreateState() {
    $('#loadImage').toggle();
    $('#result').toggle();
    $('#submitBtn').toggle();
}

$(document).ready((function () {
    $('form').submit(function () {
        if ($(this).valid()) {
            toggleCreateState();
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    setTimeout(function () {
                        $('#result').html(result);
                        toggleCreateState();
                    }, 1000);
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