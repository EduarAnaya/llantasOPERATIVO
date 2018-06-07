$(function () {
  $("._ContAccion").idleTimer(5 * 60000);
  $("._ContAccion").on("idle.idleTimer", function () {
    var alert = $(
      '<div class="alert alert-dismissible fade show alertTimeOut" role="alert">' +
      '<img src="/Content/medios/timerSucces.png" style="width:10%; margin: 5px;">' +
      "<strong>Tiempo de inactividad alcanzado.!</strong>" +
      '<button type="button" class="close" data-dismiss="alert" aria-label="Close">' +
      '<span aria-hidden="true">&times;</span>' +
      "</button>" +
      "</div>"
    );
    $(".Contapp *").prop("disabled", true);
    $(".Contapp").prepend(alert);
    $("._ContAccion").idleTimer("destroy");
    $(".close").on("click", function () {
      $(location).attr("href", "/");
    });
  });
});