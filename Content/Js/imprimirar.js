function imprimirTabla() {
    console.log("Botón de impresión de la tabla clickeado");

    var contenido = document.getElementById("detalle-articulos");

    if (!contenido) {
        console.error("No se encontró el elemento con id 'detalle-articulos'. Verifica el HTML.");
        return;
    }

    var contenidoClonado = contenido.cloneNode(true);

    // Convertir las imágenes a base64 antes de imprimir
    var imagenes = contenidoClonado.getElementsByTagName("img");
    var totalImagenes = imagenes.length;
    var imagenesConvertidas = 0;

    for (let i = 0; i < totalImagenes; i++) {
        let img = imagenes[i];
        let canvas = document.createElement("canvas");
        let ctx = canvas.getContext("2d");
        let nuevaImagen = new Image();
        nuevaImagen.crossOrigin = "anonymous"; // Para evitar problemas con imágenes externas
        nuevaImagen.src = img.src;

        nuevaImagen.onload = function () {
            canvas.width = nuevaImagen.width;
            canvas.height = nuevaImagen.height;
            ctx.drawImage(nuevaImagen, 0, 0);
            let dataURL = canvas.toDataURL("image/png"); // Convertir imagen a base64
            img.src = dataURL;

            imagenesConvertidas++;

            // Solo imprimir cuando todas las imágenes estén listas
            if (imagenesConvertidas === totalImagenes) {
                generarVistaImpresion(contenidoClonado);
            }
        };

        nuevaImagen.onerror = function () {
            console.warn("No se pudo convertir la imagen:", img.src);
            imagenesConvertidas++;
            if (imagenesConvertidas === totalImagenes) {
                generarVistaImpresion(contenidoClonado);
            }
        };
    }

    if (totalImagenes === 0) {
        generarVistaImpresion(contenidoClonado);
    }
}

function generarVistaImpresion(contenidoClonado) {
    var filas = contenidoClonado.getElementsByTagName("tr");
    for (var i = 0; i < filas.length; i++) {
        var celdas = filas[i].getElementsByTagName("td");
        if (celdas.length > 0) {
            filas[i].removeChild(celdas[celdas.length - 1]);
        }

        var encabezados = filas[i].getElementsByTagName("th");
        if (encabezados.length > 0) {
            filas[i].removeChild(encabezados[encabezados.length - 1]);
        }
    }

    var ventanaImpresion = window.open('', '', 'width=800,height=600');
    ventanaImpresion.document.write('<html><head><title>Bodega de Artículos</title>');
    ventanaImpresion.document.write('<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">');
    ventanaImpresion.document.write('</head><body>');
    ventanaImpresion.document.write('<h2>Bodega de Artículos</h2>');
    ventanaImpresion.document.write(contenidoClonado.innerHTML);
    ventanaImpresion.document.write('</body></html>');
    ventanaImpresion.document.close();

    ventanaImpresion.print();
}
