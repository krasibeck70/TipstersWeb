var localTeam;
var awayTeam;

function LocalTeam() {

    var result = document.getElementById('selectLocalTeam');
    localTeam = result.options[result.selectedIndex].value;
}

function AwayTeam() {

    var result = document.getElementById('selectAwayTeam');
    var value = result.options[result.selectedIndex].value;
    if (localTeam === value) {
        alert("A team can't play against itself! Please change team!");
        document.getElementById("selectAwayTeam").selectedIndex = 0;
    }
}

$(function () {
    $("#datetimepicker").on("change", function () {
        var result = $(this).data('date');
        var now = new Date();
        var nowResult = new Date(result);
        if (now > nowResult) {
            alert("You don`t live in the past! Please change date!");
        } 
    });
});

$('#StartMacth').on('keyup',
    function() {
        var number = $(this).val();
        var regex = /^[0-9]*([.][0-9]{1,3})?$/;
        if (regex.test(number)) {
            $('#message3').removeClass("glyphicon glyphicon-remove").addClass('glyphicon glyphicon-ok').html('')
                .css('color', 'green');
        } else
            $('#message3').removeClass("glyphicon glyphicon-ok").addClass('glyphicon glyphicon-remove').html('')
                .css('color', 'red');
    });
$('#chance').on('keyup',
    function () {
        var number = $(this).val();
        var regex = /^[0-9]/;
        if (regex.test(number)) {
            $('#message4').removeClass("glyphicon glyphicon-remove").addClass('glyphicon glyphicon-ok').html('')
                .css('color', 'green');
        } else
            $('#message4').removeClass("glyphicon glyphicon-ok").addClass('glyphicon glyphicon-remove').html('')
                .css('color', 'red');
    });

