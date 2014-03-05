function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

if (typeof getParameterByName("debug") !== "undefined") {
    Pusher.log = function(message) {
        var msg = document.createTextNode(message);
        var el = document.createElement("div");
        el.className = "log-msg";
        el.appendChild(msg);
        document.body.appendChild(el);
    };
}

//jQuery.noConflict();
(function ($) {
    $(function () {

        var subscribeToProductDetailsChannel = function () {
            var productId = $("#productId").val();

            if (typeof productId !== "undefined") {
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

            if (typeof productId !== "undefined") {
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

        var loginToChatroom = function() {
            var $this = $(this);
            var loader = $('#chat_widget_login_loader');
            var errorMessage = $('#chat_widget_login_error');

            $this.hide();
            loader.show();
            errorMessage.hide();

            var username = $('#chat_widget_username').val().replace(/[^a-z0-9]/gi, '');
            
            if (username.length > 0) {
                subscribeToChannel();
            } else {
                errorMessage.show();
                loader.hide();
                $this.show();
            }
        };

        var subscribeToChannel = function() {
            var pusher = new Pusher("76ad3a948291ca99d5d7");
            pusher.channel_auth_endpoint = "/chat/Auth";
            pusher.subscribe('presence-pushermvcchat');
        };

        subscribeToProductDetailsChannel();
        $('#BuyButton').on("click", triggerBuy);
        $('#chat_widget_login_button').on('click', loginToChatroom);
    });
})(jQuery);