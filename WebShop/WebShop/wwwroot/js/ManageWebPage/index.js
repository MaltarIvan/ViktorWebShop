function deleteCartsAndOrders() {
    $.ajax({
        type: 'GET',
        url: 'ManageWebPage/DeleteUnusedShoppingCartsAndOrders',
        success: function (data) {
            console.log(data);
            alert(data + " unfinished orders and carts deleted!");
        }
    });
}