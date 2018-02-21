$(function () {
    var url = $("#action-holder").data('request-url');
    $.ajax({
        type: 'GET',
        url: url,
        success: function (data) {
            console.log(data);
            $("#number-of-items-nav").text(data);
            if (data > 0) {
                $("#shop-nav-bar-item").css("color", "red");
            }
        }
    });
});
