function descargarDatos(format) {
    // Ocultar botones de edición, detalle y eliminación
    const buttons = document.querySelectorAll('.btn-warning, .btn-primary, .btn-danger'); // Asegúrate de que estas clases coincidan con tus botones
    buttons.forEach(button => button.style.display = 'none');

    // Obtener la tabla
    var tabla = document.getElementById("tablaArticulos");
    var filas = tabla.getElementsByTagName("tr");
    var csv = [];
    var headers = [];

    // Obtener los nombres de las columnas
    var headerCells = filas[0].getElementsByTagName("th");
    for (var j = 0; j < headerCells.length; j++) {
        headers.push(headerCells[j].innerText);
    }
    csv.push(headers.join(",")); // Agregar encabezados al CSV

    // Recorrer las filas de la tabla
    for (var i = 1; i < filas.length; i++) { // Comenzar desde 1 para omitir el encabezado
        var fila = filas[i];

        // Verificar si la fila es visible
        if (fila.style.display !== 'none') {
            var celdas = fila.getElementsByTagName("td");
            var filaDatos = [];

            // Recorrer las celdas de la fila
            for (var j = 0; j < celdas.length - 1; j++) { // -1 para omitir la última celda (botones)
                filaDatos.push(celdas[j].innerText); // Obtener el texto de la celda
            }

            // Agregar la fila al CSV
            csv.push(filaDatos.join(",")); // Unir las celdas con una coma
        }
    }

    // Crear un archivo según el formato seleccionado
    switch (format) {
        case 'csv':
            descargarCSV(csv);
            break;
        case 'excel':
            descargarExcel(csv);
            break;
        case 'sql':
            descargarSQL(csv);
            break;
        default:
            console.error('Formato no soportado');
    }

    // Mostrar nuevamente los botones
    buttons.forEach(button => button.style.display = 'block');
}

function descargarCSV(csv) {
    // Crear un archivo CSV
    var csvString = csv.join("\n"); // Unir las filas con un salto de línea
    var blob = new Blob([csvString], { type: "text/csv;charset=utf-8;" });
    var url = URL.createObjectURL(blob);

    // Crear un enlace para descargar el archivo
    var link = document.createElement("a");
    link.setAttribute("href", url);
    link.setAttribute("download", "datos_articulos.csv"); // Nombre del archivo
    document.body.appendChild(link);
    link.click(); // Simular clic para descargar
    document.body.removeChild(link); // Limpiar el DOM
}

function descargarExcel(csv) {
    // Crear un archivo Excel (XLSX) usando SheetJS
    var wb = XLSX.utils.book_new();
    var ws = XLSX.utils.aoa_to_sheet(csv.map(row => row.split(",")));
    XLSX.utils.book_append_sheet(wb, ws, "Artículos");
    XLSX.writeFile(wb, "datos_articulos.xlsx");
}

function descargarSQL(csv) {
    // Crear un archivo SQL
    var sqlString = "INSERT INTO articulos (Id, Nombre, PrecioVenta, Usuario, TipoArticulo, Categoria, Imagen, Material, Talla, Existencia) VALUES\n";
    for (var i = 1; i < csv.length; i++) { // Comenzar desde 1 para omitir el encabezado
        var values = csv[i].split(",");
        sqlString += `(${values.join(", ")})${i < csv.length - 1 ? "," : ";"}`; // Agregar coma si no es la última fila
    }

    var blob = new Blob([sqlString], { type: "text/plain;charset=utf-8;" });
    var url = URL.createObjectURL(blob);

    // Crear un enlace para descargar el archivo
    var link = document.createElement("a");
    link.setAttribute("href", url);
    link.setAttribute("download", "datos_articulos.sql"); // Nombre del archivo
    document.body.appendChild(link);
    link.click(); // Simular clic para descargar
    document.body.removeChild(link); // Limpiar el DOM
}