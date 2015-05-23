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
            //TODO: Find out how to NOT select the enum dropdown
            $("form :input:not(:submit)").each(function () {
                $(this).val("");
            });
        }
        else {
            $('#result').html("Error, invalid inputs");
        }
        return false;
    });
}));