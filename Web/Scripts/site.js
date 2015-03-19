$(document).ready(function() {
    var cartUrl = window.PhoneCat.siteName + "/api/cart";
    $.getJSON(cartUrl, function (cart) {
        if (cart.case === "Empty")
            updateCartCount(0);
        else {
            updateCartCount(cart.fields[0].length);
        }
    });

    function updateCartCount(count) {
        $("#cartItemCount").text(count);
    }

    $("#addToCart").click(function() {
        var itemId = $(this).data('item-id');
        $.ajax({
            type: "POST",
            url: cartUrl + "/add",
            data: '"' + itemId + '"',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(cart) {
                updateCartCount(cart.fields[0].length);
            },
            failure: function (errMsg) {
                alert(errMsg);
            }
        });
    });
});