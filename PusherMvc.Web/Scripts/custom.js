function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

if (getParameterByName("debug").length > 0) {
    Pusher.log = function(message) {
        var msg = document.createTextNode(message);
        var el = document.createElement("div");
        el.className = "log-msg";
        el.appendChild(msg);
        document.body.appendChild(el);
    };
}

jQuery.noConflict();
(function ($) {
    $(function () {

        var configurePusher = function () {
            var productId = $("#productId").val();

            if (productId.length > 0) {
                var pusher = new Pusher("76ad3a948291ca99d5d7");

                var channel = pusher.subscribe("product-" + productId);

                channel.bind("StockUpdated", function (product) {
                    $("#quantity").html(product.StockLevel);
                    $("#status").html(product.StockStatus);
                });
            }
        };

        var triggerBuy = function() {
            var productId = $("#productId").val();

            if (productId.length > 0) {
                $.ajax({
                    type: "POST",
                    url: "/Store/UpdateStock",
                    data: {
                        productId: productId
                    }
                    }
                );
            }
        };
        configurePusher();
        $('#BuyButton').on("click", triggerBuy);
    });
})(jQuery);