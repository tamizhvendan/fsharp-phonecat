namespace PhoneCat.DataAccess
open PhoneCat.Domain

module GitHubRepository = 
    let phoneIndexes = PhoneIndexTypeProvider.Load("https://raw.githubusercontent.com/angular/angular-phonecat/master/app/phones/phones.json")

