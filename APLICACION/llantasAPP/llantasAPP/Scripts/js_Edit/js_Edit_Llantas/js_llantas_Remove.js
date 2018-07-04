if (llNueva.length > 0) {
    $("#btnCancelremove").attr("disabled", "true");
}

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
        },
        areaObservaciones: {
            required: true
        },
        destinoLlanta: {
            required: true
        },
        archivosSoportes: {
            required: true
        },
        archivosSoportesRepara: {
            required: true
        },
        radioOpciones: {
            required: true
        },
        archivoDesmonte: {
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
        },
        destinoLlanta: {
            required: "Seleccione el inventario de destino!"
        },
        areaObservaciones: {
            required: "Describe la causa del desmonte!"
        },
        archivoDesmonte: {
            required: "El adjunto es obligatorio!"
        }
    }
});
//VALIDAR DECIMALES (CARGA PROF LLANTAS)
jQuery.validator.addMethod(
    "validarDecimal",
    function (value, element) {
        return (
            this.optional(element) || /(^\d{1,2}$|^\d{1,2}[.]\d{1,2}$)/.test(value)
        );
    },
    "Formato incorrecto!"
);
//Envio del formulario
$("#formMuestrasDesmonta").on("submit", function (event) {
    event.preventDefault();
    $("#formMuestrasDesmonta").validate();
    var estado = $("#formMuestrasDesmonta").valid();
    if (estado != false) {
        /*Información que requiere el paquete de la base de datos (el paquete no se ejecuta desde aquí)
    PROCEDURE PDB_DESMONTARLLANTA(par_vehiculo_e CHAR, par_llanta_e VARCHAR, par_grupo_e CHAR,
   par_observacion_e NUMBER,par_kilomi_e NUMBER, par_fechai_e DATE,  par_retorno_s OUT VARCHAR);
    */

        //validar que la llanta ya no este en la caneca

        var s;
        var par_vehiculo_e = $("#idPlacaRmv").text(),
            valLlanta = $("#idLlantaRmv").text(),
            par_llanta_e = valLlanta.split("-")[0],
            par_grupo_e = valLlanta.split("-")[1],
            par_observacion_e = $("#selectCausa")[0].value,
            par_destInven = $('input:radio[name=destinoLlanta]:checked').val(),
            s = $('#' + par_llanta_e + '')[0],
            par_nroItem = s.children[1].children["0"].children["0"].children[9].innerText,
            par_kilomi_e = $("#idKMMed").text(),
            par_fechai_e = $("#idFechaMed").text(),
            par_posicion_e = cajaRemove.offsetParent.id.split("eje")[1];

        var llantaRemove = {
            par_vehiculo_e: par_vehiculo_e,
            par_llanta_e: par_llanta_e,
            par_grupo_e: par_grupo_e,
            par_observacion_e: par_observacion_e,
            par_nroItem: par_nroItem,
            par_destInven: par_destInven,
            par_kilomi_e: par_kilomi_e,
            par_fechai_e: par_fechai_e,
            par_posicion_e: par_posicion_e
        };

        arrayDesmonta.push(llantaRemove);

        var $nitem = $("#" + par_llanta_e)
            .detach()
            .css({
                left: "0",
                top: "0"
            });
        $caneca.append($nitem);
        $($nitem["0"]).attr("tag", "Pl_removed");
        console.log("Llantas desmontadas:");
        console.log(arrayDesmonta);

        /* var configuraPost = {
      "crossDomain": true,
      "url": "http://localhost:54919/llantas/removerllantasPost?par_vehiculo=SQL284&par_llanta=456626&par_grupo=000&par_profi=10&par_profc=15&par_profd=30&par_posicion=2&par_kilomi=42000&par_presion=13",
      "method": "POST",
      "headers": {
        "cache-control": "no-cache",
        "postman-token": "682ce405-6cde-4282-1fcf-c8ca58c4713b"
      }
    }
    var dataPost={
      par_vehiculo:_placa
    }

    $.post(configuraPost).done(function (response) {
      if (date == "Ok") { */
        $("#modMuestraDesmonta").modal("hide");
        /*      }
           }); */
    }
});
//Evento que le permite al usuario deshacer un desmonte (cuando sualta una llanta en la caneca)
$("#modMuestraDesmonta").on("click", "#btnCancelremove", function (event) {
    event.preventDefault();
    //cancelar la accion de la montura de una llanta nueva 
    if (llNueva.length > 0) {
        llNueva.css({
            left: "0",
            top: "0"
        });
    } else {
        var valLlanta = $("#idLlantaRmv").text(),
            par_llanta_e = valLlanta.split("-")[0];
        //Se devuelve a donde estaba
        var llanta = $("#" + par_llanta_e);
        llanta.css({
            left: "0",
            top: "0"
        });
    }

    $("#modMuestraDesmonta").html("");
    $("#modMuestraDesmonta").modal("hide");


});
/**** ESTAS ACCIONES ESTÁN PENDIENTES PARA IMPLEMENTAS DEBIDO A LA FALTA DE DESARROLLO ****
* descomentarear para habilitar

$("#selectCausa").change(function() {
var opc = $(this).val();
if (opc != 3 || opc != 4) {
  $("#grupo_quemadas").addClass("visible");
  $("#grupo_reusadas").addClass("visible");
}
if (opc == 3) {
  $("#grupo_quemadas").removeClass("visible");
  $("#grupoFilesDesmonte").removeClass("visible");
} else if (opc == 4) {
  $("#grupo_reusadas").removeClass("visible");
}
});

$("#checkTemperatura").change(function() {
var estado = $(this).prop("checked");
if (estado) {
  $("#inutTemperatra").removeAttr("disabled");
} else {
  $("#inutTemperatra").attr("disabled", "");
}
});

$("#checkPresion").change(function() {
var estado = $(this).prop("checked");
if (estado) {
  $("#inutPresion").removeAttr("disabled");
} else {
  $("#inutPresion").attr("disabled", "");
}
});
$("input[name$='radioOpciones']").change(function() {
var opcion = $(this).val();
if (opcion != 1) {
  $("#grupoFilesDesmonte").removeClass("visible");
} else {
  $("#grupoFilesDesmonte").addClass("visible");
}
});
***********************************************************************/

//Funciones que permiten limitar el número de caracteres para #areaObservaciones
function init_contadorTa(idtextarea, idcontador, max) {
    $("#" + idtextarea).attr("maxlength", max);
    $("#" + idtextarea).keyup(function () {
        updateContadorTa(idtextarea, idcontador, max);
    });

    $("#" + idtextarea).change(function () {
        updateContadorTa(idtextarea, idcontador, max);
    });
}

function updateContadorTa(idtextarea, idcontador, max) {
    var contador = $("#" + idcontador);
    var ta = $("#" + idtextarea);
    contador.html("0/" + max);
    contador.html(ta.val().length + "/" + max);
}
//Inicializar el contador de caracteres
init_contadorTa("areaObservaciones", "contadorarea", 100);