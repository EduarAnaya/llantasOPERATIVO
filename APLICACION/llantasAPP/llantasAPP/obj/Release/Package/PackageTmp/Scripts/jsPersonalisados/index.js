

$(function () {

    //*********VARIABLES INICIALES*********//
    var _Caneca;
    /*  var _LlantasInicial = [], */
    var _llantasImportadas = [],
        _llantasMontadas = [],
        _llantasManipuladas = [],
        _llantasRemovidas = [],
        _llantasData = [];
    var _llantaData = 0,
        _kmTrabajoactual = 0;
    var _placaActual = "";
    var _fechaActual,
        fechaTrabajo;
    //********* ACCIONES INICIALES*********//
    actualizarFecha();

    function actualizarFecha() {

        var fecha = new Date();
        var dia = fecha.getDate();
        var mesA = new Array();
        mesA[0] = "01";
        mesA[1] = "02";
        mesA[2] = "03";
        mesA[3] = "04";
        mesA[4] = "05";
        mesA[5] = "06";
        mesA[6] = "07";
        mesA[7] = "08";
        mesA[8] = "09";
        mesA[9] = "10";
        mesA[10] = "11";
        mesA[11] = "12";
        var mes = mesA[fecha.getMonth()];
        var ano = fecha.getFullYear();
        //var ano_2d = ano.toString().substring(2, 4)

        _fechaActual = dia + "/" + mes + "/" + ano;


    }

    //VALIDAR FONMULARIOS
    $("#formBuscarPlaca").validate({
        rules: {
            placa: {
                required: true,
                validarplaca: true
            },
            km: {
                required: true,
                number: true,
                min: 1000
            }
        },
        messages: {
            placa: {
                required: "Placa obligatoria!",
            },
            km: {
                required: "Km obligatorio!",
                number: "Solo números!",
                min: "Debe mayor o igual que 1000"
            }
        }
    });

    $("#formAddLlanta").validate({
        rules: {
            nllanta: {
                required: true,
                validarllanta: true
            }
        },
        messages: {
            nllanta: {
                required: "Falta # de parte!"
            }
        }
    });

    $("#formMuestras").validate({
        rules: {
            inputPresion: {
                required: true,
                validarDecimal: true,
                number: true
            },
            inputLizq: {
                required: true,
                validarDecimal: true,
                number: true
            },
            inputCent: {
                required: true,
                validarDecimal: true,
                number: true
            },
            inputDere: {
                required: true,
                validarDecimal: true,
                number: true
            },
            kmMuestra: {
                required: true,
                number: true
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
            kmMuestra: {
                required: "Km Obligatorio!",
                number: "Solo números!"
            }
        }
    });

    //**********VALIDAR ENTRADAS ************/
    //VALIDAR PLACA (AAA###)
    jQuery.validator.addMethod("validarplaca", function (value, element) {
        return this.optional(element) || /(^[a-zA-Z]{3}\d{3}$|^[a-zA-Z]{1}\d{5}$)/.test(value);
    }, "Formato Incorrecto!");

    //VAIDAR NRO DE LLANTA (######-###)
    jQuery.validator.addMethod("validarllanta", function (value, element) {
        return this.optional(element) || /^\d{4,6}-\d{3}$/.test(value);
    }, "Formato Incorrecto!");
    //VALIDAR DECIMALES (CARGA PROF LLANTAS)
    jQuery.validator.addMethod("validarDecimal", function (value, element) {
        return this.optional(element) || /(^\d{1,2}$|^\d{1,2}[.]\d{1,2}$)/.test(value);
    }, "Formato incorrecto!");

    //*********     EVENTOS DE USUARIO       *********//

    //FORMULARIO DE CONSULTA DE LLANTAS VALIDADO
    $("#formBuscarPlaca").on("submit", function (event) {
        event.preventDefault();
        $("#formBuscarPlaca").validate();
        var estado = $("#formBuscarPlaca").valid();
        if (estado != false) {
            //pendiente para la llamada de la busqueda de la placa con el conrolador
            var placa = $("#inputsplaca").val().toUpperCase();
            cargar_llantas(placa);
            cargar_llantasFlash();
        }
    });

    //FORMULARIO DE IMPORTACION DE LLANTAS
    $("#formAddLlanta").on("submit", function (event) {
        event.preventDefault();
        $("#formAddLlanta").validate();
        var estado = $("#formAddLlanta").valid();
        if (estado != false) {

            if (_llantaData <= 9) {
                var nroLlanta = $("#inputnllanta").val().toUpperCase();
                add_nuevallanta(nroLlanta);
                _llantaData = _llantaData + 1;
            }

        }
    });

    //CUANDO EL USUARIO PULSE GUARDAR
    $("#bntGuardar").on("click", function () {
        //LLAMADO AL METODO PARA CARGAR A LAS LLANTAS IMPORTADAS ("LAS NUEVAS") Y LAS LLANTAS REMOVIDAS ("LAS QUE SE DEJAN EN LA CANECA DE BASURA")
        llantasGuardar();
    });

    $("#formMuestras").on("submit", function (event) {
        event.preventDefault();
        $("#formMuestras").validate();
        var estado = $("#formMuestras").valid();
        fechaTrabajo = $("#datepicker").val();
        alert(fechaTrabajo)

        if (estado != false) {
            //REALIZAR ACCIONES CON LOS DATOS RECOGIDOS
            eliminarPRessionesllantas();
            $('#modMuestra').modal('hide');

        }
    });



    //*********     FUNCIONES     *********//

    //Visualiza el contenido al usuario
    function habilitarContenido() {
        //limpiar acciones vacias
        $("#lblMresultados").addClass("visible");

        //habilitar btn Guardar
        $('#bntGuardar').removeAttr('disabled');

        //MOSTRAR SECCIONES
        $("#dtllTrabajoactual").removeClass("visible");
        _placaActual = $("#inputsplaca").val().toUpperCase();
        $("#tActual").html("");
        $("#tActual").html(_placaActual);

        _kmTrabajoactual = $("#inputskm").val().toUpperCase();
        $("#kmActual").html("");
        $("#kmActual").html(_kmTrabajoactual);

        $("#dateActual").html("");
        $("#dateActual").html(_fechaActual);

        $("#cajacontrolcent").removeClass("visible");
        $("#cajacontrolesderc").removeClass("visible");

        //LLAMADO A LAS FUNCIONES QUE PERMITEN EL FLUJO DE MOVIEMENTOS CON DRAG DROP

        //LLAMADO A LA CONSTRUCCION DE LOS ARRASTRABLES ("LLANTAS")
        $llantas = $(".llanta");
        llantaDraggable($llantas);

        //LLAMADO A LA CONSTRUCCION DE LOS CONTENEDORES
        var $cajas = $(".caja");
        cajaDroppable($cajas);

        //LAMADO A LA DEFINICION DE LA CANECA DE BASURA
        _Caneca = $(".caneca");//CONTENEDOR QUE CONTIENE LAS LLANTAS REMOVIDAS
        canecaDoppable(_Caneca);


        habilitarMediciones();

    };

    //Oculta el contenido al usuario
    function ocultarContenido() {
        //>trabajo actual
        $("#dtllTrabajoactual").addClass("visible");

        //deshabilitar btn Guardar
        $('#bntGuardar').attr('disabled', "");
        $("#cajacontrolcent").addClass("visible");
        $("#cajacontrolesderc").addClass("visible");
    };

    //REENDERIZAR CONSULTA LLANTAS
    function renderLlantas(paquete) {
        var t_Vechiculo = paquete[0][0];//C=Cabezote:T=Trailer
        var nroEjes = paquete[0][1];
        var paqueteLlantas = paquete[1];
        //_LlantasInicial = paqueteLlantas;

        //LIMPIAR ACTUALES
        var llantasPuesta = $("#camion div");
        $(llantasPuesta).remove();


        $("#camion").addClass("camionLimpio");


        //RECORREMOS EL PAQUETE DE LLANTAS, PARA GENERER DE CADA UNA LA VISTA AL USUARIO, (IMPOTANTES LOS ID=EJE#, PUESTO QUE CADA NUEMERO (1-10 PARA EL CASO DE LOS CABEZOTES TIENE ASIGNADO UNA POISICION))
        var errores = 0;
        if (t_Vechiculo == "C") {
            if (paqueteLlantas.length >= 0 || paqueteLlantas.length <= 10) {
                for (var i = 0; i < 10; i++) {
                    var ejes = '<div id="eje' + (i + 1) + '" class="eje ">'
                        + '<div class="caja">'
                        + '</div>'
                        + '</div>';//SE ARMA LA BASE DE LA LLANTA CAJA+EJE+LLANTA
                    $("#camion").append(ejes);//SE AGREGA AL CAMION LA NUEVA LLANTA

                    $("#camion").removeClass("camionLimpio");
                    $("#camion").removeClass("camionTrailerx1");
                    $("#camion").removeClass("camionTrailerx2");
                    $("#camion").removeClass("camionTrailerx3");
                    $("#camion").addClass("camionCabezote");

                    //UBICACION DE LLANTAS
                    $("#eje1").css({ 'top': '72%', 'left': '2%' });
                    $("#eje2").css({ 'top': '20%', 'left': '2%' });
                    $("#eje3").css({ 'top': '88%', 'left': '43%' });
                    $("#eje4").css({ 'top': '72%', 'left': '43%' });
                    $("#eje5").css({ 'top': '20%', 'left': '43%' });
                    $("#eje6").css({ 'top': '3%', 'left': '43%' });
                    $("#eje7").css({ 'top': '88%', 'left': '85%' });
                    $("#eje8").css({ 'top': '72%', 'left': '85%' });
                    $("#eje9").css({ 'top': '20%', 'left': '85%' });
                    $("#eje10").css({ 'top': '3%', 'left': '85%' });
                }
            } else {
                errores = 1;
            }
        } if (t_Vechiculo == "T") {
            if (nroEjes == "1") {
                if (paqueteLlantas >= 0 || paqueteLlantas.length <= 4) {
                    for (var i = 0; i < 4; i++) {
                        var ejes = '<div id="eje' + (i + 1) + '" class="eje ">'
                            + '<div class="caja">'
                            + '</div>'
                            + '</div>';//SE ARMA LA BASE DE LA LLANTA CAJA+EJE+LLANTA
                        $("#camion").append(ejes);//SE AGREGA AL CAMION LA NUEVA LLANTA
                    }
                    $("#camion").removeClass("camionLimpio");
                    $("#camion").removeClass("camionCabezote");
                    $("#camion").removeClass("camionTrailerx2");
                    $("#camion").removeClass("camionTrailerx3");
                    $("#camion").addClass("camionTrailerx1");

                } else {
                    errores = 1;
                }

            } else if (nroEjes == "2") {
                if (paqueteLlantas >= 0 || paqueteLlantas.length <= 8) {
                    for (var i = 0; i < 8; i++) {
                        var ejes = '<div id="eje' + (i + 1) + '" class="eje ">'
                            + '<div class="caja">'
                            + '</div>'
                            + '</div>';//SE ARMA LA BASE DE LA LLANTA CAJA+EJE+LLANTA
                        $("#camion").append(ejes);//SE AGREGA AL CAMION LA NUEVA LLANTA
                    }
                    $("#camion").removeClass("camionLimpio");
                    $("#camion").removeClass("camionCabezote");
                    $("#camion").removeClass("camionTrailerx1");
                    $("#camion").removeClass("camionTrailerx3");
                    $("#camion").addClass("camionTrailerx2");


                    //UBICACION DE LLANTAS
                    $("#eje1").css({ 'top': '88%', 'left': '43%' });
                    $("#eje2").css({ 'top': '72%', 'left': '43%' });
                    $("#eje3").css({ 'top': '20%', 'left': '43%' });
                    $("#eje4").css({ 'top': '3%', 'left': '43%' });
                    $("#eje5").css({ 'top': '88%', 'left': '85%' });
                    $("#eje6").css({ 'top': '72%', 'left': '85%' });
                    $("#eje7").css({ 'top': '20%', 'left': '85%' });
                    $("#eje8").css({ 'top': '3%', 'left': '85%' });

                } else {
                    errores = 1;
                }

            } else if (nroEjes == "3") {
                if (paqueteLlantas >= 0 || paqueteLlantas.length <= 12) {
                    for (var i = 0; i < 12; i++) {
                        var ejes = '<div id="eje' + (i + 1) + '" class="eje ">'
                            + '<div class="caja">'
                            + '</div>'
                            + '</div>';//SE ARMA LA BASE DE LA LLANTA CAJA+EJE+LLANTA
                        $("#camion").append(ejes);//SE AGREGA AL CAMION LA NUEVA LLANTA
                    }
                    $("#camion").removeClass("camionLimpio");
                    $("#camion").removeClass("camionCabezote");
                    $("#camion").removeClass("camionTrailerx1");
                    $("#camion").removeClass("camionTrailerx2");
                    $("#camion").addClass("camionTrailerx3");

                    $("#eje1").css({ 'top': '88%', 'left': '2%' });
                    $("#eje2").css({ 'top': '72%', 'left': '2%' });
                    $("#eje3").css({ 'top': '20%', 'left': '2%' });
                    $("#eje4").css({ 'top': '3%', 'left': '2%' });
                    $("#eje5").css({ 'top': '88%', 'left': '43%' });
                    $("#eje6").css({ 'top': '72%', 'left': '43%' });
                    $("#eje7").css({ 'top': '20%', 'left': '43%' });
                    $("#eje8").css({ 'top': '3%', 'left': '43%' });
                    $("#eje9").css({ 'top': '88%', 'left': '85%' });
                    $("#eje10").css({ 'top': '72%', 'left': '85%' });
                    $("#eje11").css({ 'top': '20%', 'left': '85%' });
                    $("#eje12").css({ 'top': '3%', 'left': '85%' });

                } else {
                    errores = 1;
                }
            }
        }
        if (errores == 0) {

            for (var j = 0; j < paqueteLlantas.length; j++) {
                var idLlanta = paqueteLlantas[j].LLANTA + "-" + paqueteLlantas[j].GRUPO;//SE GENERA LA ETIQUETA DE  LA LLANTA
                var pos = paqueteLlantas[j].POSICION;
                var llantaNueva = '<div class="llanta">'
                    + '<span class="idllanta"></span>'
                    + '</div>';//SE ARMA LA BASE DE LA LLANTA CAJA+EJE+LLANTA
                $("#eje" + pos + " .caja").append(llantaNueva);

                var llantas = $(".llanta");//SE CARGA LLANTA EN MEMORIA PARA PODER USARLA
                $(llantas[j]).find(".idllanta").html("");//SE LIMPIA SU ETIQUETA
                $(llantas[j]).find(".idllanta").html(idLlanta);//SE LE ASIGNA LA ETIQUETA A LA  LLATA CON SU RESPECTIVA ETIQUETA
            }
            habilitarContenido();
        } else {
            ocultarContenido();
            alert("Relación incoherente del numero de llantas montadas para este vehículo, imposible continuar!");
            Location.reload();
        }

    };

    //SOLICITUD PARA BUSCAR ACTUALES DE LA PLACA INDICADA
    function cargar_llantas(placa) {
        console.log("cargando...");
        //PETICION POST AJAX AL METODO'cargarllantas' DEL CONTROLADOR 'llantas'
        var exprCabesote = RegExp(/^[a-zA-Z]{3}\d{3}$/);
        var exprTrailer = RegExp(/^[a-zA-Z]{1}\d{5}$/);

        var cabezote = exprCabesote.test(placa);
        var trailer = exprTrailer.test(placa);

        var tipoVehiculo = 0;//1=cabezote:2=trailer

        if (cabezote) {
            tipoVehiculo = 1;
            console.log("Datos cabezote!");
        }
        if (trailer) {
            tipoVehiculo = 2;
            console.log("Datos trailer!");
        }
        $.post(
            "/llantas/cargarllantas",
            { _tipoVehiculo: tipoVehiculo, _placa: placa }
        ).done(function (paquete) {
            if (paquete[0] == 0 && paquete[1] == 0) {
                limparPantalla();
                console.log("Placa desconocida!");
            }
            if (paquete[0] != 0) {
                renderLlantas(paquete);//llamado a la funcion que reenderiza las llantas consultadas
                console.log("Llantas cargadas!");
            }
        }).fail(function () {
            alert("Error!, por favor reintente la transacción.");
        });
    };

    //PRERAR LA BUSQUEDA RAPIDA DE LLANTAS (SOLO SI 'trabajo_actual!=0'), SE CARGARA CON LA LISTA DE LLANTAS DISPONIBLES DE INVENARIO
    function cargar_llantasFlash() {
        //PETICION POST AJAX AL METODO'llantasDisponibles' DEL CONTROLADOR 'llantas'
        $.post(
            "/llantas/llantasDisponibles"
        ).done(function (paquete) {
            if (paquete != 0) {
                for (var i = 0; i < paquete.length; i++) {
                    var llantaImport = paquete[i][0] + "-" + paquete[i][1];
                    _llantasData.push(llantaImport);
                }
            } else {
                console.log("Sin datos...");
            }
        }
        ).fail(function () {
            alert("Error!, por favor reintente la transacción.");
        })
        //CARGAR LA LISTA RAPIDA CON LAS LLANTAS DISPONIBLES
        $("#inputnllanta").autocomplete(
            {
                source: _llantasData
            }
        );
    };

    //PREPARA UNA NUEVA LLANTA DEL INVENTARIO PARA SER ARRASTRADA AL VEHICULO
    function add_nuevallanta(nroLlanta) {
        //OBTENER LA POSICION DE LA LLANTA
        var pos = _llantasData.indexOf(nroLlanta);
        /*
        ENTONCES:
        - POS <0  = EL NUMERO DE LLANTA INDICADO NO EXISTE DENTRO DE LA LISTA DISPONIBLE, EL USUARIO PUDO INGRESARLO MANUALMENTE
        - POS >=0 = EL NUMERO DE LLANTA INDICADO EXISTE DENTRO DE LA LISTA DISPONIBLE, SE PERMITE EL REENDERIZADO DE LA LLANTA IMPORTADA
        */
        if (pos > -1) {
            //SE ELIMINA LA LLANTA INPORTADA DE LA LISTA, PARA QUE NO LA INGRESEN MAS DE UNA VEZ
            _llantasData.splice(pos, 1);
            $("#inputnllanta").val("");
            //SE PREPARA ELEMENTO
            var nuevallanta = '<div class="Nllanta llanta">'
                + '<span class="idllanta">' + nroLlanta + '</span>'
                + '</div>';
            //SE REENDERIZA LA LLANTA IMPORTADA
            $("#nuevallanta").append(nuevallanta);
            //SE HABILITA FUNCION DRAG DROP PARA LA LLANA IMPORTADA
            $llantas = $(".Nllanta");
            llantaDraggable($llantas);
        }
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
                var caja = $(this);//CONTENEDOR EN LA QUE SE SUELTA LA LLANTA
                var llanta = $(llantaEntra.draggable);//LA LLANTA QUE SE SUELTA EN LA CAJA
                var hijos = caja.children();//GUARDA EL ELEMENTO QUE ESTE DENDRO DE UNA CAJA
                //hashclass

                //CAMBIO ENTRE LLANTAS

                //si la llanta es importada, la llanta vieja enviarla a la basura
                if (llanta.hasClass("Nllanta")) {
                    if (hijos.length != 0) {
                        remover(hijos[0]);
                        $(caja).append(llanta.css({
                            "left": "0",
                            "top": "0"
                        }));
                    } else {
                        $(caja).append(llanta.css({
                            "left": "0",
                            "top": "0"
                        }));
                    }
                } else if (hijos.length != 0) {
                    console.log("hay algo para cambiar");
                    cambiar(llanta[0], hijos[0]);
                } else {
                    console.log("no hay nada");
                    $(caja).append($(llanta).css({
                        "left": "0",
                        "top": "0"
                    }));
                }
            }
        })

    };

    //COMVERTIR LA CANECA DE BASURA
    function canecaDoppable(objetos) {
        $(objetos).droppable({
            accept: ".llanta",
            drop: function (e, u) {
                $(_Caneca).removeClass("caneca_over");
                var $elemento = $(u.draggable);
                remover($elemento);
            },
            over: function (u, e) {
                $(this).addClass("caneca_over");
            },
            out: function (u, e) {
                $(this).removeClass("caneca_over");
            }
        })
    };

    //ENVIAR UNA LLANTA A LA CANECA (DESAPARESER)
    function remover($item) {
        var $nitem = $($item)
            .detach()
            .css({
                "left": "0",
                "top": "0"
            });
        $(_Caneca).append($nitem);
        console.log("Se removio la llanta " + $nitem[0].innerText);

    };

    //CAMBIAR UNA LLANTA POR OTRA
    function cambiar(nuevo, actual) {
        var Nuevo = $(nuevo).css({
            "left": "0",
            "top": "0"
        });
        var Actual = $(actual).css({
            "left": "0",
            "top": "0"
        });
        //padres
        var papa_origen = $(nuevo).parent();
        var papa_destino = $(actual).parent();

        papa_destino.append($(Nuevo)).fadeIn();
        papa_origen.append($(Actual)).fadeIn();

        console.log(papa_origen.attr("id") + " " + " " + papa_destino.attr("id"));
    };

    //FOTOGRAFIA DE LAS LLANTAS AL TEMINAR
    function llantasGuardar() {

        var llMontadas = $("#camion .llanta .idllanta");
        _llantasMontadas = [];
        for (var i = 0; i < llMontadas.length; i++) {
            var llValor = $(llMontadas[i]).text();

            var llLLanta = llValor.split("-")[0];
            var llPosicion = i + 1;

            var llantaMontada = { LLANTA: llLLanta, POSICION: llPosicion }
            _llantasMontadas.push(llantaMontada);
        }

        //cargar llantas importadas
        var llantasImport = $("#camion .Nllanta .idllanta");
        for (var i = 0; i < llantasImport.length; i++) {
            var valor = $(llantasImport[i]).text();
            var llLLanta = valor.split("-")[0];

            var llantaImport = { LLANTA: llLLanta }
            _llantasImportadas.push(llantaImport);
        }

        $.post(
            "/llantas/llantasGuardar",
            { _placa: _placaActual, _fechaInstala: _fechaActual, _kmIntsla: _kmTrabajoactual, llantasMontadas: JSON.stringify(_llantasMontadas), llantasImportadas: JSON.stringify(_llantasImportadas) }
        ).done(function (data) {
            if (data == 1) {
                mostrarRespuesta();
            } else {
                alert("LLANTAS INCOMPLETAS");
            }

        }
        ).fail(function (response) {
            alert("mal")
        });

    };

    //REENDERIZAR LA RESPUESTA
    function mostrarRespuesta() {
      /*   //LISTAR LAS  IMPORTADAS
        for (var i = 0; i < _llantasImportadas.length; i++) {
            var valor = _llantasImportadas[i];
            var elemento = "<li>" + valor + "</li>"
            $("#lImportadas").append(elemento);
        }
        //LISTAR LAS ACTUALES
        for (var i = 0; i < _llantasMontadas.length; i++) {
            var valor = _llantasMontadas[i];
            var elemento = "<li>" + valor + "</li>"
            $("#lMontadas").append(elemento);
        }
        //LISTAR LAS REMOVIDAS
        for (var i = 0; i < _llantasRemovidas.length; i++) {
            var valor = _llantasRemovidas[i];
            var elemento = "<li>" + valor + "</li>"
            $("#lRemovidas").append(elemento);
        } */
        //MOSTRAR MODAL
        $('#modales').modal('show');


        $('#modales').on('hidden.bs.modal', function (e) {
            limparPantalla();
            location.reload();
        })
    }

    //LIMPIA EL FORMULARIO AL GUARDAR
    function limparPantalla() {
        //VACIAR ARREGLOS
        _llantasImportadas = [];
        _llantasMontadas = [];
        _llantasRemovidas = [];
        _llantaData = 0;
        _llantasData = [];

        //LIMPIAR CAMION
        var llantasPuesta = $("#camion div");
        $(llantasPuesta).remove();

        //LIMPIAR IMPORTADAS
        var llantasImportarasDel = $("#nuevallanta div");
        $(llantasImportarasDel).remove();

        //LIMPIAR CANECA
        var llantasBasura = $(".caneca div");
        $(llantasBasura).remove();
        $("#lblMresultados").html("");
        $("#lblMresultados").html("Placa desconocida!");
        $("#lblMresultados").removeClass("visible");
        $("#tActual").html("");
        $("#kmActual").html("");
        $("#inputsplaca").val("");
        $("#inputskm").val("");
        ocultarContenido();
    };

    //HABILITA LA FUNCION QUE ESCUCHA AL PRESIONAR UNA LLANTA POR 3 SEGUNDOS
    function habilitarMediciones() {
        var timer = 0;
        $(".llanta").on("click", function () {
            var placa = _placaActual;
            var idllantas = $(this).text();
            $("#idPlacaMed").html("");
            $("#idPlacaMed").html(placa);
            $("#idLlantaMed").html("");
            $("#idLlantaMed").html(idllantas);
            $('#modMuestra').modal('show');

            $("#datepicker")
                .datepicker({ dateFormat: "dd/mm/y" })
                .datepicker("setDate", _fechaActual);
        })

    };

    //FUNCION QUE LIMPIA EL FORMULARIO DE INGRESODE PRESIONES DE LAS LLANTAS
    function eliminarPRessionesllantas() {
        $("#inputPresion").val("");
        $("#inputLizq").val("");
        $("#inputCent").val("");
        $("#inputDere").val("");
    };
});