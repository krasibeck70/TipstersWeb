﻿$(".btn-follow").click(function () {
    var user_id = $(this).data("follow");
    console.log(user_id);
    var text = this.innerHTML;
    if (text == "Follow") {
        this.innerHTML = "Followed";
    }
    else if (text == "Followed") {
        this.innerHTML = "Follow";
    }
    var url = "/Users/FollowUser/";
    $.ajax({
        type: "POST",
        url: url + user_id
    }
    );
})