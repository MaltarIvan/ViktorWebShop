function setAsDelivered(orderID) {
    $.ajax({
        type: 'POST',
        url: 'SetOrderAsDelivered',
        data: {
            'orderID': orderID
        },
        success: function (orderID) {
            $("#" + orderID + "order").remove();
        }
    });
}

function deleteOrder(orderID) {
    $.ajax({
        type: 'POST',
        url: 'DeleteOrder',
        data: {
            'orderID': orderID
        },
        success: function (orderID) {
            $("#" + orderID + "order").remove();
        }
    });
}