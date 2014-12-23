var imageHostUrl = "http://angular.github.io/angular-phonecat/step-12/app/";
var phoneBaseUrl = "phone/view/";

function PromotionPhoneViewModel(promotionPhone, active) {
    this.imageUrl = ko.observable(imageHostUrl + promotionPhone.imageUrl);
    this.phoneUrl = ko.observable(phoneBaseUrl + promotionPhone.id);
    this.name = ko.observable(promotionPhone.name);
    this.active = ko.observable(active);
}

function TopSellingPhoneViewModel(topSellingPhone) {
    this.imageUrl = ko.observable(imageHostUrl + topSellingPhone.imageUrl);
    this.phoneUrl = ko.observable(phoneBaseUrl + topSellingPhone.id);
    this.name = ko.observable(topSellingPhone.name);
    this.description = ko.observable(topSellingPhone.description);
}

function ManufacturerViewModel(manufacturer) {
    this.name = ko.observable(manufacturer.name);
    this.manufactureUrl = ko.observable("manufacturer/" + manufacturer.name);
}


function HomePageViewModel() {

    var self = this;

    self.promotionPhones = ko.observableArray([]);
    self.topSellingPhones = ko.observableArray([]);
    self.manufacturers = ko.observableArray([]);

    $.getJSON("api/promotions", function (promotionPhones) {
        var promotionPhoneViewModels = $.map(promotionPhones, function (x, i) {
            if (i == 1)
                return new PromotionPhoneViewModel(x, "item active");
            else
                return new PromotionPhoneViewModel(x, "item");
        });
        self.promotionPhones(promotionPhoneViewModels);
    });

    $.getJSON("api/phones/topselling", function (topSellingPhones) {
        var topSellingPhoneViewModels = $.map(topSellingPhones, function (x) {
            return new TopSellingPhoneViewModel(x);
        });
        self.topSellingPhones(topSellingPhoneViewModels);
    });

    $.getJSON("api/manufacturers", function (manufacturers) {
        var manufacturerViewModels = $.map(manufacturers, function (x) {
            return new ManufacturerViewModel(x);
        });
        self.manufacturers(manufacturerViewModels);
    });
}


$(document).ready(function () {
    ko.applyBindings(new HomePageViewModel());
});

