
$('.btn-vote').click(function () {
    var tip_id = $(this).data("tip");
    var tip_id_button = "";
    var url = "/Tips/VotesTips/";
    var parameter = "";
    
    if ($(this).hasClass('Up')) {

        tip_id_button = this.id;
        parameter = 'Up';
    } else {

        tip_id_button = this.id;
        parameter = 'Down';
    }

    $.ajax({
        type: "POST",
        dataType: "json",
        url: url + tip_id + "¿" + parameter,
        success: function (data) {
            console.log(tip_id);
            var result = JSON.parse(data);
            if (parameter === "Up") {
                document.getElementById(tip_id_button).innerHTML = "&nbsp;&nbsp;" + result["VotesUp_json"];
            } else {
                if (parameter === "Down") {
                    document.getElementById(tip_id_button).innerHTML = "&nbsp;&nbsp;" + result["VotesDown_json"];
                }
            }
            $("#button_up_ajax-" + tip_id).addClass("disabled");
            $("#button_down_ajax-" + tip_id).addClass("disabled");
            $("#button_up_ajax-" + tip_id).removeClass("Up").removeClass("btn-vote").removeAttr("id").removeAttr("data-tip").removeAttr("data-val").removeAttr("type");
            $("#button_down_ajax-" + tip_id).removeClass("Down").removeClass("btn-vote").removeAttr("id").removeAttr("data-tip").removeAttr("data-val2").removeAttr("type");
            $("progress_bar_" + tip_id).on(function () {
                $(this).attr('aria-valuenow').val = result["Percentage"];
            });
            var divArray = document.getElementById('progress_bar_' + tip_id);
            divArray.style.width = result["Percentage"] + "%";
            document.getElementById('progress_bar_' + tip_id).innerHTML = result["Percentage"] + "%";
        }
    });
});

$(".btn-show").click(function () {
    var tip_id = this.id;
    console.log(tip_id);
    if ($("#comments-" + tip_id).hasClass("hidden")) {
        $("#comments-" + tip_id).removeClass("hidden");
        $("#comments-" + tip_id).addClass("show");

    } else {
        $("#comments-" + tip_id).removeClass("show");
        $("#comments-" + tip_id).addClass("hidden");
    }

});
$(".btn-send").click(function () {
    var tip_id = this.id;
    var comment = document.getElementById('comment-' + tip_id).value;


    $("#form-" + tip_id).ajaxForm({
        dataType: "json",
        url: "/Tips/PostComment/" + tip_id,
        success: function (data) {
            document.getElementById('comment-' + tip_id).value = "";
            var result = JSON.parse(data);
            if ($("#Last_Comment-" + result["PronosticId"]).hasClass("hidden")) {
                $("#Last_Comment-" + result["PronosticId"]).removeClass("hidden");
                $("#Last_Comment-" + result["PronosticId"]).addClass("show");
            } else {
                $("#Last_Comment-" + result["PronosticId"]).removeClass("show");
                $("#Last_Comment-" + result["PronosticId"]).addClass("hidden");
            }

            $.ajax({
                type: "GET",
                dataType: "json",
                url: "/Users/FindUserById/" + result["UserId"],
                success: function (data) {
                    var user_post = JSON.parse(data);

                    document.getElementById("Message_ajax-" + result["PronosticId"]).innerHTML = result["Message"];
                    document.getElementById("TimePosted_ajax-" + result["PronosticId"]).innerHTML = result["TimePosted"];
                    document.getElementById("Email_ajax-" + result["PronosticId"]).innerHTML = user_post["Email"];
                    document.getElementById("Image_ajax-" + result["PronosticId"]).src = "/../Content/Images/" + user_post["Image"];
                    document.getElementById(result["PronosticId"]).innerHTML = "Show Comments" + " " + result["TotalComments"];
                }
            });
        }
    });
});

$(".btn-orderby").click(function () {
    if ($(".btns-order").hasClass("hidden")) {
        $(".btns-order").removeClass("hidden");
    } else {
        $(".btns-order").addClass("hidden");
    }
});
$(".btn-orderPercent").click(function () {
    $(".resultSorted").load("/Tips/GetOrderBy/Percentage");
});
$(".btn-orderCoefficient").click(function () {
    $(".resultSorted").load("/Tips/GetOrderBy/Coefficient");
});
$(".btn-orderStartMatch").click(function () {
    $(".resultSorted").load("/Tips/GetOrderBy/StartMatch");
});
