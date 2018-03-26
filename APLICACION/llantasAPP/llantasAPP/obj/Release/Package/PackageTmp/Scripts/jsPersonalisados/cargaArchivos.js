
$(function () {

    //espacio para la arga de archivos
    var maxFiles = 5;
    var contador = 1;
    renderContador();
    $("#btnAddFile").on("click", function () {
        if (contador < maxFiles) {
            contador = contador + 1;
            var nuevoInput =
                '<div class="form-row rowFicheros">' +
                '<div class="col">' +
                '<input type="file" name="archivoDesmonte" required="required">' +
                "</div>" +
                '<div class="col">' +
                '<button type="button" class="btn btn-danger btndellFile">X</button>' +
                "</div>" +
                "</div>";

            $("#boxFicheros").append(nuevoInput);

            delRowFile();
            renderContador();
        }
    });

    function delRowFile() {
        $(".btndellFile").on("click", function () {
            var registro = $(this)[0].parentNode.parentNode;
            registro.remove();
            var activos = $(".boxFicheros .rowFicheros");
            contador = activos.length;
            renderContador();
            return;
        });
    }
    function renderContador() {
        $("#lblContador").html("");
        $("#lblContador").html(contador + "/" + maxFiles);
    }
    
    /*     $(".soportesDesmonte").on("change", function () {
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
                    
                    var nombredoc=data.nombreArchivo;
                    var tamañodoc=((data.tamañoArchivo)*0.001).toFixed(0);//Byte=>Kilobyte (Kb)
                    contador++;
    
                    var registro = '<tr>'
                    + '<td scope="row">'+contador+'</td>'
                    + '<td>'
                    + '<img src="/soportes/' +nombredoc+ '" style="width:20px;"/>'
                    + '</td>'
                    + '<td class="text-truncate" style="max-width:100px">' + nombredoc + '</td>'
                    + '<td>' + tamañodoc + ' Kb</td>'
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
    */

})
