namespace PhoneCat.Domain

[<AutoOpen>]
module Production = 
    
    type ManufacturerName = 
        Samsung | Motorola | Dell | LG | TMobile | Sanyo | Unknown
        
        static member ToString = function           
            | Samsung -> "Samsung"
            | Motorola -> "Motorola"
            | Dell -> "Dell"
            | LG -> "LG"
            | TMobile -> "T-Mobile"
            | Sanyo -> "Sanyo"
            | Unknown -> "Unknown"

        static member ToManufacturerName (name : string) =
            match name.ToLower() with
            | n when n.Contains("samsung") -> Samsung
            | n when n.Contains("motorola") -> Motorola
            | n when n.Contains("dell") -> Dell
            | n when n.Contains("lg") -> LG
            | n when n.Contains("t-mobile") -> TMobile
            | n when n.Contains("sanyo") -> Sanyo
            | n when n.Contains("nexus") -> Samsung
            | _ -> Unknown

    type Phone = 
        { Id : string
          Name : string
          Description : string
          ImageUrl : string }
                    
    type PhoneIndex =
        { Id : string
          Name : string
          ImageUrl : string
          Age : int }        

    type Manufacturer = 
        { Name : ManufacturerName
          Phones : seq<Phone> }

