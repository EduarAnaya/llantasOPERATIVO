var _Caneca;
var $cajas;
var $llanta;
var _kmTrabajoactual;
var _placaActual;

$(function() {
  _Caneca = $(".caneca");
  $cajas = $(".caja");
  $llanta = $(".llanta");
  _kmTrabajoactual = $("#_KmAct")[0].innerText;
  _placaActual = $("#_PlacaAct")[0].innerText;

  llantaDraggable($llanta);
  canecaDoppable(_Caneca);
  cajaDroppable($cajas);
  llantaClick();
});

//evento que permite mostrar un collapse y ocultar los demás que estén abiertos.
$(".collapse").on("show.bs.collapse", function(e) {
  var dataID = e.target.id;
  $(".collapse").collapse("hide");
  $(dataID).collapse();
});

$(document).on("click", function(event) {
  var triger = event.target.className;
  var llact = $(this);
  if (triger != "imgSentido btn") {
    $(llact).attr({ "data-toggle": "collapse", "aria-expanded": "false" });
  } else {
    $(llact).removeAttr("data-toggle", "aria-expanded");
  }
});

function llantaClick() {
  $(".llanta").on("click", function(event) {
    var triger = event.target.className;
    var llact = $(this);
    if (triger != "imgSentido btn") {
      $(llact).attr({ "data-toggle": "collapse", "aria-expanded": "false" });
    } else {
      $(llact).removeAttr("data-toggle", "aria-expanded");
    }
  });
}

$("#fomrAddllanta").validate({
  rules: {
    nllanta: {
      required: true
    }
  },
  messages: {
    nllanta: {
      required: " Falta # de parte!."
    }
  }
});
var _llantaData = 0;
$("#fomrAddllanta").on("submit", function(event) {
  event.preventDefault();
  $("#fomrAddllanta").validate();
  var estado = $("#fomrAddllanta").valid();
  if (estado != false) {
    if (_llantaData <= 9) {
      var nroLlanta = $("#inputnllanta")
        .val()
        .toUpperCase();
      add_nuevallanta(nroLlanta);
      _llantaData = _llantaData + 1;
    }
  }
});

//evento que permite cambiar el sentido sentido de la imagen que indica la dirección de la llanta
function cambio_Sentido($img) {
  var dirVale = $($img).attr("tag");
  if (dirVale == 0) {
    console.log("la llanta cambio de izquierda a derecha");
    var s = $($img);
    $(s).attr("src", "/Content/medios/right.png");
    $(s).attr("tag", 1);
  }
  if (dirVale == 1) {
    console.log("la llanta cambio de derecha a izquierda");
    var s = $($img);
    $(s).attr("src", "/Content/medios/left.png");
    $(s).attr("tag", 0);
    return false;
  }
  $(".collapse").collapse("hide");
}

//PREPARA UNA NUEVA LLANTA DEL INVENTARIO PARA SER ARRASTRADA AL VEHICULO
function add_nuevallanta(nroLlanta) {
  //OBTENER LA POSICION DE LA LLANTA
  var pos = listaLllantas.indexOf(nroLlanta);
  /*
    ENTONCES:
    - POS <0  = EL NUMERO DE LLANTA INDICADO NO EXISTE DENTRO DE LA LISTA DISPONIBLE, EL USUARIO PUDO INGRESARLO MANUALMENTE
    - POS >=0 = EL NUMERO DE LLANTA INDICADO EXISTE DENTRO DE LA LISTA DISPONIBLE, SE PERMITE EL REENDERIZADO DE LA LLANTA IMPORTADA
    */
  if (pos > -1) {
    //SE ELIMINA LA LLANTA INPORTADA DE LA LISTA, PARA QUE NO LA INGRESEN MAS DE UNA VEZ
    listaLllantas.splice(pos, 1);
    $("#inputnllanta").val("");
    //SE PREPARA ELEMENTO
    var llanta = nroLlanta.split("-")[0];
    var grupo = nroLlanta.split("-")[1];
    var _importLlanta = $(
      '<div id="' +
        llanta +
        '" class="Nllanta llanta btn btn-outline-success"  href="#infoLlanta' +
        llanta +
        '" role="button" >' +
        '<img class="imgSentido btn" src="../Content/medios/left.png" tag="0" onclick="cambio_Sentido($(this))"/>' +
        '<span class="idllanta">' +
        llanta +
        "-" +
        grupo +
        "</span>" +
        '<div class="collapse infoLlanta" id ="infoLlanta' +
        llanta +
        '" >' +
        '<div class="card card-body" style="padding:0;">' +
        '<dl class="row" style="margin:0;">' +
        '<dt class="col-5 marginTituloDesc">Grupo</dt>' +
        '<dd class="col-5 marginDescTtulo">' +
        grupo +
        "</dd>" +
        '<dt class="col-5 marginTituloDesc">Km Instala</dt>' +
        '<dd class="col-5 marginDescTtulo">' +
        _kmTrabajoactual +
        "</dd>" +
        "</dl>" +
        "</div>" +
        "</div>" +
        "</div >"
    );

    //SE REENDERIZA LA LLANTA IMPORTADA
    $("#nuevallanta").append(_importLlanta);
    //SE HABILITA FUNCION DRAG DROP PARA LA LLANA IMPORTADA
    llantaDraggable($(".llanta"));
    llantaClick();
  }
}

