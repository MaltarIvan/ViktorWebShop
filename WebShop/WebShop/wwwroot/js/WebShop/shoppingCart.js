function addProduct(productID, cartItemID) {
    $.ajax({
        type: 'POST',
        url: 'AddProductToCart',
        data: {
            'productID': productID
        },
        success: function (result) {
            $("#" + cartItemID + "quantity").text(result.productCount);
            $("#total-price").text(result.totalPrice.toFixed(2) + " kn");
            $("#" + cartItemID + "cart-item-price").text("Ukupno: " + result.cartItemPrice.toFixed(2) + " kn");
            $("#number-of-items-nav").text(result.numberOfCartItems);
            if (result.numberOfCartItems > 0) {
                $("#shop-nav-bar-item").css("color", "red");
            }
        }
    });
}

function removeProduct(cartItemID) {
    $.ajax({
        type: 'POST',
        url: 'RemoveProductFromCart',
        data: {
            'cartItemID': cartItemID
        },
        success: function (result) {
            if (result.productCount === 0) {
                $("#" + cartItemID).remove();
            }
            var count = $(".cart-item").length;
            if (count <= 0) {
                $("#cart-details").hide();
                $("#cart-empty").show();
            }
            $("#" + cartItemID + "quantity").text(result.productCount);
            $("#total-price").text(result.totalPrice.toFixed(2) + " kn");
            $("#" + cartItemID + "cart-item-price").text("Ukupno " + result.cartItemPrice.toFixed(2) + " kn");
            $("#number-of-items-nav").text(result.numberOfCartItems);
            if (result.numberOfCartItems === 0) {
                $("#shop-nav-bar-item").css("color", "#9d9d9d");
            }
        }
    });
}

function deleteProduct(cartItemID) {
    $.ajax({
        type: 'POST',
        url: 'DeleteProductFromCart',
        data: {
            'cartItemID': cartItemID
        },
        success: function (result) {
            $("#" + cartItemID).remove();
            $("#total-price").text(result.totalPrice.toFixed(2) + " kn");
            var count = $(".cart-item").length;
            if (count <= 0) {
                $("#cart-details").hide();
                $("#cart-empty").show();
            }
            $("#number-of-items-nav").text(result.numberOfCartItems);
            if (result.numberOfCartItems === 0) {
                $("#shop-nav-bar-item").css("color", "#9d9d9d");
            }
        }
    });
}