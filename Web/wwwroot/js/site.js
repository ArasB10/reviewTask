// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function() {

    $('#suggest-and-park').click(function(e) {
        e.preventDefault();
        //var parkingLotId = $('#Id').val();
        var licensePlate = $('#input-license-plate').val();
        //var floorNumber = $('#input-boom-barrier-floors').val();
        var floorNumber = $("#floorSelect option:selected").val();
        $.ajax('ParkingLot/SuggestFloor',
            {
                cache: false,
                method: 'POST',
                data: {
                    //parkingLotId: parkingLotId,
                    licensePlateNumber: licensePlate,
                    entranceFloor: floorNumber
                },
                success: function (response) {
                    if (response.isSuccess) {
                        $('#Suggestion').show().html('Suggested parking floor: ' + response.suggestedFloor.floorNumber).fadeOut(5000);
                        ParkCar(response.suggestedFloor.id, licensePlate);
                    }
                    else {
                        $('#Errors').show().html(response.errors.join('. ')).fadeOut(5000);
                    }
                }
            });
    });
    
    function ParkCar(floorId, licensePlate) {
        $.ajax('ParkingLot/ParkCar',
            {
                cache: false,
                method: 'POST',
                data: {
                    licensePlateNumber: licensePlate,
                    floorId: floorId
                },
                success: function (response) {
                    RefreshData();
                }
            });
    }


    function RefreshData() {
        $.ajax('ParkingLot/Data',
            {
                cache: false,
                method: 'GET',
                success: function (response) {
                    $('#data-wrapper').html(response);
                }
            });
    }
});
