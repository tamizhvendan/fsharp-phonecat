namespace PhoneCat.Web

open PhoneCat.Domain.UserNavigationHistory

module PhoneViewTracker =     
    
  let observePhonesViewed anonymousId phoneIdBeingVisited =
    StorageAgent.Post (SavePhoneVisit (anonymousId, phoneIdBeingVisited))
  
  

  