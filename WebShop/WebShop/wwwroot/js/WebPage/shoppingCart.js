function addProduct(productID, cartItemID) {
    $.ajax({
        type: 'POST',
        url: 'AddProductToCart',
        data: {
            'productID': productID
        },
        success: function (result) {
            $("#" + cartItemID + "quantity").text(result.productCount);
            $("#total-price").text(result.totalPrice);
            $("#" + cartItemID + "cart-item-price").text(result.cartItemPrice);
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
            $("#" + cartItemID + "quantity").text(result.productCount);
            $("#total-price").text(result.totalPrice);
            $("#" + cartItemID + "cart-item-price").text(result.cartItemPrice);
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
        }
    });
}