namespace PhoneCat.Domain
open System.Reactive.Subjects
open System.Threading

type Agent<'T> = MailboxProcessor<'T>

[<AutoOpen>]
module Recommendation =

  let RecommendationPipe = new Subject<string*string option>()

  let private suggestRecommendation connectionId visitedPhoneIds = 
    match visitedPhoneIds with
    | ["motorola-xoom-with-wi-fi"; "motorola-xoom"] -> RecommendationPipe.OnNext (connectionId, Some "motorola-atrix-4g")
    | ["dell-streak-7"; "dell-venue"] -> RecommendationPipe.OnNext (connectionId, Some "nexus-s")
    | _ -> RecommendationPipe.OnNext (connectionId, None)

  let private recommendationAgentFunc (inbox : Agent<string*List<string>>) =
    let rec messageLoop () = async {
      let! connectionId, visitedPhoneIds = inbox.Receive()     
      if (Seq.length visitedPhoneIds) >= 2 then
        suggestRecommendation connectionId (visitedPhoneIds |> Seq.take 2 |> Seq.toList)
      return! messageLoop()
    }
    messageLoop ()

  let RecommendationAgent = Agent.Start recommendationAgentFunc
  

  

