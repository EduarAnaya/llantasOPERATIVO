(function ($) {

    $(document).idleTimer(1 * 60000);
    $(document).on("idle.idleTimer", function () {
        alert("1 minuto de inactividad!")
        $(location).attr('href', "/llantas/Index");
        //console.log("pagina refrescada");
    });
})(jQuery);