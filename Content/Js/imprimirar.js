function imprimirTabla() {
    console.log("Botón de impresión de la tabla clickeado");

    // Buscar el div con el ID 'detalle-articulos'
    var contenido = document.getElementById("detalle-articulos");

    if (!contenido) {
        console.error("No se encontró el elemento con id 'detalle-articulos'. Verifica el HTML.");
        return;
    }

    // Clonar el contenido para no afectar la tabla original
    var contenidoClonado = contenido.cloneNode(true);

    // Eliminar la última columna de cada fila (la que contiene los botones)
    var filas = contenidoClonado.getElementsByTagName("tr");
    for (var i = 0; i < filas.length; i++) {
        var celdas = filas[i].getElementsByTagName("td");
        if (celdas.length > 0) {
            filas[i].removeChild(celdas[celdas.length - 1]); // Elimina la última celda de cada fila
        }

        var encabezados = filas[i].getElementsByTagName("th");
        if (encabezados.length > 0) {
            filas[i].removeChild(encabezados[encabezados.length - 1]); // Elimina la última celda de los encabezados
        }
    }

    // Crear ventana de impresión
    var ventanaImpresion = window.open('', '', 'width=800,height=600');
    ventanaImpresion.document.write('<html><head><title>Bodega de Artículos</title>');
    ventanaImpresion.document.write('<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">'); // Mantener el estilo
    ventanaImpresion.document.write('</head><body>');
    ventanaImpresion.document.write('<h2>Bodega de Artículos</h2>');
    ventanaImpresion.document.write(contenidoClonado.innerHTML); // Usamos la tabla clonada sin botones
    ventanaImpresion.document.write('</body></html>');
    ventanaImpresion.document.close();

    // Iniciar impresión
    ventanaImpresion.print();
}
