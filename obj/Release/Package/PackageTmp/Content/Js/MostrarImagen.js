
    document.addEventListener("DOMContentLoaded", function () {
        document.querySelector('input[name="ImagenFile"]').addEventListener("change", function (event) {
            var input = event.target;
            var reader = new FileReader();

            reader.onload = function () {
                var img = document.getElementById("preview");
                img.src = reader.result;
                img.style.display = "block"; // Muestra la imagen cuando se selecciona
            };

            if (input.files && input.files[0]) {
                reader.readAsDataURL(input.files[0]);
            }
        });
    });

