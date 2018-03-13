(function ($) {

    $(document).idleTimer(5 * 60000);
    $(document).on("idle.idleTimer", function () {
        alert("5 minutos de inactividad!")
        $(location).attr('href', "/llantas/Index");
        //console.log("pagina refrescada");
    });
})(jQuery);