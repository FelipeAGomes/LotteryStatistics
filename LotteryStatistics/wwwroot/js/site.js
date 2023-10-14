// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function generateRandomNumbers() {
    var count = 6;
    $.ajax({
        url: '/Numbers/GenerateRandomNumbers',
        type: 'GET',
        data: { count: count },
        success: function (data) {
            // Ordenar os números em ordem crescente
            data.sort(function (a, b) {
                return a - b;
            });

            // Exibir os números ordenados nos inputs
            for (var i = 0; i < data.length; i++) {
                document.getElementById('randomNumber' + (i + 1)).value = data[i];
            }
        },
        error: function () {
            alert('Failed to generate random numbers.');
        }
    });
}
