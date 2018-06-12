$(function () {
    $("._ContAccion").idleTimer(5 * 60000);
    $("._ContAccion").on("idle.idleTimer", function () {
        var modal = $('<div class="modal fade" id="modTimeout" tabindex ="-1" role ="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false" style="display: block;z-index: 2001;"></div>');
        $('body').prepend(modal);
        $("#modTimeout").modal("show");
        var alert = $(
            '<div class="alert alert-dismissible fade show alertTimeOut" role="alert">' +
            '<img src="/Content/medios/timerSucces.png" style="width:10%; margin: 5px;">' +
            "<strong>Tiempo de inactividad alcanzado.!</strong>" +
            '<button type="button" class="close" data-dismiss="alert" aria-label="Close">' +
            '<span aria-hidden="true">&times;</span>' +
            "</button>" +
            "</div>"
        );
        $("#modTimeout").append(alert);
        $(".Contapp *").prop("disabled", true);
        //$(".Contapp").prepend(alert);
        $("._ContAccion").idleTimer("destroy");
        $(".close").on("click", function () {
            $(location).attr("href", "/");
        });
    });
});