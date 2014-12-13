function PromotionItemViewModel(promotionItem, active) {
    var imageHostUrl = "http://angular.github.io/angular-phonecat/step-12/app/";
    this.imageUrl = ko.observable(imageHostUrl + promotionItem.imageUrl);
    this.phoneUrl = ko.observable("phones/" + promotionItem.id);
    this.name = ko.observable(promotionItem.name);
    this.active = ko.observable(active);
}

function HomePageViewModel() {

    var self = this;

    self.promotionItems = ko.observableArray([]);

    $.getJSON("api/promotions", function(promotionItems) {
        var items = $.map(promotionItems, function(x, i) {
            if (i == 1)
                return new PromotionItemViewModel(x, "item active");
            else
                return new PromotionItemViewModel(x, "item");
        });
        self.promotionItems(items);
    });
}


$(document).ready(function () {
    ko.applyBindings(new HomePageViewModel());
});

