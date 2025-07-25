function GetPartialInquilino() {
    $.ajax({
        type: "post",
        url: "/Rentas/GetPartialInquilino",
        success: function (data) {
            $('#workArea').html(data);
        }
    })
}

function GetPartialContrato() {
    $.ajax({
        type: "post",
        url: "/Rentas/GetPartialContrato",
        success: function (data) {
            $('#workArea').html(data);
        }
    })
}

function GetPartialPago() {
    $.ajax({
        type: "post",
        url: "/Rentas/GetPartialPago",
        success: function (data) {
            $('#workArea').html(data);
        }
    })
}

function GetPartialReportes() {
    num = $('#numIden').val()

    $.ajax({
        type: "post",
        url: "/Rentas/GetPartialReportes",
        data: { iden: num },
        success: function (data) {
            $('#workArea').html(data);
        }
    })
}
