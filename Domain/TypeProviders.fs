namespace PhoneCat.Domain
open FSharp.Data

[<AutoOpen>]
module TypeProviders =

    [<Literal>]
    let private samplePhoneIndexes = """
    [{
        "age": 1,
        "id": "id0",
        "imageUrl": "id0.jpg",
        "name": "Name0",
        "snippet": "Sample Snippet0"
    },{
        "age": 2,
        "id": "id1",
        "imageUrl": "id1.jpg",
        "name": "Name1",
        "snippet": "Sample Snippet2"
    }]
    """
    
    type PhoneIndexTypeProvider = JsonProvider<samplePhoneIndexes>
    

