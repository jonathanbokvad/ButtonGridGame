// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function doButtonUpdate(buttonNumber) {
    $.ajax({
        datatype: "json",
        method: 'POST',
        url: '/button/ShowOneButton',
        data: { "buttonNumber": buttonNumber },
        success: function (data) {

            console.log(data);
            $("#" + buttonNumber).html(data.part1);
            $("#messageArea").html(data.part2);
        }
    })
}