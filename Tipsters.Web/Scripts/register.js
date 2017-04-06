$("#datetimepicker").datepicker({
    format: 'mm-dd-yyyy'
});

$('#Email_js').on('keyup',
    function () {
        var email = $(this).val();
        var regex = /^(([^<>()\[\]\\.,;:\&@@"]+(\.[^<>()\[\]\\.,;:\s@@"]+)*)|(".+"))@@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (regex.test(email)) {
            $('#message3').removeClass("glyphicon glyphicon-remove").addClass('glyphicon glyphicon-ok').html('').css('color', 'green');
        } else $('#message3').removeClass("glyphicon glyphicon-ok").addClass('glyphicon glyphicon-remove').html('').css('color', 'red');
        //$.ajax({var regex
        //    type: "GET",
        //    dataType: "json",
        //    url: "/Users/AllEmails",
        //    success: function (data) {
        //        var emails = JSON.stringify(data);
        //        var email2 = emails.split(',');
        //        for (var i = 0; i < email2.length; i++) {
        //            var current = email2[i].split('"')[1];
        //            if (!(email === current)) {
        //                $('#message3').html('').css('color', 'green');
        //            } else $('#message3').html('').css('color', 'red');
        //        }
        //    }
        //});

    });
$('#ConfirmPassword_js').on('keyup',
    function () {
        if ($(this).val() == $('#Password_js').val()) {
            $('#message').removeClass("glyphicon glyphicon-remove").addClass('glyphicon glyphicon-ok').html('').css('color', 'green');
        } else $('#message').removeClass("glyphicon glyphicon-ok").addClass('glyphicon glyphicon-remove').html('').css('color', 'red');
    });
$('#Password_js').on('keyup',
    function () {

        var regex = /^(?=.*[0-9])(?=.*[!#$%^&*])[a-zA-Z0-9!#$%^&*]{6,16}$/;
        if (regex.test($(this).val())) {
            $('#message2').removeClass("glyphicon glyphicon-remove").addClass('glyphicon glyphicon-ok').html('').css('color', 'green');
        } else $('#message2').removeClass("glyphicon glyphicon-ok").addClass('glyphicon glyphicon-remove').html('').css('color', 'red');
    });
