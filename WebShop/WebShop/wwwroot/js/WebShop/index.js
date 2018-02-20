function addProduct(productID) {
    $.ajax({
        type: 'POST',
        url: 'WebShop/AddProductToCart',
        data: {
            'productID': productID
        },
        success: function (productCount) {
            console.log(productCount);
            $("#" + productID + "is-in-cart").show();
        }
    });
}