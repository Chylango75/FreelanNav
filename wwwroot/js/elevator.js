function reqFromElevator(floor) {
    var _tmp = $('#txtReqsElevator').val();
    $('#txtReqsElevator').val(floor + ',' + _tmp);
    $('#txtReqsAll').val($('#txtReqsAll').val() + ',' + floor)    //  save history
}

function reqFromBuilding(floor) {
    var _tmp = $('#txtReqsBuilding').val();
    $('#txtReqsBuilding').val(_tmp + ',' + floor)
    $('#txtReqsAll').val($('#txtReqsAll').val() + ',' + floor)  //  also save history
}

function Go() {
    var worklift = {
        StrReqsBuilding: $('#txtReqsBuilding').val(),
        StrReqsElevator: $('#txtReqsElevator').val(),
        StrNextFloors: $('#txtNextFloors').val(),
        CurrentFloor: $('#txtCurrentFloor').val(),
        MovingUp: $('#txtMovingUp').val(),
        OpenDoors: $('#txtOpenDoors').val(),
    }

    // Send data to process
    $.ajax({
        type: "post",
        url: "/Elevator/Go",
        data: { worklift: worklift },
        success: function (data) {

            if (data != null) {
                $('#txtCurrentFloor').val(data.currentFloor);
                $('#txtMovingUp').val(data.movingUp);
                $('#txtOpenDoors').val(data.openDoors);
                $('#txtNextFloors').val(data.strNextFloors);
                $('#txtReqsBuilding').val(data.StrReqsBuilding);
                $('#txtReqsElevator').val(data.StrReqsElevator);
            }
        }
    })

    DrawFloor();
}

function DrawFloor() {
    let _openDoors = $('#txtOpenDoors').val() == "" ? false : $('#txtOpenDoors').val();
    let nextFloors = $('#txtNextFloors').val();

    $.ajax({
        type: "post",
        url: "/Elevator/DrawBuilding?"
            + "piso=" + $('#txtCurrentFloor').val()
            + '&OpenDoors=' + _openDoors
            + '&nextFloors=' + nextFloors,
        success: function (data) {
            $("#partialBuilding").html(data);
        }
    })
}

//////////////////    Funtions to control process       ////////////////////////////////

const speed = 1000;         //  elevator speed
_moving = setInterval(Go, speed);    // Start moving elevator

function elevatorMove() {
    console.log("Launching");
    this._moving = setInterval(Go, speed);
}

function elevatorStop() {
    console.log("Deslaunching");
    clearInterval(this._moving);
}
