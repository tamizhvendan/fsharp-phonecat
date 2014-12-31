(function () {
  var recommendationHub = $.connection.recommendationHub;

  recommendationHub.client.showRecommendation = function (phone, url) {
    var html = notificationHtml.replace("{url}", url).replace("{phoneName}", phone.Name);
    generate("alert", html);
  };
  
  var notificationHtml = "<div class='activity-item'>" +
                              "<i class='fa fa-heart text-danger'></i>" +
                              "<div class='activity'>" +
                                "You may like visiting <a href='{url}' style='color: #428bca;'>{phoneName}</a>" +
                                "<span>Powered by Noty</span>" +
                              "</div>" +
                            "</div>";

  function generate(type, text) {

    noty({
      text: text,
      type: type,
      dismissQueue: true,
      layout: 'centerRight',
      closeWith: ['click'],
      theme: 'relax',
      maxVisible: 10,
      animation: {
        open: 'animated bounceInRight',
        close: 'animated bounceOutRight',
        easing: 'swing',
        speed: 500
      }
    });
  }

  $.connection.hub.start().done(function() {
    console.log("Hub started");
    recommendationHub.server.getRecommendation().done(function(data) {
      console.log("Notification");
      console.log(data);
    });
  });

})();