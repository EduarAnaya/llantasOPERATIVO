$(function () {


    $("#btn1").on("click", function () {
        alert("sirve1")
    })
    //validacion del formulario
    $("#formMuestrasDesmonta").validate({
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
            },
            selectCausa: {
                required: true
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
            },
            selectCausa: {
                required: "Se debe especificar la causa del desmonte!"
            }
        }
    });
    //VALIDAR DECIMALES (CARGA PROF LLANTAS)
    jQuery.validator.addMethod("validarDecimal", function (value, element) {
        return this.optional(element) || /(^\d{1,2}$|^\d{1,2}[.]\d{1,2}$)/.test(value);
    }, "Formato incorrecto!");
    $("#formMuestrasDesmonta").on("submit", function (event) {
        event.preventDefault();
        $("#modMuestraDesmonta").validate();
        var estado = $("#formMuestrasDesmonta").valid();
        if (estado != false) {
            //REALIZAR ACCIONES CON LOS DATOS RECOGIDOS
            $('#formMuestrasDesmonta').modal('hide');
        }
    });


})