namespace PhoneCat.DataAccess
open FSharp.Data
open PhoneCat.Domain

module Promotions =

    [<Literal>]
    let private samplePhonesJsonData = """ 
        [{
            "age": 0,
            "id": "bar",
            "imageUrl": "foo",
            "name": "bar",
            "snippet": "foo"
        },{
            "age": 1,
            "id": "bar",
            "imageUrl": "foo",
            "name": "bar",
            "snippet": "foo"
        }]
    """

    type PhonesTypeProvider = JsonProvider<"https://raw.githubusercontent.com/angular/angular-phonecat/master/app/phones/phones.json">

    let private phones = PhonesTypeProvider.GetSamples()

    let getPromotions () = 
        phones 
        |> Seq.filter (fun phone -> phone.Age < 3) 
        |> Seq.map (fun phone -> {Id = phone.Id; Name = phone.Name; ImageUrl = phone.ImageUrl})