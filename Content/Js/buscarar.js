function filtrarTabla() {
    let input = document.getElementById("buscador").value.toLowerCase();
    let tabla = document.querySelector("table");
    let filas = tabla.getElementsByTagName("tr");

    for (let i = 1; i < filas.length; i++) { // Saltamos la primera fila (encabezados)
        let celdas = filas[i].getElementsByTagName("td");
        let mostrarFila = false;

        for (let j = 0; j < celdas.length; j++) { // Recorremos todas las columnas
            if (celdas[j].innerText.toLowerCase().includes(input)) {
                mostrarFila = true;
                break; // Si encuentra coincidencia en una celda, no sigue buscando
            }
        }

        filas[i].style.display = mostrarFila ? "" : "none";
    }
}