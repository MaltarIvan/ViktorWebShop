function addProduct(productID) {
    $.ajax({
        type: 'POST',
        url: 'WebShop/AddProductToCart',
        data: {
            'productID': productID
        },
        success: function (result) {
            if (result.productCount === 1) {
                $("#" + productID + "is-in-cart").show();
            }
            var count = $("#number-of-items-nav").text();
            count++;
            $("#number-of-items-nav").text(count);
            $("#number-of-items").text(count);
            $("#shop-nav-bar-item").css("color", "red");
        }
    });
}

$(function () {
    var url = $("#action-holder").data('request-url');
    $.ajax({
        type: 'GET',
        url: url,
        success: function (data) {
            console.log(data);
            $("#number-of-items").text(data);
            if (data > 0) {
                $("#shop-nav-bar-item").css("color", "red");
            }
        }
    });
});