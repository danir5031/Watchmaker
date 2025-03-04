document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("buscadorImagen").addEventListener("change", function (event) {
        var input = event.target;
        var reader = new FileReader();

        reader.onload = function (e) {
            var img = document.getElementById("previewBuscar");
            img.src = e.target.result;
            img.style.display = "block";

            compararImagenConArticulos(img);
        };

        if (input.files && input.files[0]) {
            reader.readAsDataURL(input.files[0]);
        }
    });
});

// Función para comparar la imagen seleccionada con las imágenes de los artículos
function compararImagenConArticulos(imagenReferencia) {
    var canvasRef = document.createElement("canvas");
    var ctxRef = canvasRef.getContext("2d");
    var imgRef = new Image();
    imgRef.src = imagenReferencia.src;

    imgRef.onload = function () {
        canvasRef.width = 50;
        canvasRef.height = 50;
        ctxRef.drawImage(imgRef, 0, 0, 50, 50);

        var dataRef = ctxRef.getImageData(0, 0, 50, 50).data;
        var colorRef = calcularColorPromedio(dataRef);

        var filas = document.querySelectorAll("#detalle-articulos table tr");

        filas.forEach((fila, index) => {
            if (index === 0) return; // Saltar encabezado

            var imgTag = fila.querySelector("td img");
            if (imgTag) {
                var canvasComp = document.createElement("canvas");
                var ctxComp = canvasComp.getContext("2d");
                var imgComp = new Image();
                imgComp.src = imgTag.src;

                imgComp.onload = function () {
                    canvasComp.width = 50;
                    canvasComp.height = 50;
                    ctxComp.drawImage(imgComp, 0, 0, 50, 50);

                    var dataComp = ctxComp.getImageData(0, 0, 50, 50).data;
                    var colorComp = calcularColorPromedio(dataComp);

                    var diferencia = calcularDiferencia(colorRef, colorComp);
                    if (diferencia < 50) { // Umbral de similitud
                        fila.style.display = "";
                    } else {
                        fila.style.display = "none";
                    }
                };
            }
        });
    };
}

// Calcular el color promedio de una imagen
function calcularColorPromedio(data) {
    var r = 0, g = 0, b = 0, count = 0;
    for (var i = 0; i < data.length; i += 4) {
        r += data[i];     // Rojo
        g += data[i + 1]; // Verde
        b += data[i + 2]; // Azul
        count++;
    }
    return { r: r / count, g: g / count, b: b / count };
}

// Calcular diferencia entre dos colores promedio
function calcularDiferencia(color1, color2) {
    return Math.sqrt(
        Math.pow(color1.r - color2.r, 2) +
        Math.pow(color1.g - color2.g, 2) +
        Math.pow(color1.b - color2.b, 2)
    );
}

// Restablecer filtro
function resetearFiltro() {
    var filas = document.querySelectorAll("#detalle-articulos table tr");
    filas.forEach(fila => fila.style.display = "");

    document.getElementById("previewBuscar").style.display = "none";
    document.getElementById("buscadorImagen").value = "";
}
