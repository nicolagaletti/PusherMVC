jQuery.noConflict();
(function ($) {
    $(function () {

        var configurePusher = function () {
            var productId = $("#productId").val;

            if (productId.length > 0) {
                var pusher = new Pusher("76ad3a948291ca99d5d7");

                var channel = pusher.subscribe("product-" + productId);

                channel.bind("stockUpdated", function (product) {
                    $("#quantity").html(product.StockLevel);
                    $("#status").html(product.StockStatus);
                });

                alert("subscribed!");
            }
        };

        configurePusher();
        // by passing the $ you can code using the $ alias for jQuery
        //alert('Page: ' + $('title').html() + ' dom loaded!');
    });
})(jQuery);