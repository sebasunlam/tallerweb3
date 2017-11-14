$(document).ready(function () {

    $(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });

    $("#ddPageSize").change(function () {
        window.location.href = $(this).attr('url') + $(this).val();
    });

    $(".fa-star").click(function () {
        changeDestacado($(this));
    });

    $(".fa-star-o").click(function () {
        changeDestacado($(this));
    });

    $(".btn-outline-danger").click(function () {
        waitingDialog.show();
        window.location.href = $(this).attr('url') + '/' + $(this).attr('paqueteId');
    });

    $(".btn-outline-primary").click(function () {
        
        waitingDialog.show();
        window.location.href = $(this).attr('url') + '/' + $(this).attr('paqueteId');
    });

    function changeDestacado(star) {
        var current = star.attr('class');
        star.removeClass();
        star.addClass('fa fa-spinner fa-pulse');

        $.post(star.attr('url') + '/' + star.attr('paqueteId'),
            function (data) {
                star.removeClass();
                if (data) {
                    star.addClass('text-warning fa fa-star');
                } else {
                    star.addClass('text-warning fa fa-star-o');
                }
            }).fail(function () {
                star.removeClass();
                star.addClass(current);
            });
    }


})