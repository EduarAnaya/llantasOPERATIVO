/* $(function () {
    //validacion del formulario
    $("#modMuestraDesmonta").validate({
        rules: {
            inputPresionRmv: {
                required: true,
                validarDecimal: true,
                number: true
            },
            inputLizqRmv: {
                required: true,
                validarDecimal: true,
                number: true
            },
            inputCentRmv: {
                required: true,
                validarDecimal: true,
                number: true
            },
            inputDereRmv: {
                required: true,
                validarDecimal: true,
                number: true
            }
        },
        messages: {
            inputPresionRmv: {
                required: "Presión Obligatoria",
                number: "Solo números!"
            },
            inputLizqRmv: {
                required: "Obligatorio!",
                number: "Solo números!"
            },
            inputCentRmv: {
                required: "Obligatorio!",
                number: "Solo números!"
            },
            inputDereRmv: {
                required: "Obligatorio!",
                number: "Solo números!"
            }
        }
    });
    //VALIDAR DECIMALES (CARGA PROF LLANTAS)
    jQuery.validator.addMethod("validarDecimal", function (value, element) {
        return this.optional(element) || /(^\d{1,2}$|^\d{1,2}[.]\d{1,2}$)/.test(value);
    }, "Formato incorrecto!");

    $("#modMuestraDesmonta").on("submit", function (event) {
        event.preventDefault();
        $("#modMuestraDesmonta").validate();
        var estado = $("#modMuestraDesmonta").valid();

        if (estado != false) {
            //REALIZAR ACCIONES CON LOS DATOS RECOGIDOS
            $('#modMuestraDesmonta').modal('hide');
        }
    });


}) */