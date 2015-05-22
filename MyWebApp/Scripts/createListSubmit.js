$(function () {
    $('form').submit(function () {
        $('#result').toggle();
        $('#loadImage').toggle();
        $('#submitBtn').toggle();
        if ($(this).valid()) {
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    setTimeout(function () {
                        $('#result').html(result);
                        $('#loadImage').toggle();
                        $('#result').toggle();
                        $('#submitBtn').toggle();
                    }, 1000);
                }
            });
        }
        return false;
    });
});