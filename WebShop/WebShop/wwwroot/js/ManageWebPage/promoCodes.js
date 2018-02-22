function deletePromoCode(promoCodeID) {
    $.ajax({
        type: 'POST',
        url: 'DeletePromoCode',
        data: {
            'promoCodeID': promoCodeID
        },
        success: function (productID) {
            $("#" + productID + "table-row").remove();
        }
    });
}