namespace PhoneCat.DataAccess
open PhoneCat.Domain

module GitHubRepository = 

    [<Literal>]
    let private gitHubRepoUrl = "https://raw.githubusercontent.com/angular/angular-phonecat/master/app/phones/"
    
    let private phoneIndexes = PhoneIndexTypeProvider.Load(gitHubRepoUrl + "phones.json")

    let private phoneIds = phoneIndexes |> Seq.map (fun p -> p.Id)

    let private pMap ids =
        seq {for id in ids -> async { return id, PhoneTypeProvider.Load(gitHubRepoUrl + id + ".json") }}
        |> Async.Parallel
        |> Async.RunSynchronously

    let private phones = pMap phoneIds |> Map.ofSeq

    let getPhoneIndexes () = phoneIndexes

    let getPhone id = phones.Item(id)

    let getPhones () = phones |> Seq.map (fun p -> p.Value)
        
