namespace PhoneCat.Web
open System.Web
open System.Reactive.Subjects
open PhoneCat.Domain

module PhoneViewTracker =
  
  let private sessionVariableName = "ViewedPhoneIds"

  let private recentlyViewedPhoneIds (session : HttpSessionStateBase) = 
    if session.[sessionVariableName] = null then List.empty
    else session.[sessionVariableName] :?> list<string>
    
  let observePhonesViewed (session : HttpSessionStateBase) lastViewedPhoneId =
    let phoneIdsVisited = lastViewedPhoneId :: (recentlyViewedPhoneIds session)
    RecommendationAgent.Post phoneIdsVisited
    session.[sessionVariableName] <- phoneIdsVisited 
