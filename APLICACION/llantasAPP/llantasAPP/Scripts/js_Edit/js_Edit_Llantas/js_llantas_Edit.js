var $caneca=$(".caneca"),$cajas=$(".caja"),$llantas=$(".llanta"),_kmTrabajoactual=$("#_KmAct")[0].innerText,_placaActual=$("#_PlacaAct")[0].innerText,arrayDesmonta=[];llantaDraggable($llantas),canecaDoppable($caneca),cajaDroppable($cajas),$(".collapse").on("show.bs.collapse",function(a){var l=a.target.id;$(".collapse").collapse("hide"),$(l).collapse()}),$(".llanta").on("click",function(a){var l=a.target.className,t=$(this);"imgSentido btn"!=l?$(t).attr({"data-toggle":"collapse","aria-expanded":"false"}):$(t).removeAttr("data-toggle","aria-expanded")}),$("#fomrAddllanta").validate({rules:{nllanta:{required:!0}},messages:{nllanta:{required:" Falta # de parte!."}}});var _llantaData=0;function cambio_Sentido(a){var l=$(a).attr("tag");if(0==l){console.log("la llanta cambio de izquierda a derecha");var t=$(a);$(t).attr("src","/Content/medios/right.png"),$(t).attr("tag",1)}if(1==l){console.log("la llanta cambio de derecha a izquierda");t=$(a);return $(t).attr("src","/Content/medios/left.png"),$(t).attr("tag",0),!1}$(".collapse").collapse("hide")}function add_nuevallanta(a){var l=listaLllantas.indexOf(a);if(-1<l){listaLllantas.splice(l,1),$("#inputnllanta").val("");var t=a.split("-")[0],n=a.split("-")[1],e=$('<div id="'+t+'" class="Nllanta llanta btn btn-outline-success"  href="#infoLlanta'+t+'" role="button" tag="Nllanta"><span class="idllanta">'+t+"-"+n+'</span><div class="collapse infoLlanta" id ="infoLlanta'+t+'" ><div class="card card-body" style="padding:0;"><dl class="row" style="margin:0;"><dt class="col-5 marginTituloDesc">Grupo</dt><dd class="col-5 marginDescTtulo">'+n+'</dd><dt class="col-5 marginTituloDesc">Km Instala</dt><dd class="col-5 marginDescTtulo">'+_kmTrabajoactual+"</dd></dl></div></div></div >");$("#nuevallanta").append(e),llantaDraggable($(".llanta")),llantaClick()}}function llantaDraggable(a){$(a).draggable({revert:"invalid",start:function(a,l){$(this).addClass("llanta_dragg")},stop:function(a,l){$(this).removeClass("llanta_dragg")}})}function cajaDroppable(a){$(a).droppable({accept:".llanta",drop:function(a,l){var t=$(this),n=$(l.draggable),e=t.children();"Nllanta"==l.draggable[0].attributes[4].value?(0!=e.length&&remover(e[0]),$(t).append(n.css({left:"0",top:"0"}))):0!=e.length?cambiar(n[0],e[0]):$(t).append($(n).css({left:"0",top:"0"}))}})}function canecaDoppable(a){$(a).droppable({accept:".llanta",drop:function(a,l){$caneca.removeClass("caneca_over"),remover($(l.draggable))},over:function(a,l){$(this).addClass("caneca_over")},out:function(a,l){$(this).removeClass("caneca_over")}})}function remover(a){var l=a[0].children[0].innerText,t=l.split("-")[0],n=l.split("-")[1];desmontarLlanta(_placaActual,t,n,_kmTrabajoactual)}function desmontarLlanta(a,l,t,n){$.post("/llantas/llantasRemove/",{_placa:a,_llanta:l,_grupo:t,_kmMEdida:n}).done(function(a){$("#modMuestraDesmonta").html(""),$("#modMuestraDesmonta").append(a),$("#modMuestraDesmonta").modal("show")})}function cambiar(a,l){var t=$(a).css({left:"0",top:"0"}),n=$(l).css({left:"0",top:"0"}),e=$(a).parent(),o=$(l).parent();o.append($(t)),e.append($(n)),console.log("Del eje "+e.attr("id")+" al eje "+o.attr("id"))}$("#fomrAddllanta").on("submit",function(a){(a.preventDefault(),$("#fomrAddllanta").validate(),0!=$("#fomrAddllanta").valid())&&(_llantaData<=9&&(add_nuevallanta($("#inputnllanta").val().toUpperCase()),_llantaData+=1))});