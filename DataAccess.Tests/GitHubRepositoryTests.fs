namespace PhoneCat.DataAccess.Tests

open NUnit.Framework
open FsUnit
open PhoneCat.Domain
open PhoneCat.DataAccess
open FSharp.Data
open FSharp.Data.HttpResponseHeaders


module GitHubRepositoryTests = 
    [<Literal>]
    let private baseUrl = "https://raw.githubusercontent.com/angular/angular-phonecat/master/app/phones/"
    
    [<Literal>]
    let private phoneIndexUrl = baseUrl + "phones.json"
    
    let private requestHeaders = [ ContentType, HttpContentTypes.Json ]
    
    [<Test>]
    [<Category("LongRunning")>]
    [<Ignore>]
    let `` getPhoneIndexes returns all the phone indexes from the angular github repository ``() = 
        let response = Http.RequestString(phoneIndexUrl, silentHttpErrors = true, headers = requestHeaders)
        let expectedPhoneIndexes = PhoneIndexTypeProvider.Parse(response)

        let actualPhoneIndexes = GitHubRepository.getPhoneIndexes()
        
        actualPhoneIndexes |> should equal expectedPhoneIndexes
    
    let private getPhoneRoots ids =
        seq {for id in ids -> Http.AsyncRequestString(baseUrl + id + ".json", silentHttpErrors = true, headers = requestHeaders)}
        |> Async.Parallel
        |> Async.RunSynchronously
        |> Seq.map (fun response -> PhoneTypeProvider.Parse(response))    

    [<Test>]
    [<Category("LongRunning")>]
    [<Ignore>]
    let `` getPhones returns all the phones from the angular github repository ``() = 
        let response = Http.RequestString(phoneIndexUrl, silentHttpErrors = true, headers = requestHeaders)
        let phoneIds = PhoneIndexTypeProvider.Parse(response) |> Seq.map (fun p -> p.Id)
        let expectedPhoneRoots = getPhoneRoots phoneIds

        let actualPhoneRoots = GitHubRepository.getPhones()

        actualPhoneRoots |> should equal actualPhoneRoots
        
        
