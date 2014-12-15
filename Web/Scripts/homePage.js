var imageHostUrl = "http://angular.github.io/angular-phonecat/step-12/app/";

function PromotionPhoneViewModel(promotionPhone, active) {
    this.imageUrl = ko.observable(imageHostUrl + promotionPhone.imageUrl);
    this.phoneUrl = ko.observable("phones/" + promotionPhone.id);
    this.name = ko.observable(promotionPhone.name);
    this.active = ko.observable(active);
}

function TopSellingPhoneViewModel(topSellingPhone) {
    this.imageUrl = ko.observable(imageHostUrl + topSellingPhone.imageUrl);
    this.phoneUrl = ko.observable("phones/" + topSellingPhone.id);
    this.name = ko.observable(topSellingPhone.name);
    this.description = ko.observable(topSellingPhone.description);
}


function HomePageViewModel() {

    var self = this;

    self.promotionPhones = ko.observableArray([]);
    self.topSellingPhones = ko.observableArray([]);

    $.getJSON("api/promotions", function (promotionPhones) {
        var phones = $.map(promotionPhones, function (x, i) {
            if (i == 1)
                return new PromotionPhoneViewModel(x, "item active");
            else
                return new PromotionPhoneViewModel(x, "item");
        });
        self.promotionPhones(phones);
    });

    $.getJSON("api/phones/topselling", function (topSellingPhones) {
        var phones = $.map(topSellingPhones, function (x) {
            return new TopSellingPhoneViewModel(x);
        });
        self.topSellingPhones(phones);
    });
}


$(document).ready(function () {
    ko.applyBindings(new HomePageViewModel());
});

