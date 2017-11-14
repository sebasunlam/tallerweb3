$(document).ready(function () {
    $("#ddPageSize").change(function () {
        window.location.href = $(this).attr('url') + $(this).val();
    });

    $(".delete").click(function () {
        var id = $(this).attr("idReserva");
        $.post("http://localhost:60569/api/reserva/delete/" + id).then(function () {
            location.reload();
        });
    });
})