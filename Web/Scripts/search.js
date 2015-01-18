var siteName = window.PhoneCat.siteName;
var phoneBaseUrl = siteName + "/phone/show/";
var imageHostUrl = "http://angular.github.io/angular-phonecat/step-12/app/";

function PhoneViewModel(phone) {
  this.imageUrl = ko.observable(imageHostUrl + phone.images[0]);
  this.phoneUrl = ko.observable(phoneBaseUrl + phone.id);
  this.name = ko.observable(phone.name);
  this.description = ko.observable(phone.description);
}

function SearchPageViewModel() {
  var self = this;
  self.q = ko.observable("");
  self.isSearching = ko.observable(false);
  self.isSearchQueryError = ko.observable(false);
  self.errorMsg = ko.observable("");
  self.searchResults = ko.observableArray([]);
  self.isEmptyResults = ko.observable(false);

  self.search = function () {
    var q = this.q();
    self.isSearching(true);
    self.searchResults([]);
    self.errorMsg("");
    self.isSearchQueryError(false);
    self.isEmptyResults(false);
    $.getJSON(siteName + "/api/phones/search?q=" + q, function (searchResults) {
      self.isSearching(false);
      if (searchResults.message) {
        self.errorMsg(searchResults.message);
        self.isSearchQueryError(true);

      } else {
        if (searchResults.length === 0) {
          self.isEmptyResults(true);
        } else {
          var phoneViewModels = $.map(searchResults, function (x) {
            return new PhoneViewModel(x);
          });
          self.searchResults(phoneViewModels)
        }
      }
    });
  }
}

$(document).ready(function () {
  ko.applyBindings(new SearchPageViewModel());
});

