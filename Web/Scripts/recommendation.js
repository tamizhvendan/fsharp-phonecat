(function () {
  var recommendationHub = $.connection.recommendationHub;

  recommendationHub.client.showRecommendation = function (phone, url) {
    console.log("Phone : " + phone.Name);
    console.log("Url : " + url);
  };

  $.connection.hub.start();

})();