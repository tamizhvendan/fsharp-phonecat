function SearchPageViewModel() {
  var self = this;
  self.q = ko.observable("");
  self.search = function () {
    console.log(this.q());
  }
}

$(document).ready(function () {
  ko.applyBindings(new SearchPageViewModel());
});