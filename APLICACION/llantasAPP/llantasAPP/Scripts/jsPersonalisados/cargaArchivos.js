
$(function () {
    var datos = [];
    var daticos = "nuevos datos";
    $(".soportesDesmonte").on("change", function () {
        var formulario=$("#formMuestrasDesmonta")[0];
        var formdata=new FormData(formulario);

        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/cargaArchivos/cargarArchivo",
            data: formdata,
            processData: false,
            contentType: false,
            cache: false,
            timeout: 600000,
            success: function (data) {
                
                var registro = '<tr>'
                + '<td scope="row">1</td>'
                + '<td>'
                + '<img src="' + event.target.result + '" style="width:20px;"/>'
                + '</td>'
                + '<td class="text-truncate" style="max-width:100px">' + data + '</td>'
                + '<td>' + tamañoDoc + ' Kb</td>'
                + '<td>'
                + '<img class="borarFila" src="/Content/medios/borrarFile.png" />'
                + '</td>'
                + '</tr>';

            $("#subidos tbody").append(registro);
            borrar();
            },
            error: function (e) {
                console.log("ERROR : ", e);
            }
        });






        /* 
        
        
                for (var i = 0; i < file.length; i++) {
                    var nombreDoc = file[i].name;
                    var tamañoDoc = (file[i].size / 1024).toFixed(1);
        
        
                    $.post(
                        'cargarArchivo/cargarArchivo',
                        { archivo: archivosSoportesRepara }
                    )
        
        
        
                    var reader = new FileReader();
                    reader.onload = function (event) {
                        // El texto del archivo se mostrará por consola aquí
                        var archivosSoportesRepara = $("#soportesDesmonte");
        
        
        
                        var registro = '<tr>'
                            + '<td scope="row">1</td>'
                            + '<td>'
                            + '<img src="' + event.target.result + '" style="width:20px;"/>'
                            + '</td>'
                            + '<td class="text-truncate" style="max-width:100px">' + nombreDoc + '</td>'
                            + '<td>' + tamañoDoc + ' Kb</td>'
                            + '<td>'
                            + '<img class="borarFila" src="/Content/medios/borrarFile.png" />'
                            + '</td>'
                            + '</tr>';
        
                        $("#subidos tbody").append(registro);
                        borrar();
                    };
        
                    reader.readAsDataURL(file[i]);
                }
                console.log("subido"); */


    });
    function borrar() {
        $(".borarFila").on("click", function () {
            var s = $(this)[0].parentNode.parentNode.remove();
        })
    }

})
