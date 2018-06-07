//Validacion de Formulario
$("#formSearchVehiculo").validate({
    rules: {
        placa: {
            required: true,
            validarplaca: true
        },
        km: {
            required: true,
            number: true,
            min: 1
        }
    },
    messages: {
        placa: {
            required: " Placa obligatoria!",
        },
        km: {
            required: " Km obligatorio!",
            number: " Solo números!",
            min: " Km mayor a 0!"
        }
    }
});

//**********VALIDAR ENTRADAS ************/
//VALIDAR PLACA (AAA###)
jQuery.validator.addMethod("validarplaca", function (value, element) {
    return this.optional(element) || /(^[a-zA-Z]{3}\d{3}$|^[a-zA-Z]{1}\d{5}$)/.test(value);
}, "Formato Incorrecto!");

$("#formSearchVehiculo").on("submit", function (event) {
    $("#formSearchVehiculo").validate();
    var estado = $("#formSearchVehiculo").valid();
    if (estado != true) { //detienen la ejecucion predeterminada en caso que que el formulario no sea valido
        event.preventDefault();
    }
});