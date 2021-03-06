﻿function getParameterByName(name) {
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

        var ajaxCall = function (ajaxUrl, ajaxData, callback) {
            $.ajax({
                type: "POST",
                url: ajaxUrl,
                dataType: "json",
                data: ajaxData,
                time: 10,
                success: function (msg) {
                    if (msg.success) {
                        callback(msg)
                    }
                    else {
                        alert(msg.errorMessage);
                    }
                },
            });
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
                ajaxCall("/chat/StartSession", {username : username}, subscribeToChannel);
            } else {
                errorMessage.show();
                loader.hide();
                $this.show();
            }
        };

        var subscribeToChannel = function () {
            var pusher = new Pusher("76ad3a948291ca99d5d7");
            Pusher.channel_auth_endpoint = "/chat/Auth";
            var pushermvcchannel = pusher.subscribe('presence-pushermvcchat');

            //bind a function after connecting to pusher
            //pusher.connection.bind('connected', loadChatroom(pushermvcchannel));
            pusher.connection.bind('connected', function () {
                $("#chat_widget_login_loader").hide();
                $("#chat_widget_login_button").show();

                $("#chat_widget_login").hide();
                $("#chat_widget_main_container").show();

                //this event fires when in succesfully subscribe to the channel
                //pushermvcchannel.bind("pusher:subscription_succeeded", viewAvailableMembers);
                pushermvcchannel.bind("pusher:subscription_succeeded", function (members) {
                    var whosonlinehtml = "";
                    members.each(function (member) {
                        whosonlinehtml += '<li class="chat_widget_member" id="chat_widget_member_'
                            + member.id + '">'
                            + member.info.username
                            + '</li>';
                    });

                    $("#chat_widget_online_list").html(whosonlinehtml);
                });

                //this event fires whenever another user succesfully subscribes to the channel
                //pushermvcchannel.bind("pusher:member_added", addSubscribedUser);
                pushermvcchannel.bind("pusher:member_added", function (member) {
                    $("#chat_widget_online_list").append('<li class="chat_widget_member" '
                        + 'id="chat_widget_member_'
                        + member.id
                        + '">'
                        + member.info.username
                        + '</li>')
                });

                //this event fires whenever a user leaves the channel
                //pushermvcchannel.bind("pusher:member_removed", removeUser);
                pushermvcchannel.bind("pusher:member_removed", function (member) {
                    $('#chat_widget_member_' + member.id).remove();

                    //updateonlinecount
                });
            });

        };

            //var loadChatroom = function (pushermvcchannel) {
            //    $("#chat_widget_login_loader").hide();
            //    $("#chat_widget_login_button").show();

            //    $("#chat_widget_login").hide();
            //    $("chat_widget_main_container").show();

            //    //this event fires when in succesfully subscribe to the channel
            //    pushermvcchannel.bind("pusher:subscription_succeeded", viewAvailableMembers);

            //    //this event fires whenever another user succesfully subscribes to the channel
            //    pushermvcchannel.bind("pusher:member_added", addSubscribedUser);

            //    //this event fires whenever a user leaves the channel
            //    pushermvcchannel.bind("pusher:member_removed", removeUser);
            //};

            //var removeUser = function (member) {
            //    $('#chat_widget_member_' + member.id).remove();

            //    //updateonlinecount
            //};

            //var addSubscribedUser = function (member) {
            //    $("#chat_widget_online_list").append('li class="chat_widget_member" '
            //        + 'id="chat_widget_member_'
            //        + member.id
            //        + '">'
            //        + member.info.username
            //        + '</li>');

            //    //updateonlinecount
            //};

            //var viewAvailableMembers = function (members) {
            //    var whosonlinehtml = "";
            //    members.each(function (member) {
            //        whosonlinehtml += '<li class="chet_widget_member" id="chat_widget_member_'
            //            + member.id + '">'
            //            + member.info.username
            //            + '</li>';
            //    });

            //    $("#chat_widget_online_list").html(whosonlinehtml);
            //    //updateonlinecount
            //};

            subscribeToProductDetailsChannel();
            $('#BuyButton').on("click", triggerBuy);
            $('#chat_widget_login_button').on('click', loginToChatroom);
        });
    })(jQuery);