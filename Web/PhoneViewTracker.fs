namespace PhoneCat.Web
open System.Web
open System.Reactive.Subjects
open PhoneCat.Domain
open System.Collections.Generic
open PhoneCat.Domain.UserNavigationHistory

module PhoneViewTracker =     
    
  let observePhonesViewed (anonymousId : string) (phoneIdBeingVisited : string) =
    StorageAgent.Post (SavePhoneVisit (anonymousId, phoneIdBeingVisited))
  
  

  