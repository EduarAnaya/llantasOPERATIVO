$(function () {
    var $caneca = $(".caneca");
    var $cajas = $(".caja");
    var $llantas = $(".llanta");
    var _kmTrabajoactual = $("#_KmAct")[0].innerText;
    var _placaActual = $("#_PlacaAct")[0].innerText;
    var arrayDesmonta = [];
    var arrayMonta = [];
    var arrayMuestras = [];
    var arrayCamion = [];
    var transaccion = null;
    var llNueva = null;

    var estadomuestrasDesmonta = null; //1:solo monta; 2:incluye desmonte
    llantaDraggable($llantas);
    canecaDoppable($caneca);
    cajaDroppable($cajas);

    //evento que permite mostrar un collapse y ocultar los demás que estén abiertos.
    $(".collapse").on("show.bs.collapse", function (e) {
        var dataID = e.target.id;
        $(".collapse").collapse("hide");
        $(dataID).collapse();
    });
    //evento click de la clase llanta que permite distinguir ente la imagen y el botón que llama el evento
    $(".llanta").on("click", function (event) {
        var triger = event.target.className;
        var llact = $(this);
        if (triger != "imgSentido btn") {
            $(llact).attr({
                "data-toggle": "collapse",
                "aria-expanded": "false"
            });
        } else {
            $(llact).removeAttr("data-toggle", "aria-expanded");
        };
    });
    $(".llanta").on("dblclick", function (event) {
        transaccion = 1;
        llNueva = [];
        cajaNueva = $(this.parentElement);
        montar($(this));

    });

    $("#fomrAddllanta").validate({
        rules: {
            nllanta: {
                required: true
            }
        },
        messages: {
            nllanta: {
                required: " Falta # de parte!.",
            }
        }
    });
    var _llantaData = 0;
    $("#fomrAddllanta").on("submit", function (event) {
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
            };
        };
    });

    /***************************GUARDAR LOS CAMBIOS*********************** */
    $("#btnGUARDAR").on("click", function () {
        enviarCambios(arrayCamion, arrayMonta, arrayDesmonta, arrayMuestras);
    });

    function fotovehiculo() {
        arrayCamion = [];
        var listaCamion = $(".vehiculo [tag='preload']");
        for (let index = 0; index < listaCamion.length; index++) {
            var valorLlanta = listaCamion[index].childNodes[1].innerText;
            var llanta = valorLlanta.split("-")[0];
            var grupo = valorLlanta.split("-")[1];
            var posicion = listaCamion[index].parentNode.parentElement.id.split("eje")[1];
            var llantaPreload = {
                LLANTA: llanta,
                GRUPO: grupo,
                POS: posicion
            };
            arrayCamion.push(llantaPreload);
        }
        console.log(arrayCamion);
    };

    function enviarCambios(arrayCamion, arrayMonta, arrayDesmonta, arrayMuestras) {
        $.post("/llantas/llantasGuardar/", {
            arrayCamion: JSON.stringify(arrayCamion),
            arrayMonta: JSON.stringify(arrayMonta),
            arrayDesmonta: JSON.stringify(arrayDesmonta),
            arrayMuestras: JSON.stringify(arrayMuestras)
        }).done(function (data) {
            $(".Contapp").html("");
            $(".Contapp").html(data);
        });

    };

    //evento que permite cambiar el sentido sentido de la imagen que indica la dirección de la llanta
    /*HABILITAR PARA DESARROLLO 
    function cambio_Sentido($img) {
            var dirVale = $($img).attr("tag");
            var img = $($img);
            if (dirVale == 0) {
                //la llanta cambio de izquierda a derecha
                $(img).attr("src", "/Content/medios/right.png");
                $(img).attr("tag", 1);
            };
            if (dirVale == 1) {
                //la llanta cambio de derecha a izquierda
                $(img).attr("src", "/Content/medios/left.png");
                $(img).attr("tag", 0);
                return false;
            };
            $(".collapse").collapse("hide");
        }; */

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

            /* HABILITAR LA IMG PARA EL SENTIDO DE LA LLANTA */
            /*     var _importLlanta = $(
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
                ); */

            var _importLlanta = $(
                '<div id="' +
                llanta +
                '" class="Nllanta llanta btn btn-outline-success"  href="#infoLlanta' +
                llanta +
                '" role="button" tag="Nllanta">' +
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
        };
    };
    //CONVIERTIR UNA LLANTA EN ARRASTABLE
    function llantaDraggable(objetos) {
        $(objetos).draggable({
            revert: "invalid",
            start: function (event, ui) {
                $(this).addClass("llanta_dragg");
            },
            stop: function (event, ui) {
                $(this).removeClass("llanta_dragg");
            }
        });
    };
    //COMVERTIR UNA CAJA EN CONTENEDORA
    function cajaDroppable(objetos) {
        $(objetos).droppable({
            accept: ".llanta",
            drop: function (ev, llantaEntra) {
                var caja = $(this); //CONTENEDOR EN LA QUE SE SUELTA LA LLANTA
                cajaNueva = caja;
                var llanta = $(llantaEntra.draggable); //LA LLANTA QUE SE SUELTA EN LA CAJA
                var hijos = caja.children(); //GUARDA EL ELEMENTO QUE ESTE DENDRO DE UNA CAJA
                var tagLlanta = $(llanta).attr("tag"); //valido con el tag si la llanta es importada, precargada o removida
                /*tags llantas:
                 * preload: llanta que fue cargada al momento de realizar la peticion 'llantas/editLlantas'
                 * import: llana que se sescarga de la lista de inventario
                 * remove:llanta que está o viene de estar en la caneca
                 * Pl_removed: llanta precargada, que enalgun momento se envio a la caneca y quiere ser revivida
                 */

                //CAMBIO ENTRE LLANTAS
                if (tagLlanta == "preload" || tagLlanta == "import") {
                    if (hijos.length != 0) {
                        //"hay algo para cambiar"
                        cambiar(llanta[0], hijos[0]);
                    } else {
                        //"no hay nada para cambiar"
                        $(caja).append(
                            $(llanta).css({
                                left: "0",
                                top: "0"
                            })
                        );
                    }
                    fotovehiculo();
                } else if (tagLlanta == "Pl_removed") {
                    if (hijos.length != 0) {
                        //"hay algo para cambiar"
                        cambiar(llanta[0], hijos[0]);
                    } else {
                        //"no hay nada para cambiar"
                        $(caja).append(
                            $(llanta).css({
                                left: "0",
                                top: "0"
                            })
                        );
                    };

                    //como la llanta fue revivida se debe remover del arreglo de llantas removidas
                    var llantaRev = llanta["0"].id;
                    for (let index = 0; index < arrayDesmonta.length; index++) {
                        if (llantaRev == arrayDesmonta[index].par_llanta_e) {
                            arrayDesmonta.splice(index, 1)
                            console.log(arrayDesmonta);
                        };
                    };
                    //agregar el atributo original
                    $(llanta[0]).attr("tag", "preload")
                    fotovehiculo();
                } else if (tagLlanta == "Nllanta") {
                    transaccion = 2;
                    llNueva = $(llanta);
                    if (hijos.length != 0) {
                        cajaRemove = hijos[0];
                        estadomuestrasDesmonta = 2; //1:muestreo;2:montar
                        montar($(llanta));
                    } else if (hijos.length == 0) {
                        estadomuestrasDesmonta = 1; //1:muestreo;2:montar
                        montar($(llanta));
                    };
                    $(caja).append(
                        llanta.css({
                            left: "0",
                            top: "0"
                        })
                    );
                };
            }
        });
    };
    //COMVERTIR LA CANECA DE BASURA
    function canecaDoppable(objetos) {
        $(objetos).droppable({
            accept: ".llanta",
            drop: function (e, u) {
                $caneca.removeClass("caneca_over");
                cajaRemove = u.draggable[0];
                var $elemento = $(u.draggable);
                llNueva = $();
                var tagLlanta = $($elemento).attr("tag"); //valido con el tag si la llanta es importada, precargada o removida

                if (tagLlanta == "import") {
                    $("#nuevallanta").append($elemento);
                    $($elemento).css({
                        left: "0",
                        top: "0"
                    });
                    //como la llanta fue habilitada para ser importada nuevamente se debe remover del arreglo de llantas montadas
                    var llantaRev = $($elemento)["0"].id;
                    for (let index = 0; index < arrayMonta.length; index++) {
                        if (llantaRev == arrayMonta[index].par_llanta_e) {
                            arrayMonta.splice(index, 1)
                            console.log(arrayMonta);
                        }
                    }
                    $($elemento).attr("tag", "Nllanta");
                } else {
                    remover($elemento[0]);
                };

            },
            over: function (u, e) {
                $(this).addClass("caneca_over");
            },
            out: function (u, e) {
                $(this).removeClass("caneca_over");
            }
        });
    };
    //ENVIAR UNA LLANTA A LA CANECA
    function remover($item) {
        var valor = $item.firstElementChild.innerText;
        var llanta = valor.split("-")[0];
        var grupo = valor.split("-")[1];
        desmontarLlanta(_placaActual, llanta, grupo, _kmTrabajoactual);
    };
    //prepara el metodo para montar una llanta
    function montar($item) {
        var valor = $item["0"].firstElementChild.innerText;
        var llanta = valor.split("-")[0];
        var grupo = valor.split("-")[1];
        montarLlanta(_placaActual, llanta, grupo, _kmTrabajoactual);
    };
    //FUNCNIONES QUE PERMITEN LA INTERACCION DINAMICA DEL USUARIO CON LAS LLANTAS

    //MUESTRA EL MODAL PARA DESMONTAR UNA LLANTA
    function desmontarLlanta(_placa, _llanta, _grupo, _kmMEdida) {
        $.post("/llantas/llantasRemove/", {
            _placa: _placa,
            _llanta: _llanta,
            _grupo: _grupo,
            _kmMEdida: _kmMEdida
        }).done(function (data) {
            $("#modMuestraDesmonta").html("");
            $("#modMuestraDesmonta").append(data);
            $("#modMuestraDesmonta").modal("show");
        });
    };
    //modal que permite la transaccion
    function montarLlanta(_placa, _llanta, _grupo, _kmMEdida) {
        $.post("/llantas/llantasMonta/", {
            _placa: _placa,
            _llanta: _llanta,
            _grupo: _grupo,
            _kmMEdida: _kmMEdida
        }).done(function (data) {
            $("#modMuestraMonta").html("");
            $("#modMuestraMonta").append(data);
            $("#modMuestraMonta").modal("show");
        });
    };

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
    };
});