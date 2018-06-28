$(function () {

    //*********VARIABLES INICIALES*********//
    var _fechaActual,
        fechaTrabajo;
    //********* ACCIONES INICIALES*********//
    $("#dtpFecMuestra").datepicker({
        showOn: "button",
        buttonImage: "/Content/medios/calendar.png",
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd',
        yearRange: '-18: +0',
        maxDate: '+0m +0d'
    });

    //VALIDAR FONMULARIOS
    $("#formMuestras").validate({
        rules: {
            inputPresion: {
                required: true,
                validarDecimal: true
            },
            inputLizq: {
                required: true,
                validarDecimal: true
            },
            inputCent: {
                required: true,
                validarDecimal: true
            },
            inputDere: {
                required: true,
                validarDecimal: true
            },
            datePick: {
                required: true,
                validarFecha: true
            }
        },
        messages: {
            inputPresion: {
                required: "Presión Obligatoria",
                number: "Solo números!"
            },
            inputLizq: {
                required: "Obligatorio!",
                number: "Solo números!"
            },
            inputCent: {
                required: "Obligatorio!",
                number: "Solo números!"
            },
            inputDere: {
                required: "Obligatorio!",
                number: "Solo números!"
            },
            datePick: {
                required: "Obligatorio!",
            }
        }
    });
    //VALIDAR DECIMALES (CARGA PROF LLANTAS)
    jQuery.validator.addMethod("validarDecimal", function (value, element) {
        return this.optional(element) || /^(\d{1,2}\.\d{1})?$/.test(value); //#####,##
    }, "Formato incorrectos!");
    jQuery.validator.addMethod("validarFecha", function (value, element) {
        return this.optional(element) || /(^\d{2}[\/]\d{2}[\/]\d{4}$)/.test(value);
    }, "Formato incorrecto!");

    $("#formMuestras").on("submit", function (event) {
        event.preventDefault();
        $("#formMuestras").validate();
        var estado = $("#formMuestras").valid();
        fechaTrabajo = $("#datepicker").val();

        if (estado != false) {
            //REALIZAR ACCIONES CON LOS DATOS RECOGIDOS
            /*segun paquete:
            PROCEDURE PDB_MONTARLLANTA(par_vehiculo_e CHAR, par_llanta_e VARCHAR, par_grupo_e CHAR,
                par_profi_e NUMBER, par_profc_e NUMBER, par_profd_e NUMBER, par_posicion_e NUMBER,
                par_kilomi_e NUMBER, par_fechai_e DATE, par_presion_e NUMBER, par_retorno_s OUT VARCHAR)
             */


            var par_vehiculo_e = $("#idPlacaMonta").text(),
                valLlanta = $("#idLlantaMonta").text(),
                par_llanta_e = valLlanta.split("-")[0],
                par_grupo_e = valLlanta.split("-")[1],
                par_profi_e = $("#inputLizq").val(),
                par_profc_e = $("#inputCent").val(),
                par_profd_e = $("#inputDere").val(),
                par_posicion_e = cajaNueva["0"].parentElement.id.split("eje")[1],
                par_sentido = $('#' + par_llanta_e + ' .imgSentido').attr('tag'),
                par_kilomi_e = $("#idKMMed").text(),
                par_fechai_e = $("#dtpFecMuestra").val(),
                par_presion_e = $("#inputPresion").val();

            var llantaDetalle = {
                par_vehiculo_e: par_vehiculo_e,
                par_llanta_e: par_llanta_e,
                par_grupo_e: par_grupo_e,
                par_profi_e: par_profi_e,
                par_profc_e: par_profc_e,
                par_profd_e: par_profd_e,
                par_posicion_e: par_posicion_e,
                par_sentido: par_sentido,
                par_kilomi_e: par_kilomi_e,
                par_fechai_e: par_fechai_e,
                par_presion_e: par_presion_e
            };

            if (transaccion == 1) { //1:muestreo;2:montar
                console.log("Muestreos:");
                arrayMuestras.push(llantaDetalle);
                console.log(arrayMuestras);

            } else if (transaccion == 2) {
                var $nitem = $("#" + par_llanta_e);
                if (estadomuestrasDesmonta == 1) { //1:solo monta; 2:incluye desmonte
                    console.log("Solo fue un montar");
                    arrayMonta.push(llantaDetalle);
                    $nitem.detach()
                        .css({
                            left: "0",
                            top: "0"
                        });
                    cajaNueva.append($nitem);
                    $($nitem["0"]).attr("tag", "import");
                    $("#btnCancelremove").attr("disabled", "true");
                    console.log("Llantas Montadas:");
                    console.log(arrayMonta);
                } else if (estadomuestrasDesmonta == 2) {
                    console.log("Monte con desmonte");
                    arrayMonta.push(llantaDetalle);
                    console.log("Llantas Montadas:");
                    console.log(arrayMonta);
                    remover(cajaRemove);
                    /* $nitem.css({
                        left: "0",
                        top: "0"
                    }); */
                }
            }
            $("#modMuestraMonta").modal("hide");

        }
    });

    //Evento que le permite al usuario deshacer un desmonte (cuando sualta una llanta en la caneca)
    $("#modMuestraMonta").on("click", "#btnCancelAdd", function (event) {
        event.preventDefault();
        //cancelar la accion del modal

        if (transaccion == 1) { //1:muestreo;2:montar
            $("#modMuestraMonta").html("");
            $("#modMuestraMonta").modal("hide");

        } else if (transaccion == 2) {
            if (estadomuestrasDesmonta == 1) { //1:solo monta; 2:incluye desmonte
                $("#nuevallanta").append($(llNueva));
                console.log($(llNueva));
                $(llNueva).css({
                    left: "0",
                    top: "0"
                });
            } else if (estadomuestrasDesmonta == 2) {
                $("#nuevallanta").append($(llNueva));
                console.log($(llNueva));
                $(llNueva).css({
                    left: "0",
                    top: "0"
                });


                var valLlanta = $("#idLlantaMonta").text(),
                    par_llanta_e = valLlanta.split("-")[0];
                //Se devuelve a donde estaba
                var llanta = $("#" + par_llanta_e);
                llanta.css({
                    left: "0",
                    top: "0"
                });
            }
            $("#modMuestraMonta").html("");
            $("#modMuestraMonta").modal("hide");
        }


        /* if (estadomuestras != 1 && llNueva.length > 0) {
            $("#nuevallanta").append(llNueva);
            llNueva.css({
                left: "0",
                top: "0"
            });
        } else {
            var valLlanta = $("#idLlantaMonta").text(),
                par_llanta_e = valLlanta.split("-")[0];
            //Se devuelve a donde estaba
            var llanta = $("#" + par_llanta_e);
            llanta.css({
                left: "0",
                top: "0"
            });
        } */
        $("#btnDesmonta").attr("disabled", "true")
        $("#modMuestraMonta").html("");
        $("#modMuestraMonta").modal("hide");


    });
});