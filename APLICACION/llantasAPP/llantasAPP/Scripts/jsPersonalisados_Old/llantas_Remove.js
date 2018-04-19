
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
    radioOpciones: {
      required: "Seleccione el destino de la llanta!"
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

$("#formMuestrasDesmonta").on("submit", function (event) {
  //event.preventDefault();
  $("#formMuestrasDesmonta").validate();
  var estado = $("#formMuestrasDesmonta").valid();
  if (estado != false) {
    //REALIZAR ACCIONES CON LOS DATOS RECOGIDOS


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

$("#selectCausa").change(function () {
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

$("#checkTemperatura").change(function () {
  var estado = $(this).prop("checked");
  if (estado) {
    $("#inutTemperatra").removeAttr("disabled");
  } else {
    $("#inutTemperatra").attr("disabled", "");
  }
});

$("#checkPresion").change(function () {
  var estado = $(this).prop("checked");
  if (estado) {
    $("#inutPresion").removeAttr("disabled");
  } else {
    $("#inutPresion").attr("disabled", "");
  }
});
$("input[name$='radioOpciones']").change(function () {
  var opcion = $(this).val();
  if (opcion != 1) {
    $("#grupoFilesDesmonte").removeClass("visible");
  } else {
    $("#grupoFilesDesmonte").addClass("visible");
  }
});

function init_contadorTa(idtextarea, idcontador, max) {
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
  if (parseInt(ta.val().length) > max) {
    ta.val(ta.val().substring(0, max - 0));
    contador.html(max + "/" + max);
  }
}
init_contadorTa("areaObservaciones", "contadorarea", 100);
