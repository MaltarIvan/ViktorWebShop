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

window.fbAsyncInit = function () {
    FB.init({
        appId: 'your-app-id',
        autoLogAppEvents: true,
        xfbml: true,
        version: 'v2.12'
    });
};
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement(s); js.id = id;
    js.src = "https://connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

/*
    <div class="fb-customerchat"
        page_id="316515465174039"
        theme_color="#13cf13"
    >
    </div>
*/