// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*
function RegNoCheck() {
    $('#RegNoStatus').removeClass('badge-success').removeClass('badge-danger').addClass('badge-info')
    $('#RegNoStatus').html('Checking availability...')

    $.post("/ParkedVehicles/CheckIfRegNoExists",
        {
            regNo: $("#RegNo").val()
        },
        function (data) {
            if (data == 1) {
                $('#RegNoStatus').removeClass('badge-info').addClass('badge-danger')
                $('#RegNoStatus').html('Reg-number already exists')
            }
            else if (data == 0) {
                $('#RegNoStatus').removeClass('badge-info').addClass('badge-success')
                $('#RegNoStatus').html('Reg-number available')
            }
        }
    )
}
*/