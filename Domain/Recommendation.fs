namespace PhoneCat.Domain
open System.Reactive.Subjects
open System.Threading

type Agent<'T> = MailboxProcessor<'T>

[<AutoOpen>]
module Recommendation =

  let RecommendationPipe = new Subject<string>()

  let private suggestRecommendation = function
    | ["motorola-xoom-with-wi-fi"; "motorola-xoom"] -> RecommendationPipe.OnNext "motorola-atrix-4g"
    | ["dell-streak-7"; "dell-venue"] -> RecommendationPipe.OnNext "nexus-s"
    | _ -> ()

  let private recommendationAgentFunc (inbox : Agent<List<string>>) =
    let rec messageLoop () = async {
      let! visitedPhoneIds = inbox.Receive()     
      if (Seq.length visitedPhoneIds) >= 2 then
        Thread.Sleep(3000)
        suggestRecommendation (visitedPhoneIds |> Seq.take 2 |> Seq.toList)
      return! messageLoop()
    }
    messageLoop ()

  let RecommendationAgent = Agent.Start recommendationAgentFunc
  

  

