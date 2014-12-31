namespace PhoneCat.Web
open System.Web
open System.Reactive.Subjects
open PhoneCat.Domain
open System.Collections.Generic

type Agent<'T> = MailboxProcessor<'T>

module PhoneViewTracker =  

  type StorageAgentMessage =
    | SavePhoneVisit of string * string
    | GetRecommendation of string * string
  
  let private storageAgentFunc (agent : Agent<StorageAgentMessage>) =  
    let rec loop (dict : Dictionary<string, list<string>>) = async { 
      let! storageAgentMessage = agent.Receive()
      match storageAgentMessage with
      | SavePhoneVisit (anonymousId, phoneIdBeingVisited) -> 
          if dict.ContainsKey(anonymousId) then
            let phoneIdsVisited = phoneIdBeingVisited :: dict.[anonymousId]
            dict.[anonymousId] <- phoneIdsVisited
          else
            dict.Add(anonymousId, [phoneIdBeingVisited])
      | GetRecommendation (anonymousId, connectionId) ->
          if dict.ContainsKey(anonymousId) then
            let phoneIdsVisited = dict.[anonymousId]
            RecommendationAgent.Post (connectionId,phoneIdsVisited)
      return! loop dict
    }
    loop (new Dictionary<string, list<string>>())

  let StorageAgent = Agent.Start(storageAgentFunc)  
    
  let observePhonesViewed (anonymousId : string) (phoneIdBeingVisited : string) =
    StorageAgent.Post (SavePhoneVisit (anonymousId, phoneIdBeingVisited))
  
  

  