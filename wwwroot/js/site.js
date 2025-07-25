


function IniPartial() {
    $.ajax({
        type: "post",
        url: "/Rentas/GetPartialInquilino",
        success: function (data) {
            $('#workArea').html(data);
        }
    })
}

IniPartial();



//////     xTRA  functions

function isEmptyOrNull(str) {
    return !str; // Returns true if str is null, undefined, or an empty string ("")
}