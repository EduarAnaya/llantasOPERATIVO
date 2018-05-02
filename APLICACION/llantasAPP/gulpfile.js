var gulp = require("gulp");
var uglify = require("gulp-uglify"); //minimizar archivps

gulp.task("test", function() {
  //aciones a realizar en la tarea
  console.log("Se ejecuto la tarea 1");
});
//tarea que permite minidicar archivos y ponerlos en producción
gulp.task("compressAll", function() {
  /*js_Edit
  /js_Edit_Llantas*/
  gulp
    .src("./Scripts_desarrollo/js_Edit/js_Edit_Llantas/js_SearchsLlantas.js")
    .pipe(uglify())
    .pipe(gulp.dest("./llantasAPP/Scripts/js_Edit/js_Edit_Llantas"));
  gulp
    .src("./Scripts_desarrollo/js_Edit/js_Edit_Llantas/js_llantas_Edit.js")
    .pipe(uglify())
    .pipe(gulp.dest("./llantasAPP/Scripts/js_Edit/js_Edit_Llantas"));
  gulp
    .src("./Scripts_desarrollo/js_Edit/js_Edit_Llantas/js_cargaArchivos.js")
    .pipe(uglify())
    .pipe(gulp.dest("./llantasAPP/Scripts/js_Edit/js_Edit_Llantas"));
  gulp
    .src("./Scripts_desarrollo/js_Edit/js_Edit_Llantas/js_llantas_Remove.js")
    .pipe(uglify())
    .pipe(gulp.dest("./llantasAPP/Scripts/js_Edit/js_Edit_Llantas"));
  /*js_Edit
  /js_timer*/
  gulp
    .src("./Scripts_desarrollo/js_Edit/js_timer.Js")
    .pipe(uglify())
    .pipe(gulp.dest("./llantasAPP/Scripts/js_Edit/"));
    console.log("Archivos Modificados");
    
});

gulp.task("observador", function() {
  gulp.watch("./Scripts_desarrollo/js_Edit/*.Js", ["compressAll"]);
  gulp.watch("./Scripts_desarrollo/js_Edit/js_Edit_Llantas/*.js", ["compressAll"]);
  console.log("Observando");  
});