//CONVIERTIR UNA LLANTA EN ARRASTABLE
function llantaDraggable(objetos) {
  $(objetos).draggable({
    revert: "invalid",
    start: function(event, ui) {
      $(this).addClass("llanta_dragg");
    },
    stop: function(event, ui) {
      $(this).removeClass("llanta_dragg");
    }
  });
}

//COMVERTIR UNA CAJA EN CONTENEDORA
function cajaDroppable(objetos) {
  $(objetos).droppable({
    accept: ".llanta",
    drop: function(ev, llantaEntra) {
      var caja = $(this); //CONTENEDOR EN LA QUE SE SUELTA LA LLANTA
      var llanta = $(llantaEntra.draggable); //LA LLANTA QUE SE SUELTA EN LA CAJA
      var hijos = caja.children(); //GUARDA EL ELEMENTO QUE ESTE DENDRO DE UNA CAJA
      //hashclass

      //CAMBIO ENTRE LLANTAS

      //si la llanta es importada, la llanta vieja enviarla a la basura
      if (llanta.hasClass("Nllanta")) {
        llanta.removeClass("Nllanta");
        if (hijos.length != 0) {
          remover(hijos[0]);
          $(caja).append(
            llanta.css({
              left: "0",
              top: "0"
            })
          );
        } else {
          $(caja).append(
            llanta.css({
              left: "0",
              top: "0"
            })
          );
        }
      } else if (hijos.length != 0) {
        console.log("hay algo para cambiar");
        cambiar(llanta[0], hijos[0]);
      } else {
        console.log("no hay nada");
        $(caja).append(
          $(llanta).css({
            left: "0",
            top: "0"
          })
        );
      }
    }
  });
}

//COMVERTIR LA CANECA DE BASURA

function canecaDoppable(objetos) {
  $(objetos).droppable({
    accept: ".llanta",
    drop: function(e, u) {
      $(_Caneca).removeClass("caneca_over");
      var $elemento = $(u.draggable);
      remover($elemento);
    },
    over: function(u, e) {
      $(this).addClass("caneca_over");
    },
    out: function(u, e) {
      $(this).removeClass("caneca_over");
    }
  });
}

//ENVIAR UNA LLANTA A LA CANECA
function remover($item) {
  var $nitem = $($item)
    .detach()
    .css({
      left: "0",
      top: "0"
    });
  $(_Caneca).append($nitem);
  var valor = $nitem[0].innerText;
  var llanta = valor.split("-")[0];
  var grupo = valor.split("-")[1];

  //desmontarLlanta(_placaActual, llanta, grupo, _kmTrabajoactual, _fechaActual)
  desmontarLlanta(_placaActual, llanta, grupo, _kmTrabajoactual);
  console.log("Se removio la llanta " + llanta + " del grupo " + grupo);
}
//FUNCNIONES QUE PERMITEN LA INTERACCION DINAMICA DEL USUARIO CON LAS LLANTAS

//MUESTRA EL MODAL PARA DESMONTAR UNA LLANTA
function desmontarLlanta(_placa, _llanta, _grupo, _kmMEdida) {
  $.post("/llantas/removerllantasPost/", {
    _placa: _placa,
    _llanta: _llanta,
    _grupo: _grupo,
    _kmMEdida: _kmMEdida
  }).done(function(data) {
    $("#modMuestraDesmonta").html(data);
    $("#modMuestraDesmonta").modal("show");
  });
}

//CAMBIAR UNA LLANTA POR OTRA
function cambiar(nuevo, actual) {
  var Nuevo = $(nuevo).css({
    left: "0",
    top: "0"
  });
  var Actual = $(actual).css({
    left: "0",
    top: "0"
  });
  //padres
  var papa_origen = $(nuevo).parent();
  var papa_destino = $(actual).parent();

  papa_destino.append($(Nuevo));
  papa_origen.append($(Actual));

  console.log(
    "Del eje " + papa_origen.attr("id") + " al eje " + papa_destino.attr("id")
  );
}
